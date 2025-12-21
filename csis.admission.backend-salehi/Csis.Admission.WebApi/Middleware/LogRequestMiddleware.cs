using System.Diagnostics;
using Csis.Admission.Persistence;
using Csis.Admission.Domain.Entities;

namespace Csis.Admission.WebApi.Middleware;

/// <inheritdoc/>
public partial class LogRequestMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHttpContextAccessor _contextAccessor;

    /// <inheritdoc/>
    public IConfiguration Config;

    /// <inheritdoc/>
    public LogRequestMiddleware(RequestDelegate next, IHttpContextAccessor contextAccessor) {
        _next = next;
        _contextAccessor = contextAccessor;
    }

    /// <inheritdoc/>
    public async Task Invoke(HttpContext context, AppDbContext db) {

        context.Request.EnableBuffering();
        using var requestBodyReader = new StreamReader(context.Request.Body);
        var requestBody = await requestBodyReader.ReadToEndAsync();
        context.Request.Body.Position = 0;
        var requestLog=await CreateRequestLog(db,context, requestBody);

        var originalResponseBodyStream = context.Response.Body;
        using var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;
        await _next(context);
        responseBodyStream.Seek(0, SeekOrigin.Begin);
        using var responseBodyReader = new StreamReader(context.Response.Body);
        var responseBody = await responseBodyReader.ReadToEndAsync();
        responseBodyStream.Position = 0;
        await responseBodyStream.CopyToAsync(originalResponseBodyStream);
        _=await CreateResponseLog(db,context,responseBody,requestLog);
    }

    private TraceLog CreateTraceLog(HttpContext context,LogType type,string body=null,int? duration=null,int? statusCode=null) {
        var traceId = Activity.Current?.TraceId.ToString() ?? _contextAccessor.HttpContext?.TraceIdentifier;
        var url = $"({context.Request.Method}) {context.Request.Scheme}://{context.Request.Host}{context.Request.Path}";
        var authorization = context.Request.Headers.Authorization.ToString();
        var log = new TraceLog(traceId, url, data: body, type.ToString(),duration,statusCode);

        return log;
    }

    private async Task<TraceLog> CreateRequestLog(AppDbContext db,HttpContext context,string body) {
        var log = CreateTraceLog(context, LogType.Request,body:body);
        db.Set<TraceLog>().Add(log);
        await db.SaveChangesAsync();
        return log;
    }

    private async Task<TraceLog> CreateResponseLog(AppDbContext db, HttpContext context,string body, TraceLog requestLog) {
        requestLog.SetDuration();
        var responseLog = CreateTraceLog(context, LogType.Response,body,requestLog.Duration,context.Response.StatusCode);
        db.Set<TraceLog>().Add(responseLog);

        db.Set<TraceLog>().Update(requestLog);
        await db.SaveChangesAsync();
        return responseLog;
    }

    private enum LogType{
        Request,
        Response
    }
}
