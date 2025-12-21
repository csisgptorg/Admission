/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

using Csis.Admission.Application.Common.Interfaces;
using StackExchange.Redis;

namespace Csis.Admission.Services;
internal sealed class DistributedPubSubService(IConnectionMultiplexer redis) : IDistributedPubSubService
{
    public void Publish(string channelName, string message) {
        redis.GetSubscriber().Publish(new RedisChannel(channelName, RedisChannel.PatternMode.Literal), new RedisValue(message));
    }

    public async Task PublishAsync(string channelName, string message) {
        await redis.GetSubscriber().PublishAsync(new RedisChannel(channelName, RedisChannel.PatternMode.Literal), new RedisValue(message));
    }

    public void Subscribe(string channelName, Action<string> handler) {
        ArgumentNullException.ThrowIfNull(handler);

        redis.GetSubscriber().Subscribe(new RedisChannel(channelName, RedisChannel.PatternMode.Literal), (channel, message) => {
            handler(message.ToString());
        });
    }

    public async Task SubscribeAsync(string channelName, Func<string, Task> handler) {
        ArgumentNullException.ThrowIfNull(handler);

        await redis.GetSubscriber().SubscribeAsync(new RedisChannel(channelName, RedisChannel.PatternMode.Literal), async (channel, message) => {
            await handler(message.ToString());
        });
    }
}
