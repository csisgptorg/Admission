/* =========================================================
 * This file is part of the Csis.Template
 * Do not make any modifications on this file
 * All changes will be rejected in code review sessions
 * ========================================================= */

namespace Csis.Admission.Application.Common.Interfaces;

/// <summary>
/// Distributed publisher/subscriber service
/// </summary>
public interface IDistributedPubSubService
{
    /// <summary>
    /// Subscribe to perform some operation when a message received on <paramref name="channelName"/>
    /// </summary>
    /// <param name="channelName">Channel name</param>
    /// <param name="handler">The handler to invoke when a message is received on <paramref name="channelName"/>. The input of action is the message.</param>
    void Subscribe(string channelName, Action<string> handler);

    /// <summary>
    /// Subscribe to perform some operation when a message received on <paramref name="channelName"/>
    /// </summary>
    /// <param name="channelName">Channel name</param>
    /// <param name="handler">The handler to invoke when a message is received on <paramref name="channelName"/>. The input of action is the message.</param>
    Task SubscribeAsync(string channelName, Func<string, Task> handler);

    /// <summary>
    /// Publish a message on <paramref name="channelName"/>
    /// </summary>
    /// <param name="channelName">Channel name</param>
    /// <param name="message">Message</param>
    void Publish(string channelName, string message);

    /// <summary>
    /// Publish a message on <paramref name="channelName"/>
    /// </summary>
    /// <param name="channelName">Channel name</param>
    /// <param name="message">Message</param>
    Task PublishAsync(string channelName, string message);
}
