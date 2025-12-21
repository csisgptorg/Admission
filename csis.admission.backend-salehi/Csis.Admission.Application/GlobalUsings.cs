/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

global using AutoMapper;
global using Csis.Abstractions.Exceptions;
global using Csis.Admission.Application.Common;
global using Csis.Admission.Application.Common.Interfaces;
global using Csis.Admission.Domain.Entities;
global using Csis.Admission.Domain.Enums;
global using Csis.Utilities.Extensions;
global using Csis.Utilities.Json;
global using FluentValidation;
global using MediatR;
global using Microsoft.Extensions.Logging;
global using System.Text.Json;
global using System.Text.Json.Serialization;
