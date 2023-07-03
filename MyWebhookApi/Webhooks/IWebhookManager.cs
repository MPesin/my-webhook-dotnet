using MyWebhook.Subscriptions.Models;
using MyWebhookApi.DataModels;

namespace MyWebhookApi.Webhooks;

public interface IWebhookManager
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    /// <exception cref=""></exception>
    Task<SubscriptionKey> SubscribeAsync(ListenerData data);
}