/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Abstractions.Exceptions;
using Csis.Admission.Application.Common;
using Csis.Admission.Application.Common.Configuration;
using Csis.Admission.Application.Common.Interfaces;
using Csis.Admission.Application.Common.Models;
using Csis.Shared.Kernel.Public.Models.Employee;
using Csis.Utilities.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Csis.Admission.Services;
internal sealed partial class EmployeeDataService : IEmployeeDataService
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly EmployeeDataServiceOptions _options;
    private readonly IMemoryCacheService _cache;
#pragma warning disable IDE0052 // Remove unread private members
    private readonly IConfiguration _configuration;
#pragma warning restore IDE0052 // Remove unread private members
    private readonly ILogger<EmployeeDataService> _logger;

    public EmployeeDataService(
        HttpClient client,
        IOptions<EmployeeDataServiceOptions> options,
        IMemoryCacheService cache,
        IConfiguration configuration,
        ILogger<EmployeeDataService> logger) {
        _client = client;
        _options = options.Value;
        if ( !_options.BaseUrl.HasValue() || !Uri.TryCreate(_options.BaseUrl, UriKind.Absolute, out var baseUri) ) {
            throw new ArgumentException("Invalid base url provided for employee data service", nameof(options));
        }

        if ( !_options.ApiKey.HasValue() ) {
            throw new ArgumentException("Invalid api key provided for employee data service", nameof(options));
        }

        _client.DefaultRequestHeaders.Add("authorization", _options.ApiKey);
        _client.BaseAddress = baseUri;
        _client.Timeout = TimeSpan.FromSeconds(_options.TimeoutInSeconds);
        _jsonSerializerOptions = new JsonSerializerOptions {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        _cache = cache;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<EmployeeInfo> GetEmployeeInfoAsync(int personnelId, bool activeOnly = false) {
        _logger.LogDebug(Events.GetEmployeeInfo, "Getting employee info with personnel id {personnelId} and activeOnly {activeOnly}", personnelId, activeOnly);

        var cacheKey = $"ex-empinfo-{personnelId}-{activeOnly}";
        var cachedEmployee = _cache.Get<EmployeeInfo>(cacheKey);
        if ( cachedEmployee is not null ) {
            _logger.LogDebug(Events.GetEmployeeInfo, "Reading employee info with personnelId {personnelId} from cache", personnelId);
            return cachedEmployee;
        }

        var response = await _client.GetAsync($"api/employee/{personnelId}?activeOnly={activeOnly}");

        _logger.LogDebug(Events.GetEmployeeInfo, "Getting employee info with personnel id {personnelId} finished with status code {status}", personnelId, (int) response.StatusCode);
        response.EnsureSuccessStatusCode();

        if ( response.StatusCode == HttpStatusCode.NoContent ) {
            return default;
        }

        var employee = await response.Content.ReadFromJsonAsync<EmployeeInfo>();

        if ( employee is not null ) {
            _cache.Set(cacheKey, employee, new CacheOptions {
                AbsoluteExpirationSeconds = 60
            });
        }

        return employee;
    }

    public async Task<List<JobPositionModel>> GetAllJobPositionsAsync() {
        _logger.LogDebug(Events.GetAllJobPositions, "Getting all job positions");

        var cacheKey = "all-job-positions";
        var cachedJobPositions = _cache.Get<List<JobPositionModel>>(cacheKey);
        if ( cachedJobPositions is not null ) {
            _logger.LogDebug(Events.GetAllJobPositions, "Reading job positions from cache");
            return cachedJobPositions;
        }

        _logger.LogDebug(Events.GetAllJobPositions, "No cache found. Getting all job positions from employee service");
        var response = await _client.GetAsync("api/employee/employeeStaffIDs");
        _logger.LogDebug(Events.GetAllJobPositions, "Getting all job positions finished with status code {status}", (int) response.StatusCode);
        response.EnsureSuccessStatusCode();

        if ( response.StatusCode == HttpStatusCode.NoContent ) {
            return default;
        }

        var jobPositions = (await response.Content.ReadFromJsonAsync<List<JobPositionModel>>())
            .DistinctBy(x => x.Id).ToList();

        if ( jobPositions is not null ) {
            _cache.Set(cacheKey, jobPositions, new CacheOptions {
                AbsoluteExpirationSeconds = 600
            });
        }

        return jobPositions;
    }

    public async Task<List<JobPositionModel>> GetJobPositionsByPersonnelIdAsync(int personnelId) {
        if ( personnelId <= 0 ) {
            _logger.LogDebug(Events.GetJobPositionsByPersonnelId, "Invalid personnel id {personnelId}", personnelId);
            return [];
        }

        _logger.LogDebug("Getting job positions for personnel id {personnelId}", personnelId);
        var cacheKey = $"job-positions_{personnelId}";
        var cachedJobPositions = _cache.Get<List<JobPositionModel>>(cacheKey);
        if ( cachedJobPositions is not null ) {
            _logger.LogDebug(Events.GetJobPositionsByPersonnelId, "Reading job positions for personnel Id {personnelId} from cache", personnelId);
            return cachedJobPositions;
        }

        _logger.LogDebug(Events.GetJobPositionsByPersonnelId, "Getting job positions from employee service for personnel id {personnelId}", personnelId);
        var response = await _client.GetAsync($"api/employee/employeeStaffIDs?personnelNo={personnelId}");
        _logger.LogDebug("Getting job positions for personnel id {personnelId} finished with status code {status}", personnelId, (int) response.StatusCode);

        if ( response.StatusCode == HttpStatusCode.NoContent ) {
            return default;
        } else if ( response.StatusCode == HttpStatusCode.NotFound ) {
            return [];
        }

        var jobPositions = (await response.Content.ReadFromJsonAsync<List<JobPositionModel>>())
            .DistinctBy(x => x.Id).ToList();

        if ( jobPositions is not null ) {
            _cache.Set(cacheKey, jobPositions, new CacheOptions {
                AbsoluteExpirationSeconds = 60
            });
        }

        return jobPositions;
    }

    public async Task<List<EmployeeContactInfo>> GetEmployeeContactInfoAsync(List<int> personnelIds) {
        _logger.LogDebug(Events.GetEmployeeContactInfo, "Getting employee contact info");

        var response = await _client.PostAsync("api/employee/get-contact-info", personnelIds.Distinct().Select(x => x.ToString()).ToArray().ToStringContent(_jsonSerializerOptions));
        _logger.LogDebug(Events.GetEmployeeContactInfo, "Getting employee contact info finished with status code {status}", (int) response.StatusCode);

        if ( response.StatusCode == HttpStatusCode.NoContent ) {
            return default;
        }

        return await response.Content.ReadFromJsonAsync<List<EmployeeContactInfo>>();
    }

    public async Task<List<EmployeeInfo>> GetEmployeesGroupInfoAsync(IEnumerable<int> personnelIds, int chunkSize = 1000) {
        var data = personnelIds?
            .Where(x => x > 0)
            .Distinct()
            .Select(x => x.ToString())
            .ToArray() ?? [];
        if ( data.Length == 0 ) {
            _logger.LogDebug(Events.GetEmployeesGroupInfo, "No valid personnel id entered. Returning empty list");
            return [];
        }

        if ( chunkSize <= 0 ) {
            throw new ArgumentException($"'{chunkSize}' is invalid for chunk size", nameof(chunkSize));
        }

        var employees = new ConcurrentBag<EmployeeInfo>();
        var parallelOptions = new ParallelOptions {
            MaxDegreeOfParallelism = 8
        };

        await Parallel.ForEachAsync(data.Chunk(chunkSize), parallelOptions, async (chunk, cancellationToken) => {
            _logger.LogDebug(Events.GetEmployeesGroupInfo, "Getting employee group info for chunk: {chunk}", chunk);

            var response = await _client.PostAsync($"api/employee/employee-group/", chunk.ToStringContent(_jsonSerializerOptions), cancellationToken);

            _logger.LogDebug(Events.GetEmployeesGroupInfo, "Getting employee group info (first chunk item: {item}) finished with status code {status}", chunk[0], response.StatusCode);

            if ( response.StatusCode == HttpStatusCode.OK ) {
                var newEmployees = await response.Content.ReadFromJsonAsync<List<EmployeeInfo>>(_jsonSerializerOptions, cancellationToken: cancellationToken);
                foreach ( var newEmployee in newEmployees ) {
                    employees.Add(newEmployee);
                }
            } else if ( response.StatusCode is HttpStatusCode.NoContent or HttpStatusCode.NotFound ) {

            } else {
                throw new ExternalDataServiceException("Error in getting employee group info");
            }
        });

        _logger.LogDebug(Events.GetEmployeesGroupInfo, "Getting employee group info returned {count} employees total", employees.Count);

        return [.. employees];
    }

    public async Task<List<EmployeePersonInfo>> GetEmployeesAndDependantsGroupInfoAsync(IEnumerable<int> personnelIds, IEnumerable<int> takafolIds, int chunkSize = 1000) {
        var employees = (await GetEmployeesGroupInfoAsync(personnelIds, chunkSize))
            .Select(x => new EmployeePersonInfo {
                PersonnelId = x.PersonnelId,
                TakafolId = null,
                FirstName = x.FirstName,
                LastName = x.LastName,
                NationalId = x.NationalId,
                Mobile = x.Mobile,
                //BirthDate = x.BirthDate,
                BranchId = x.BranchId,
                BranchTitle = x.BranchTitle,
                //Gender = (byte) newDependant.Gender,
                //GenderTitle = newDependant.Gender == 1 ? "مرد" : "زن",
                RelationId = Relation.Householder,
                Relation = "سرپرست"
            });

        return [.. employees, .. await GetDependantsGroupInfoAsync(takafolIds, chunkSize)];
    }

    public async Task<List<EmployeePersonInfo>> GetDependantsGroupInfoAsync(IEnumerable<int> takafolIds, int chunkSize = 1000) {
        var data = takafolIds?
            .Where(x => x > 0)
            .Distinct()
            .ToArray() ?? [];
        if ( data.Length == 0 ) {
            _logger.LogDebug(Events.GetEmployeeDependantsGroupInfo, "No valid takafol id entered. Returning empty list");
            return [];
        }

        if ( chunkSize <= 0 ) {
            throw new ArgumentException($"'{chunkSize}' is invalid for chunk size", nameof(chunkSize));
        }

        var dependants = new ConcurrentBag<EmployeePersonInfo>();
        var parallelOptions = new ParallelOptions {
            MaxDegreeOfParallelism = 8
        };

        await Parallel.ForEachAsync(data.Chunk(chunkSize), parallelOptions, async (chunk, cancellationToken) => {
            _logger.LogDebug(Events.GetEmployeeDependantsGroupInfo, "Getting dependant group info for chunk: {chunk}", chunk);

            var response = await _client.PostAsync($"api/employee/dependant-group/", chunk.ToStringContent(_jsonSerializerOptions), cancellationToken);

            _logger.LogDebug(Events.GetEmployeeDependantsGroupInfo, "Getting dependant group info (first chunk item: {item}) finished with status code {status}", chunk[0], response.StatusCode);

            if ( response.StatusCode == HttpStatusCode.OK ) {
                var newDependants = await response.Content.ReadFromJsonAsync<List<DependentInfo>>(_jsonSerializerOptions, cancellationToken: cancellationToken);
                foreach ( var newDependant in newDependants ) {
                    dependants.Add(new EmployeePersonInfo {
                        FirstName = newDependant.FirstName,
                        LastName = newDependant.LastName,
                        BirthDate = newDependant.BirthDate,
                        Gender = (byte) newDependant.Gender,
                        GenderTitle = newDependant.Gender == 1 ? "مرد" : "زن",
                        //BranchId = newDependant.BranchId,
                        //Mobile = newDependant.Mobile,
                        NationalId = newDependant.NationalCode,
                        PersonnelId = newDependant.PersonnelNo.ToInt(),
                        RelationId = (Relation) newDependant.RelationID,
                        Relation = ((Relation) newDependant.RelationID).GetEnumDisplayName(),
                        TakafolId = newDependant.TakaffolID
                    });
                }
            } else if ( response.StatusCode == HttpStatusCode.NoContent ) {

            } else {
                throw new ExternalDataServiceException("Error in getting dependant group info");
            }
        });

        _logger.LogDebug(Events.GetEmployeeDependantsGroupInfo, "Getting dependant group info returned {count} dependants total", dependants.Count);

        return [.. dependants];
    }
}
