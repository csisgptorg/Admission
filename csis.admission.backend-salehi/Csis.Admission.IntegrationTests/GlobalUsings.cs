/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

global using Csis.Abstractions.Exceptions;
global using Csis.Paging;
global using Csis.Admission.Domain.Entities;
global using Csis.Admission.Domain.Enums;
global using Csis.Admission.Domain.Enums.Common;
global using Csis.Utilities;
global using Csis.Utilities.Extensions;
global using FluentAssertions;
global using FluentValidation.TestHelper;
global using Moq;
global using NUnit.Framework;
global using static Csis.Admission.IntegrationTests.Testing;
