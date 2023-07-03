using MyWebhook.Localizations;
using MyWebhook.Repos;
using MyWebhook.Subscriptions.Models;

namespace MyWebhook.Subscriptions;

internal class EventSubscriber : IEventSubscriber
{
    private readonly IEventListenersRepo _repo;

    public EventSubscriber(IEventListenersRepo repo)
    {
        _repo = repo;
    }

    public async Task<SubscriptionKey> SubscribeAsync(ListenerAddress address, ListenerLocal local)
    {
        var key = await _repo.FindKeyAsync(address);
        if (key == null)
        {
            key = new SubscriptionKey();
            await _repo.AddKeyAsync(key, address, local);
        }

        return key;
    }

    public async Task UnsubscribeAsync(SubscriptionKey key)
    {
        await _repo.DeleteAsync(key);
    }

    public async Task AddListenerAsync(SubscriptionKey key, IEnumerable<IHookEvent> events)
    {
        await _repo.AppendEventsAsync(key, events);
    }

    public async Task ClearListenerEventsAsync(SubscriptionKey key, IEnumerable<IHookEvent> events)
    {
        await _repo.RemoveEventsAsync(key, events);
    }

    public Task<bool> ExistsAsync(SubscriptionKey key)
    {
        return _repo.KeyExistsAsync(key);
    }
}