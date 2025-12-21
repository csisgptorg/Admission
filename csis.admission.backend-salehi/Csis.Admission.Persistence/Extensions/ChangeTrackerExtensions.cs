using Csis.Utilities.Extensions;
using Csis.Admission.Domain.Enums;
using Csis.Admission.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Csis.Admission.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Csis.Admission.Persistence.Extensions;
internal static partial class ChangeTrackerExtensions
{
    // Excluded fields from audit log
    private readonly static string[] _excludedAuditFields = [
        "Id",
        "CreatedOn",
        "UpdatedOn",
        "CreatedById",
        "LastUpdatedById",
        "Deleted",
        "DeletedOn",
        "DeletedById",
        "TempId"
    ];

    private readonly static Dictionary<EntityEntry, Guid> _entryTempIdMap = [];

    internal static List<AuditLog> DetectAuditLogs(this ChangeTracker changeTracker,
        int? personnelId, int? codm, int? currentUserId, int? delegatedUserId, int? applicationId, DateTime currentDate) {
        var entries = changeTracker.Entries<IAuditable>()
                    .Where(x => x.State is
                        EntityState.Added or
                        EntityState.Modified or
                        EntityState.Deleted);

        if ( entries.Any() ) {

        }

        var auditLogs = new List<AuditLog>();
        var operationId = Guid.NewGuid();

        foreach ( var entry in entries ) {

            //TODO نیازمند بهبود
            if ( codm is null ) {
                var codmProp = entry.Properties.Where(x => x.Metadata.Name.Equals("Codm")).FirstOrDefault();
                if ( codmProp is not null ) {
                    codm = int.Parse(codmProp.CurrentValue.ToString());
                }
            }

            var logs = GetAuditLogEntries(entry, currentDate, currentUserId, delegatedUserId, applicationId, operationId).ToArray();
            foreach ( var log in logs ) {
                log.PersonnelId = personnelId;
                log.Codm = codm;
                log.DataSource = applicationId > 0 ? DataSource.WebService : personnelId > 0 ? DataSource.Employee : codm > 0 ? DataSource.Student : null;
            }
            auditLogs.AddRange(logs);
        }

        return auditLogs;
    }

    internal static int SetAuditTableRecordIds(this ChangeTracker changeTracker, List<AuditLog> auditLogs) {
        var count = 0;
        var auditableEntries = changeTracker.Entries<IAuditable>();

        foreach ( var auditLog in auditLogs ) {
            var entry = auditableEntries.Where(x => x.Entity.TempId == auditLog.TempId).FirstOrDefault();
            if ( entry is not null ) {
                auditLog.TableRecordId = Convert.ToInt64(entry.Property(nameof(IEntity.Id)).CurrentValue);
                count++;
            } else {
                // ?
            }
        }

        return count;
    }

