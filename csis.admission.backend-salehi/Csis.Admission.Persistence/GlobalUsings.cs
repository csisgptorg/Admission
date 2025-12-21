/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

global using Csis.Paging;
global using Csis.Admission.Application.Common.Configuration;
global using Csis.Admission.Application.Common.Interfaces;
global using Csis.Admission.Application.Common.Interfaces.Repositories;
global using Csis.Admission.Application.Enums;
global using Csis.Admission.Domain.Entities;
global using Csis.Admission.Domain.Enums;
global using Csis.Admission.Persistence;
global using Csis.Admission.Persistence.Extensions;
global using Csis.Admission.Persistence.Repositories;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.EntityFrameworkCore.Migrations;
global using Microsoft.Extensions.Options;
