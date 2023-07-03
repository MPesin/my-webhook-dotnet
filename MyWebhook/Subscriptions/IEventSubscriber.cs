using MyWebhook.Localizations;
using MyWebhook.Subscriptions.Models;

namespace MyWebhook.Subscriptions;

public interface IEventSubscriber
{
    Task<SubscriptionKey> SubscribeAsync(ListenerAddress address, ListenerLocal local);
    Task UnsubscribeAsync(SubscriptionKey key);
    Task AddListenerAsync(SubscriptionKey key, IEnumerable<IHookEvent> events);
    Task ClearListenerEventsAsync(SubscriptionKey key, IEnumerable<IHookEvent> events);
    Task <bool> ExistsAsync(SubscriptionKey key);
}