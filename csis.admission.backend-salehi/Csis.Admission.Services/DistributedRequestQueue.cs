/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Security.Cryptography;

namespace Csis.Admission.Services;
internal sealed class DistributedRequestQueue : IDistributedRequestQueue
{
    private readonly IConnectionMultiplexer _redis;
    private readonly ILogger<DistributedRequestQueue> _logger;
    private readonly string _defaultQueueKey;
    private readonly string _hashKey;
    private readonly TimeSpan _pollInterval = TimeSpan.FromMilliseconds(200);
    private readonly int _defaultMaxAgeSeconds = 60;

    public DistributedRequestQueue(IConnectionMultiplexer redis, ILogger<DistributedRequestQueue> logger) {
        _redis = redis;
        _logger = logger;
        _defaultQueueKey = $"{typeof(DistributedRequestQueue).Assembly.GetName().Name.Replace(".Services", "")}_requests_queue";
        _hashKey = $"{_defaultQueueKey}_timestamps";
    }

    public async Task DequeueAsync(string requestId) {
        await DequeueRequestAsync(requestId, GetKey(null));
    }

    public async Task DequeueAsync(string requestId, string queueKey) {
        await DequeueRequestAsync(requestId, GetKey(queueKey));
    }

    private async Task DequeueRequestAsync(string requestId, string queueKey) {
        var db = _redis.GetDatabase();

        var transaction = db.CreateTransaction();
        _ = transaction.ListRemoveAsync(queueKey, requestId);
        _ = transaction.HashDeleteAsync(_hashKey, requestId);
        var committed = await transaction.ExecuteAsync();

        if ( committed ) {
            _logger.LogDebug("Dequeued {requestId} from {queueKey}", requestId, queueKey);
        } else {
            throw new Exception("Could not dequeue value from redis");
        }
    }

    public string GenerateRequestId() {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(16));
    }

    public async Task WaitInQueueAsync(string requestId, TimeSpan timeout, CancellationToken cancellationToken) {
        await QueueRequestAsync(requestId, GetKey(null), timeout, TimeSpan.FromSeconds(_defaultMaxAgeSeconds), cancellationToken);
    }

    public async Task WaitInQueueAsync(string requestId, string queueKey, TimeSpan timeout, CancellationToken cancellationToken) {
        await QueueRequestAsync(requestId, GetKey(queueKey), timeout, TimeSpan.FromSeconds(_defaultMaxAgeSeconds), cancellationToken);
    }

    public async Task WaitInQueueAsync(string requestId, string queueKey, TimeSpan timeout, TimeSpan maxAge, CancellationToken cancellationToken) {
        await QueueRequestAsync(requestId, GetKey(queueKey), timeout, maxAge, cancellationToken);
    }

    public async Task WaitInQueueAsync(string requestId, TimeSpan timeout, TimeSpan maxAge, CancellationToken cancellationToken) {
        await QueueRequestAsync(requestId, GetKey(null), timeout, maxAge, cancellationToken);
    }

    private async Task QueueRequestAsync(string requestId, string queueKey, TimeSpan timeout, TimeSpan maxAge, CancellationToken cancellationToken) {
        await TryRemoveStaleHeadAsync(queueKey);

        var db = _redis.GetDatabase();
        var timestamp = DateTimeOffset.UtcNow.Add(maxAge).ToUnixTimeSeconds();

        var transaction = db.CreateTransaction();
        _ = transaction.ListRightPushAsync(queueKey, requestId);
        _ = transaction.HashSetAsync(_hashKey, requestId, timestamp);
        var committed = await transaction.ExecuteAsync();

        if ( committed ) {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            while ( stopwatch.Elapsed < timeout && !cancellationToken.IsCancellationRequested ) {
                var front = await db.ListGetByIndexAsync(queueKey, 0);
                if ( front == requestId ) {
                    _logger.LogDebug("It's turn. finish waiting");
                    return;
                }

                _logger.LogDebug("Still waiting");
                await Task.Delay(_pollInterval, cancellationToken);
            }

            throw new TimeoutException("Timed out waiting in queue.");
        } else {
            throw new Exception("Could not push value to redis queue");
        }
    }

    private string GetKey(string requestedKey) {
        if ( !string.IsNullOrWhiteSpace(requestedKey) ) {
            return $"{_defaultQueueKey}_{requestedKey}";
        }

        return _defaultQueueKey;
    }

    private async Task TryRemoveStaleHeadAsync(string queueKey) {
        var db = _redis.GetDatabase();
        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        var index = 0L;
        while ( true ) {
            var requestId = await db.ListGetByIndexAsync(queueKey, index);
            if ( !requestId.HasValue ) {
                break;
            }

            var timestampString = await db.HashGetAsync(_hashKey, requestId);
            if ( !timestampString.HasValue ) {
                await db.ListRemoveAsync(queueKey, requestId);
            } else if ( timestampString.HasValue && long.TryParse(timestampString, out var timestamp) ) {
                if ( now > timestamp ) {
                    var transaction = db.CreateTransaction();
                    _ = transaction.ListRemoveAsync(queueKey, requestId);
                    _ = transaction.HashDeleteAsync(_hashKey, requestId);
                    var committed = await transaction.ExecuteAsync();

                    if ( !committed ) {
                        _logger.LogDebug("Could not commit stale item remove for request id {requestId}", requestId);
                    }
                }
            }

            index++;
        }
    }
}
