using Dapper;
using System.Data;
using static Dapper.SqlMapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Common.Dtos;
using Csis.Admission.Application.Common.Configuration;

namespace Csis.Admission.Persistence;
public class AppDapperContext
{
    private readonly DatabaseOptions _databaseOptions;
    private readonly ILogger<AppDapperContext> _logger;
    private readonly int _commandTimeout;
    public AppDapperContext(ILogger<AppDapperContext> logger, IOptions<DatabaseOptions> databaseOptions) {
        _logger = logger;
        _databaseOptions = databaseOptions.Value;
        _commandTimeout = 180;//second
    }

    private IDbConnection CreateConnection() => new SqlConnection(_databaseOptions.ConnectionStrings.SqlServer);

    public async Task<dynamic[]> ExecuteQuery(string query) {
        using var connection = CreateConnection();
        var entities = await connection.QueryAsync(query,commandTimeout:_commandTimeout);
        return entities.ToArray();
    }

    public async Task<List<TEntity>> ExecuteProcedureToList<TEntity>(ProcedureName procedure, object param = null) {
        var procedureName = "stu."+procedure.ToString();
        using var connection = CreateConnection();
        var queryLog = $"EXEC {procedureName} {SqlParametersToString(param)}";
        _logger.LogInformation(procedureName);
        var entities = await connection.QueryAsync<TEntity>(procedureName, param, commandType: CommandType.StoredProcedure, commandTimeout: _commandTimeout);
        return entities.ToList();
    }

    public async Task<TEntity> ExecuteProcedureSingleOrDefault<TEntity>(ProcedureName procedure, object param = null) {
        var procedureName = "stu." + procedure.ToString();
        using var connection = CreateConnection();
        var queryLog = $"EXEC {procedureName} {SqlParametersToString(param)}";
        _logger.LogInformation(procedureName);
        var entity = await connection.QuerySingleOrDefaultAsync<TEntity>(procedureName, param, commandType: CommandType.StoredProcedure, commandTimeout: _commandTimeout);

        if ( entity is ProcedureResultDto result ) {
            result.ThrowIfUnsuccessful();
        }

        return entity;
    }

    public async Task<TEntity> ExecuteProcedureBaseSingleOrDefault<TEntity>(ProcedureName procedure, object param = null) {
        var procedureName = "stu." + procedure.ToString();
        using var connection = CreateConnection();
        var queryLog = $"EXEC {procedureName} {SqlParametersToString(param)}";
        _logger.LogInformation(procedureName);
        var entity = await connection.QuerySingleOrDefaultAsync<TEntity>(procedureName, param, commandType: CommandType.StoredProcedure, commandTimeout: _commandTimeout);

        if ( entity is ProcedureResultDto result ) {
            result.ThrowIfUnsuccessful();
        }

        return entity;
    }

    public async Task ExecuteProcedure(ProcedureName procedure, object param = null) {
        var procedureName = "stu." + procedure.ToString();
        using var connection = CreateConnection();
        var queryLog = $"EXEC {procedureName} {SqlParametersToString(param)}";
        _logger.LogInformation(procedureName);
        var entity = await connection.QueryAsync(procedureName, param, commandType: CommandType.StoredProcedure, commandTimeout: _commandTimeout);
    }

    private static string SqlParametersToString(object @params) {

        if ( @params == null ) {
            return null;
        }

        var parameters = new List<string>();
        foreach ( var property in @params.GetType().GetProperties() ) {

            var name = property.Name;
            var value = property.GetValue(@params);
            if ( property.PropertyType == typeof(string) || property.PropertyType == typeof(DateTime) ) {
                value = $"'{value}'";
            }

            if ( property.PropertyType.IsEnum ) {
                value = Convert.ChangeType(value, Enum.GetUnderlyingType(property.PropertyType));
            }

            parameters.Add("@" + name + "=" + (value != null ? value : "null"));
        }

        return string.Join(',', parameters);
    }
}
