/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common;
using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Shared.Kernel.Public.Models;
using Csis.Shared.Kernel.Public.Models.BaseInformation;
using Csis.Shared.Kernel.Public.Models.Students;
using Csis.Utilities.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Csis.Admission.Services;

internal sealed partial class StudentDataService : IStudentDataService
{
    #region Initialization
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly StudentDataServiceOptions _options;
    private readonly IMemoryCache _cache;
    private readonly ILogger<StudentDataService> _logger;

    public StudentDataService(
        HttpClient client,
        IOptions<StudentDataServiceOptions> options,
        IMemoryCache cache,
        ILogger<StudentDataService> logger) {
        _client = client;
        _options = options.Value;
        if ( !_options.BaseUrl.HasValue() || !Uri.TryCreate(_options.BaseUrl, UriKind.Absolute, out var baseUri) ) {
            throw new ArgumentException("Invalid base url provided for student data service", nameof(options));
        }

        if ( !_options.ApiKey.HasValue() ) {
            throw new ArgumentException("Invalid api key provided for student data service", nameof(options));
        }

        _client.DefaultRequestHeaders.Add("authorization", _options.ApiKey);
        _client.BaseAddress = baseUri;
        _client.Timeout = TimeSpan.FromSeconds(_options.TimeoutInSeconds);
        _jsonSerializerOptions = new JsonSerializerOptions {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _cache = cache;
        _logger = logger;
    }
    #endregion

    public async Task<PersonInfoExtended> GetStudentInfoAsync(string codm) {
        if ( !codm.HasValue() || !int.TryParse(codm, out var _) ) {
            _logger.LogDebug(Events.GetStudentInfo, "Invalid codm entered. returning null");
            return default;
        }

        #region Cache
        var cacheKey = $"ex-pinfo-{codm}";
        var cachedStudent = _cache.Get<PersonInfoExtended>(cacheKey);
        if ( cachedStudent is not null ) {
            _logger.LogDebug(Events.GetStudentInfo, "Reading student info with codm {codm} from cache", codm);
            return cachedStudent;
        }
        #endregion

        _logger.LogDebug(Events.GetStudentInfo, "Getting student info with codm {codm}", codm);
        var response = await _client.GetAsync($"api/person-info/student-extended/{codm}");
        _logger.LogDebug(Events.GetStudentInfo, "Getting student info with codm {codm} request finished with status code {status}", codm, response.StatusCode);
        response.EnsureSuccessStatusCode();

        if ( response.StatusCode == HttpStatusCode.NoContent ) {
            return default;
        }

        var student = await response.Content.ReadFromJsonAsync<PersonInfoExtended>(_jsonSerializerOptions);

        if ( student is not null ) {
            student.TakafolId = null;
            _cache.Set(cacheKey, student, TimeSpan.FromMinutes(1));
        }

        return student;
    }

    public async Task<PersonInfoExtended> SearchStudentAsync(string searchTerm) {
        if ( !searchTerm.HasValue() ) {
            _logger.LogDebug(Events.SearchStudent, "Invalid search term entered. returning null");
            return default;
        }

        _logger.LogDebug(Events.SearchStudent, "Searching student with search term '{term}'", searchTerm);
        var response = await _client.GetAsync($"api/person-info/student/by-multiplecode/{searchTerm}");
        _logger.LogDebug(Events.SearchStudent, "Searching student with search term '{term}' request finished with status code {status}", searchTerm, response.StatusCode);
        response.EnsureSuccessStatusCode();

        if ( response.StatusCode == HttpStatusCode.NoContent ) {
            return default;
        }

        return await response.Content.ReadFromJsonAsync<PersonInfoExtended>(_jsonSerializerOptions);
    }

    public async Task<PersonInfoExtended> GetStudentWithDependantsAsync(string codm, bool onlyActiveDependants = false) {
        if ( !codm.HasValue() || !int.TryParse(codm, out var _) ) {
            _logger.LogDebug(Events.GetStudentWithDependants, "Invalid codm entered. returning null");
            return default;
        }

        #region Cache
        var cacheKey = $"ex-pinfo-deps-{codm}-{onlyActiveDependants}";
        var cachedStudent = _cache.Get<PersonInfoExtended>(cacheKey);
        if ( cachedStudent is not null ) {
            _logger.LogDebug(Events.GetStudentWithDependants, "Reading student info with dependants with codm {codm} from cache", codm);
            return cachedStudent;
        }
        #endregion

        _logger.LogDebug(Events.GetStudentWithDependants, "Getting student info with dependants with codm {codm}", codm);
        var response = await _client.GetAsync($"api/person-info/student-with-dependants?codm={codm}&isActivedOnly={onlyActiveDependants.ToString().ToLower()}");
        _logger.LogDebug(Events.GetStudentWithDependants, "Getting student info with dependants with codm {codm} request finished with status code {status}", codm, response.StatusCode);
        response.EnsureSuccessStatusCode();

        if ( response.StatusCode == HttpStatusCode.NoContent ) {
            return default;
        }

        var student = await response.Content.ReadFromJsonAsync<PersonInfoExtended>(_jsonSerializerOptions);

        if ( student is not null ) {
            student.TakafolId = null;
            _cache.Set(cacheKey, student, TimeSpan.FromMinutes(1));
        }

        return student;
    }

    public async Task<List<PersonInfoExtended>> GetDependantsAsync(string codm, bool onlyActiveDependants = false) {
        return (await GetStudentWithDependantsAsync(codm, onlyActiveDependants))?.Dependants;
    }

    public async Task<List<PersonInfoExtended>> GetStudentGroupInfoAsync(IEnumerable<string> codmList, int chunkSize = 1000) {
        var data = codmList?
            .Select(x => x.ToIntNullable(defaultValue: 0).Value)
            .Where(x => x > 0)
            .Distinct()
            .ToArray() ?? [];
        if ( data.Length == 0 ) {
            _logger.LogDebug(Events.GetStudentGroupInfo, "No valid codm entered. Returning empty list");
            return [];
        }

        if ( chunkSize <= 0 ) {
            throw new ArgumentException($"'{chunkSize}' is invalid for chunk size", nameof(chunkSize));
        }

        var students = new ConcurrentBag<PersonInfoExtended>();
        var parallelOptions = new ParallelOptions {
            MaxDegreeOfParallelism = 8
        };

        await Parallel.ForEachAsync(data.Chunk(chunkSize), parallelOptions, async (chunk, cancellationToken) => {
            _logger.LogDebug(Events.GetStudentGroupInfo, "Getting student group info for chunk: {chunk}", chunk);

            var response = await _client.PostAsync($"api/person-info/student-group/", chunk.ToStringContent(_jsonSerializerOptions), cancellationToken);

            _logger.LogDebug(Events.GetStudentGroupInfo, "Getting student group info (first chunk item: {item}) finished with status code {status}", chunk[0], response.StatusCode);

            if ( response.StatusCode == HttpStatusCode.OK ) {
                var newStudents = await response.Content.ReadFromJsonAsync<List<PersonInfoExtended>>(_jsonSerializerOptions, cancellationToken: cancellationToken);
                foreach ( var newStudent in newStudents ) {
                    students.Add(newStudent);
                }
            } else if ( response.StatusCode == HttpStatusCode.NoContent ) {

            } else {
                throw new ExternalDataServiceException("Error in getting student group info");
            }
        });

        _logger.LogDebug(Events.GetStudentGroupInfo, "Getting student group info returned {count} students total", students.Count);

        return [.. students];
    }

    public async Task<List<PersonInfoExtended>> GetDependantsGroupInfoAsync(IEnumerable<int> takafolIds, int chunkSize = 1000) {
        var data = takafolIds.Where(x => x > 0).Distinct().ToArray();
        if ( data.Length == 0 ) {
            _logger.LogDebug(Events.GetDependantsGroupInfo, "No valid takafolId entered. Returning empty list");
            return [];
        }

        if ( chunkSize <= 0 ) {
            throw new ArgumentException($"'{chunkSize}' is invalid for chunk size", nameof(chunkSize));
        }

        var dependants = new ConcurrentBag<PersonInfoExtended>();
        var parallelOptions = new ParallelOptions {
            MaxDegreeOfParallelism = 8
        };

        await Parallel.ForEachAsync(data.Chunk(chunkSize), parallelOptions, async (chunk, cancellationToken) => {
            _logger.LogDebug(Events.GetDependantsGroupInfo, "Getting dependants group info for chunk: {chunk}", chunk);

            var response = await _client.GetAsync($"api/person-info/takafol-list-group?takafolIds={string.Join(',', chunk)}", cancellationToken);

            _logger.LogDebug(Events.GetDependantsGroupInfo, "Getting dependants group info (first chunk item: {item}) finished with status code {status}", chunk[0], response.StatusCode);

            if ( response.StatusCode == HttpStatusCode.OK ) {
                var newDependants = await response.Content.ReadFromJsonAsync<List<PersonInfoExtended>>(_jsonSerializerOptions, cancellationToken: cancellationToken);
                foreach ( var newDependant in newDependants ) {
                    dependants.Add(newDependant);
                }
            } else if ( response.StatusCode == HttpStatusCode.NoContent ) {

            } else {
                throw new ExternalDataServiceException("Error in getting dependants group info");
            }
        });

        _logger.LogDebug(Events.GetDependantsGroupInfo, "Getting dependants group info returned {count} dependants total", dependants.Count);

        return [.. dependants];
    }

    public async Task<List<PersonInfoExtended>> GetStudentsAndDependantsGroupInfoAsync(IEnumerable<string> codmList, IEnumerable<int> takafolIds, int chunkSize = 1000) {
        return [.. await GetStudentGroupInfoAsync(codmList, chunkSize), .. await GetDependantsGroupInfoAsync(takafolIds, chunkSize)];
    }

    public async Task<List<CsisBranch>> GetCsisBranchesAsync() {
        #region Cache
        var cachedBranches = _cache.Get<List<CsisBranch>>("ex-branches");
        if ( cachedBranches is not null && cachedBranches.Count > 0 ) {
            _logger.LogDebug(Events.GetCsisBranches, "Reading {count} csis branches from cache", cachedBranches.Count);
            return cachedBranches;
        }
        #endregion

        _logger.LogDebug(Events.GetCsisBranches, "Getting csis branches list");
        var response = await _client.GetAsync("api/base-info/branch-list");
        _logger.LogDebug(Events.GetCsisBranches, "Getting csis branches list request finished with status code {status}", response.StatusCode);
        response.EnsureSuccessStatusCode();

        if ( response.StatusCode == HttpStatusCode.NoContent ) {
            return default;
        }

        var branchesList = await response.Content.ReadFromJsonAsync<List<CsisBranch>>(_jsonSerializerOptions);

        if ( branchesList?.Count > 0 ) {
            _logger.LogDebug(Events.GetCsisBranches, "Setting {count} csis branches to cache", branchesList.Count);
            _cache.Set("ex-branches", branchesList, TimeSpan.FromHours(24));
        }

        return branchesList;
    }

    public async Task<string> GetBranchNameAsync(int branchId) {
        if ( branchId < 0 ) {
            return null;
        }

        return (await GetCsisBranchesAsync())?.FirstOrDefault(x => x.Code == branchId)?.Title;
    }

    public async Task<List<StudentSearchResult>> AdvancedSearchAsync(StudentSearchParam searchParams) {
        if ( searchParams is null ) {
            _logger.LogDebug(Events.AdvancedSearchStudent, "Invalid search params entered. returning null");
            return default;
        }

        _logger.LogDebug(Events.AdvancedSearchStudent, "Students advanced search with search params '{params}' started", JsonSerializer.Serialize(searchParams));
        var response = await _client.PostAsync("api/person-info/advanced-search", searchParams.ToStringContent());
        _logger.LogDebug(Events.AdvancedSearchStudent, "Students advanced search request finished with status code {status}", response.StatusCode);
        response.EnsureSuccessStatusCode();

        if ( response.StatusCode == HttpStatusCode.NoContent ) {
            return default;
        }

        return await response.Content.ReadFromJsonAsync<List<StudentSearchResult>>(_jsonSerializerOptions);
    }
}