    private static IEnumerable<AuditLog> GetAuditLogEntries(
        EntityEntry<IAuditable> entry,
        DateTime now,
        int? userId,
        int? delegatedUserId,
        int? applicationId,
        Guid operationId) {

        var table = entry.Entity.GetType().Name;

        //var tableId = AuditUtilities.GetTableId(entityName)
        //    ?? throw new Exception($"No table id is set for entity {entityName}");

        var idProp = entry.Properties.Where(x => x.Metadata.Name.Equals(nameof(IEntity.Id))).FirstOrDefault()
            ?? throw new Exception($"No Id property found on entity {entry.Entity.GetType().Name}");

        var id = idProp.OriginalValue.ToString().ToLong();
        var date = now.ToPersianInteger();
        var time = new TimeOnly(now.Hour, now.Minute, now.Second, now.Millisecond);
        var auditGroupId = Guid.NewGuid();

        if ( entry.State is EntityState.Added ) {
            var tempId = Guid.NewGuid();
            var auditLog = new AuditLog {
                AuditOperationId = operationId,
                AuditGroupId = auditGroupId,
                TempId = tempId,
                Table = table,
                ActionType = AuditActionType.Create,
                Date = date,
                Time = time,
                RequestId = entry.Entity.AuditRequestId,
                DataSource = entry.Entity.AuditDataSource,
                PersonnelId = entry.Entity.AuditPersonId,
                ApplicationId = applicationId,
            };

            auditLog.SetCreatedOn(now);
            auditLog.SetCreatedById(userId, delegatedUserId);
            //entry.Property(x => x.TempId).CurrentValue = tempId;
            entry.Entity.TempId = tempId;
            yield return auditLog;

        } else { // Modified (including soft deleted) or deleted

            var deletedProp = entry.Properties.Where(x => x.Metadata.Name.Equals(nameof(ISoftDeletedEntity.Deleted))).FirstOrDefault();
            var softDeleted = deletedProp is not null && !Equals(deletedProp.OriginalValue, deletedProp.CurrentValue) && Equals(deletedProp.CurrentValue, true);
            if ( softDeleted || entry.State == EntityState.Deleted ) { // Delete                

                var auditLog = new AuditLog {
                    AuditOperationId = operationId,
                    AuditGroupId = auditGroupId,
                    ActionType = AuditActionType.Delete,
                    Table = table,
                    TableRecordId = id,
                    Date = date,
                    Time = time,
                    RequestId = entry.Entity.AuditRequestId,
                    DataSource = entry.Entity.AuditDataSource,
                    //PersonId = entry.Entity.AuditPersonId,
                    ApplicationId = applicationId,
                };

                auditLog.SetCreatedOn(now);
                auditLog.SetCreatedById(userId, delegatedUserId);
                yield return auditLog;

            } else { // Update

                foreach ( var prop in entry.Properties ) {
                    if ( entry.Metadata.FindNavigation(prop.Metadata.Name) is not null ) {
                        continue;
                    }

                    if ( _excludedAuditFields.Contains(prop.Metadata.Name, StringComparer.OrdinalIgnoreCase) ) {
                        continue;
                    }

                    // Get the original and current values
                    var originalValue = prop.OriginalValue;
                    var currentValue = prop.CurrentValue;

                    // Check if the property is a non-navigation collection
                    if ( prop.Metadata.ClrType != typeof(string) &&
                        typeof(System.Collections.IEnumerable).IsAssignableFrom(prop.Metadata.ClrType) ) {

                        if ( originalValue is System.Collections.IEnumerable originalCollection && originalCollection is not string ) {
                            if ( originalCollection.GetType().GetGenericArguments().FirstOrDefault()?.IsEnum == true ) {
                                originalValue = string.Join(',', originalCollection.Cast<Enum>().Select(Convert.ToInt32));
                            } else {
                                originalValue = string.Join(',', originalCollection.Cast<object>());
                            }
                        }

                        if ( currentValue is System.Collections.IEnumerable currentCollection && currentCollection is not string ) {
                            if ( currentCollection.GetType().GetGenericArguments().FirstOrDefault()?.IsEnum == true ) {
                                currentValue = string.Join(',', currentCollection.Cast<Enum>().Select(Convert.ToInt32));
                            } else {
                                currentValue = string.Join(',', currentCollection.Cast<object>());
                            }
                        }
                    }

                    if ( !Equals(originalValue, currentValue) ) {

                        var auditLog = new AuditLog {
                            AuditOperationId = operationId,
                            AuditGroupId = auditGroupId,
                            Table = table,
                            Column = prop.Metadata.Name,
                            TableRecordId = id,
                            ActionType = AuditActionType.Update,
                            OldValue = originalValue?.ToString(),
                            NewValue = currentValue?.ToString(),
                            Date = date,
                            Time = time,
                            RequestId = entry.Entity.AuditRequestId,
                            DataSource = entry.Entity.AuditDataSource,
                            //PersonId = entry.Entity.AuditPersonId,
                            ApplicationId = applicationId,
                        };

                        auditLog.SetCreatedOn(now);
                        auditLog.SetCreatedById(userId, delegatedUserId);
                        yield return auditLog;
                    }
                }
            }

        }

    }
}
