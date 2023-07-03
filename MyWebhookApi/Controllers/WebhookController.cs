using Microsoft.AspNetCore.Mvc;
using MyWebhook.Localizations;
using MyWebhook.Subscriptions.Models;
using MyWebhookApi.DataModels;
using MyWebhookApi.Webhooks;

namespace MyWebhookApi.Controllers;

[Route("api/v1/webhooks")]
public class WebhookController : Controller
{
    private readonly IWebhookManager _webhookManager;

    public WebhookController(IWebhookManager eventSubscriber)
    {
        _webhookManager = eventSubscriber ?? throw new ArgumentNullException(nameof(eventSubscriber));
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Subscribe(ListenerData data)
    {
        try
        {
            var key = await _webhookManager.SubscribeAsync(data);
            return Ok(new SubscriptionResponse {SubscriptionKey = key.Key});
        }
        catch (ArgumentException e)
        {
            return BadRequest(e);
        }
    }

    private ListenerLocal CreateLocal(string localCode)
    {
        ListenerLocal local;
        try
        {
            local = ListenerLocal.Create(localCode);
        }
        catch (ArgumentException e)
        {
            _logService.LogWarning(this,
                $"Failed to read localization '{localCode}' in request '{Request.RequestUri}': {e.Message}");
            throw;
        }

        return local;
    }

    [HttpPost]
    [Route("add/{id}")]
    public async Task<IHttpActionResult> AddEventListeners(string id, EventsCollectionData data)
    {
        try
        {
            await _webhookManager.AddListenerAsync(CreateKey(id), data.Events);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpPost]
    [Route("remove/{id}")]
    public async Task<IHttpActionResult> RemoveEventListeners(string id, EventsCollectionData data)
    {
        try
        {
            await _webhookManager.ClearListenerEventsAsync(CreateKey(id), data.Events);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete]
    public async Task<IHttpActionResult> Unsubscribe(string id)
    {
        try
        {
            await _webhookManager.UnsubscribeAsync(CreateKey(id));
            return Ok(id);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    private SubscriptionKey CreateKey(string id) => new(Guid.Parse(id));
}