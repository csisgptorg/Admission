using Csis.Admission.Application.Common.Models;
using Csis.Admission.Application.Features.CaseFilings.Dtos;
using Csis.Admission.Application.Features.Files.Dtos;
using Csis.FileManagement;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Csis.Admission.Application.Common.Helpers;

/// <summary>
/// Helper for FileInfo
/// </summary>
public static class FileInfoHelper
{
    /// <summary>
    /// Set files info for request dto
    /// </summary>
    /// <param name="requestDto"></param>
    /// <param name="fileManagementService"></param>
    /// <returns></returns>
    public static async Task SetRequestFilesInfoAsync(List<RequestDto> requestDto, ICsisFileManagementService fileManagementService) {
        if ( requestDto.Any() ) {
            foreach ( var document in requestDto ) {
                foreach ( var documentDto in document.Documents ) {
                    try {
                        var downloadLink = await fileManagementService.DownloadLink(documentDto.FileId);
                        document.FilesInfo.Add(new FileModelDto {
                            Link = downloadLink.Link,
                            FullName = downloadLink.FullName,
                            FileType = (FileTypeEnum) documentDto.Type,
                            Guid = documentDto.FileId,
                        });
                    } catch ( Exception ) {
                        document.FilesInfo.Add(new FileModelDto {
                            Link = null,
                            FullName = null,
                            FileType = FileTypeEnum.Unknown,
                            Guid = documentDto.FileId,
                        });
                    }
                }
            }
        }
    }

    /// <summary>
    /// Set files info for request dto
    /// </summary>
    /// <param name="requestDto"></param>
    /// <param name="fileManagementService"></param>
    /// <returns></returns>
    public static async Task SetRequestFilesInfoAsync(List<CaseFillingRequestDto> requestDto, ICsisFileManagementService fileManagementService) {
        if ( requestDto.Any() ) {
            foreach ( var document in requestDto ) {
                foreach ( var documentDto in document.Documents ) {
                    try {
                        var downloadLink = await fileManagementService.DownloadLink(documentDto.FileId);
                        document.FilesInfo.Add(new FileModelDto {
                            Link = downloadLink.Link,
                            FullName = downloadLink.FullName,
                            FileType = (FileTypeEnum) documentDto.Type,
                            Guid = documentDto.FileId,
                        });
                    } catch ( Exception ) {
                        document.FilesInfo.Add(new FileModelDto {
                            Link = null,
                            FullName = null,
                            FileType = FileTypeEnum.Unknown,
                            Guid = documentDto.FileId,
                        });
                    }
                }
            }
        }
    }

    /// <summary>
    /// Set files info for request dto
    /// </summary>
    /// <param name="approveResults"></param>
    /// <param name="fileManagementService"></param>
    /// <returns></returns>
    public static async Task SetRequestFilesInfoAsync(List<SearchPersonnelRequestsToApproveResult> approveResults, ICsisFileManagementService fileManagementService) {
        if ( approveResults.Any() ) {
            foreach ( var document in approveResults ) {
                foreach ( var documentDto in document.Documents ) {
                    try {
                        var downloadLink = await fileManagementService.DownloadLink(documentDto.FileId);
                        document.FilesInfo.Add(new FileModelDto {
                            Link = downloadLink.Link,
                            FullName = downloadLink.FullName,
                            FileType = (FileTypeEnum) documentDto.Type,
                            Guid = documentDto.FileId,
                        });
                    } catch ( Exception ) {
                        document.FilesInfo.Add(new FileModelDto {
                            Link = null,
                            FullName = null,
                            FileType = FileTypeEnum.Unknown,
                            Guid = documentDto.FileId,
                        });
                    }
                }
            }
        }
    }

