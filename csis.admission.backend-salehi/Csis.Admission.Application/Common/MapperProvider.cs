/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common;

/// <summary>
/// Provide static instance of <see cref="IMapper"/>
/// </summary>
public static class MapperProvider
{
    private static IMapper _mapper;

    /// <summary>
    /// Get instance of <see cref="IMapper"/>
    /// </summary>
    public static IMapper Mapper => _mapper;

    /// <summary>
    /// Get mapping configuration provider
    /// </summary>
    public static IConfigurationProvider MapperConfiguration => _mapper.ConfigurationProvider;

    /// <summary>
    /// Initialize mapper instance
    /// </summary>
    /// <param name="mapper">Mapper instance</param>
    /// <exception cref="Exception"></exception>
    public static void Initialize(IMapper mapper) {
        _mapper = mapper;
    }
}
