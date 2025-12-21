using Csis.Authorization.Services;
using Microsoft.AspNetCore.Hosting;
using Csis.Admission.Application.Enums;
using Csis.Admission.Application.Common.Interfaces.Repositories.Student;

namespace Csis.Admission.Application.Features.Students.Queries;

/// <inheritdoc/>
public sealed record GetStudentProfileImageByCodmQuery(int Codm) : IRequest<string>;

internal sealed class GetStudentProfileImageByCodmQueryHandler : IRequestHandler<GetStudentProfileImageByCodmQuery, string>
{
    private readonly IStudentRepository _studentRepo;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly ICsisAuthenticatedUserService _authenticatedUserService;

    public GetStudentProfileImageByCodmQueryHandler(IStudentRepository studentRepo, IWebHostEnvironment hostEnvironment,
        ICsisAuthenticatedUserService authenticatedUserService) {
        _studentRepo = studentRepo;
        _hostEnvironment = hostEnvironment;
        _authenticatedUserService = authenticatedUserService;
    }

    public async Task<string> Handle(GetStudentProfileImageByCodmQuery request, CancellationToken cancellationToken) {
        const string imagePrefix = "data:image/jpg;base64,";

        var studentProfile = await _studentRepo.GetProfileImageByCodm(request.Codm);
        if ( studentProfile is null ) {
            return null;
        }

        var isFemale = studentProfile.Gender == Gender.Female;
        var isPersonnel = await _authenticatedUserService.GetPersonnelIdAsync() > 0;
        var accessToFemalePicture = !await _authenticatedUserService.IsAuthorizedToAsync(PermissionsEnum.FemaleInfoProfilePicture);
        if (isFemale && isPersonnel && accessToFemalePicture ) {
            return await GetNoAccessImageBase64("admission-files/no-access-profile-picture.png");
        }

        return imagePrefix + Convert.ToBase64String(studentProfile.Image);
    }

    private async Task<string> GetNoAccessImageBase64(string relativeImagePath) {
        const string imagePrefix = "data:image/jpg;base64,";
        var fullPath = Path.Combine(_hostEnvironment.WebRootPath, relativeImagePath);
        var imageBytes = await File.ReadAllBytesAsync(fullPath);
        return imagePrefix + Convert.ToBase64String(imageBytes);
    }
}