    /// <summary>
    /// Set files info for case-filing approval results (handles picture + old image from payload)
    /// </summary>
    /// <param name="approveResults"></param>
    /// <param name="fileManagementService"></param>
    /// <returns></returns>
    public static async Task SetRequestFilesInfoAsync(List<SearchPersonnelCaseFillingRequestsToApproveResult> approveResults, ICsisFileManagementService fileManagementService) {
        if ( !approveResults.Any() ) {
            return;
        }

        var serializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        foreach ( var approveResult in approveResults ) {
            if ( string.IsNullOrWhiteSpace(approveResult.Payload) ) {
                continue;
            }

            // Parse top-level payload
            JsonNode? root;
            try {
                root = JsonNode.Parse(approveResult.Payload);
            } catch ( Exception ) {
                root = null;
            }

            // Extract payloads array under caseUser
            JsonNode? payloadsNode = root?["caseUser"]?["payloads"];
            List<PayloadHelper.NamedPayload> payloads = new();

            if ( payloadsNode != null ) {
                try {
                    payloads = JsonSerializer.Deserialize<List<PayloadHelper.NamedPayload>>(payloadsNode.ToString(), serializerOptions) ?? new();
                } catch ( Exception ) {
                    payloads = new();
                }
            }

            // Local helper to add file info for a given nullable Guid
            async Task AddFileInfoAsync(Guid? id,RelatedSection relatedSection) {
                if ( id == null || id == Guid.Empty )
                    return;

                try {
                    var downloadLink = await fileManagementService.DownloadLink(id.Value);
                    approveResult.FilesInfo.Add(new CaseFilingFileModelDto {
                        Link = downloadLink.Link,
                        FullName = downloadLink.FullName,
                        FileType = FileTypeEnum.Image,
                        Guid = id.Value,
                        RelatedSection = relatedSection
                    });
                } catch ( Exception ) {
                    approveResult.FilesInfo.Add(new CaseFilingFileModelDto {
                        Link = null,
                        FullName = null,
                        FileType = FileTypeEnum.Image,
                        Guid = id.Value,
                        RelatedSection = relatedSection
                    });
                }
            }

            // Find picture payload and parse its payload JSON
            var pictureEntry = payloads.FirstOrDefault(x => x.Name == nameof(AdmissionCasePayloadName.Picture));
            if ( pictureEntry?.Payload != null ) {
                JsonNode? pictureNode;
                try {
                    pictureNode = JsonNode.Parse(pictureEntry.Payload.ToString(), new JsonNodeOptions { PropertyNameCaseInsensitive = true });
                } catch ( Exception ) {
                    pictureNode = null;
                }

                Guid? fileId = pictureNode?["fileId"]?.GetValue<Guid?>();
                Guid? oldImageFileId = pictureNode?["oldImageFileId"]?.GetValue<Guid?>();

                // Add primary and old image file infos if present
                await AddFileInfoAsync(fileId,RelatedSection.NewImage);
                await AddFileInfoAsync(oldImageFileId,RelatedSection.OldImage);
            }

            // Find bank account payload and parse its payload JSON
            var bankAccountEntry = payloads.FirstOrDefault(x => x.Name == nameof(AdmissionCasePayloadName.BankAccount));
            if ( bankAccountEntry?.Payload != null ) {
                JsonNode? bankAccountNode;
                try {
                    bankAccountNode = JsonNode.Parse(bankAccountEntry.Payload.ToString(), new JsonNodeOptions { PropertyNameCaseInsensitive = true });
                } catch ( Exception ) {
                    bankAccountNode = null;
                }

                Guid? bankFileId = bankAccountNode?["fileId"]?.GetValue<Guid?>();

                // Add bank account file info if present
                await AddFileInfoAsync(bankFileId,RelatedSection.Bank);
            }
        }
    }
    /// <summary>
    /// Set files info for case-filing approval results (handles picture + old image from payload)
    /// </summary>
    /// <param name="caseUser"></param>
    /// <param name="fileManagementService"></param>
    /// <returns></returns>
    public static async Task SetRequestFilesInfoAsync(AdmissionCaseUserDto caseUser, ICsisFileManagementService fileManagementService) {
        if ( caseUser is null ) {
            return;
        }

        // Local helper to add file info for a given nullable Guid
        async Task AddFileInfoAsync(Guid? id, RelatedSection relatedSection) {
            if ( id == null || id == Guid.Empty )
                return;

            try {
                var downloadLink = await fileManagementService.DownloadLink(id.Value);
                caseUser.FilesInfo.Add(new CaseFilingFileModelDto {
                    Link = downloadLink.Link,
                    FullName = downloadLink.FullName,
                    FileType = FileTypeEnum.Image,
                    Guid = id.Value,
                    RelatedSection = relatedSection
                });
            } catch ( Exception ) {
                caseUser.FilesInfo.Add(new CaseFilingFileModelDto {
                    Link = null,
                    FullName = null,
                    FileType = FileTypeEnum.Image,
                    Guid = id.Value,
                    RelatedSection = relatedSection
                });
            }
        }

        // Find picture payload and parse its payload JSON
        var pictureEntry = caseUser.Payloads?.FirstOrDefault(x => x.Name == nameof(AdmissionCasePayloadName.Picture));
        if ( pictureEntry?.Payload != null ) {
            JsonNode? pictureNode;
            try {
                pictureNode = JsonNode.Parse(pictureEntry.Payload.ToString(), new JsonNodeOptions { PropertyNameCaseInsensitive = true });
            } catch ( Exception ) {
                pictureNode = null;
            }

            Guid? fileId = pictureNode?["fileId"]?.GetValue<Guid?>();
            Guid? oldImageFileId = pictureNode?["oldImageFileId"]?.GetValue<Guid?>();

            // Add primary and old image file infos if present
            await AddFileInfoAsync(fileId,RelatedSection.NewImage);
            await AddFileInfoAsync(oldImageFileId,RelatedSection.OldImage);
        }

        // Find bank account payload and parse its payload JSON
        var bankAccountEntry = caseUser.Payloads?.FirstOrDefault(x => x.Name == nameof(AdmissionCasePayloadName.BankAccount));
        if ( bankAccountEntry?.Payload != null ) {
            JsonNode? bankAccountNode;
            try {
                bankAccountNode = JsonNode.Parse(bankAccountEntry.Payload.ToString(), new JsonNodeOptions { PropertyNameCaseInsensitive = true });
            } catch ( Exception ) {
                bankAccountNode = null;
            }

            Guid? bankFileId = bankAccountNode?["fileId"]?.GetValue<Guid?>();

            // Add bank account file info if present
            await AddFileInfoAsync(bankFileId,RelatedSection.Bank);
        }

    }
}
