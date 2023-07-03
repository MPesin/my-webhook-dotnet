using MyWebhook.Localizations;
using MyWebhook.Subscriptions.Models;

namespace MyWebhook.Repos;

public interface IEventListenersRepo
{
    Task AddKeyAsync(SubscriptionKey key, ListenerAddress address, ListenerLocal local);
    Task<bool> KeyExistsAsync(SubscriptionKey key);
    Task DeleteAsync(SubscriptionKey key);
    Task AppendEventsAsync(SubscriptionKey key, IEnumerable<IHookEvent> events);
    Task RemoveEventsAsync(SubscriptionKey key, IEnumerable<IHookEvent> events);
    Task<IEnumerable<ListenerAddress>> GetAddressesAsync(IHookEvent hookEvent);
    Task<SubscriptionKey?> FindKeyAsync(ListenerAddress address);
    Task<LocalLanguage> GetLocalCodeAsync(SubscriptionKey key);
}