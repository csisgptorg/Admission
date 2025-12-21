/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Interfaces;

/// <summary>
/// Distributed queue service
/// </summary>
public interface IDistributedRequestQueue
{
    /// <summary>
    /// Wait request in the queue using default queue and max age of 60 seconds
    /// </summary>
    /// <param name="requestId">Unique request id to enqueue</param>
    /// <param name="timeout">Maximum wait time</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task WaitInQueueAsync(string requestId, TimeSpan timeout, CancellationToken cancellationToken);

    /// <summary>
    /// Wait request in the queue using specified queue and max age of 60 seconds
    /// </summary>
    /// <param name="requestId">Unique request id to enqueue</param>
    /// <param name="queueKey">Queue name</param>
    /// <param name="timeout">Maximum wait time</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task WaitInQueueAsync(string requestId, string queueKey, TimeSpan timeout, CancellationToken cancellationToken);

    /// <summary>
    /// Wait request in the queue using specified queue and max age
    /// </summary>
    /// <param name="requestId">Unique request id to enqueue</param>
    /// <param name="queueKey">Queue name</param>
    /// <param name="timeout">Maximum wait time</param>
    /// <param name="maxAge">Maximum age of request to keep in queue. If past and next request arrives, it is allowed</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task WaitInQueueAsync(string requestId, string queueKey, TimeSpan timeout, TimeSpan maxAge, CancellationToken cancellationToken);

    /// <summary>
    /// Wait request in the queue using default queue and specified max age
    /// </summary>
    /// <param name="requestId">Unique request id to enqueue</param>
    /// <param name="timeout">Maximum wait time</param>
    /// <param name="maxAge">Maximum age of request to keep in queue. If past and next request arrives, it is allowed</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task WaitInQueueAsync(string requestId, TimeSpan timeout, TimeSpan maxAge, CancellationToken cancellationToken);

    /// <summary>
    /// Dequeue a request from default queue. Must be called after processing all requests
    /// </summary>
    /// <param name="requestId">Unique request id to dequeue</param>
    /// <returns></returns>
    Task DequeueAsync(string requestId);

    /// <summary>
    /// Dequeue a request from specified queue. Must be called after processing all requests
    /// </summary>
    /// <param name="requestId">Unique request id to dequeue</param>
    /// <param name="queueKey">Queue name</param>
    /// <returns></returns>
    Task DequeueAsync(string requestId, string queueKey);

    /// <summary>
    /// Generate a random request id
    /// </summary>
    /// <returns></returns>
    string GenerateRequestId();
}
