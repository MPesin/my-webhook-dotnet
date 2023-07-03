using MyWebhook.Subscriptions.Models;

namespace MyWebhookApi.DataModels;

public class EventData
{
    public IHookEvent HookEvent { get; set; }
    public object Data { get; set; }
}