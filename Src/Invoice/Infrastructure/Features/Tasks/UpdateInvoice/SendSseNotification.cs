using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.UpdateInvoice;
using CsharpKioskDemoDotnet.Shared;
using CsharpKioskDemoDotnet.Shared.Sse;
using Lib.AspNetCore.ServerSentEvents;

namespace CsharpKioskDemoDotnet.Invoice.Infrastructure.Features.Tasks.UpdateInvoice;

public class SendSseNotification : IAfterInvoiceUpdate
{
    private readonly INotificationsServerSentEventsService _serverSentEventsService;
    private readonly IObjectToJsonConverter _objectToJsonConverter;

    public SendSseNotification(
        INotificationsServerSentEventsService serverSentEventsService,
        IObjectToJsonConverter objectToJsonConverter
    )
    {
        _serverSentEventsService = serverSentEventsService;
        _objectToJsonConverter = objectToJsonConverter;
    }

    public void Execute(
        Invoice.Domain.Invoice invoice,
        string? eventName
    )
    {
        var eventType = GetEventMessageTypeFromEventName(eventName);
        var eventMessage = GetEventMessageFromEventName(invoice.BitPayId, eventName);
        var serverSentEvent = GetEvent(invoice, "invoice/update", eventType, eventMessage);

        _serverSentEventsService.SendEventAsync(serverSentEvent);
    }

    private string? GetEventMessageFromEventName(
        string invoiceBitPayId,
        string? eventName
    )
    {
        if (eventName == null)
        {
            return null;
        }

        return eventName switch
        {
            "invoice_paidInFull" => $"Invoice {invoiceBitPayId} has been paid in full.",
            "invoice_expired" => $"Invoice {invoiceBitPayId} has expired.",
            "invoice_confirmed" => $"Invoice {invoiceBitPayId} has been confirmed.",
            "invoice_completed" => $"Invoice {invoiceBitPayId} is complete.",
            "invoice_failedToConfirm" => $"Invoice {invoiceBitPayId} has failed to confirm.",
            "invoice_declined" => $"Invoice {invoiceBitPayId} has been declined.",
            _ => null
        };
    }

    private UpdateInvoiceEventType? GetEventMessageTypeFromEventName(string? eventName)
    {
        if (eventName == null)
        {
            return null;
        }

        return eventName switch
        {
            "invoice_paidInFull" => UpdateInvoiceEventType.Success,
            "invoice_confirmed" => UpdateInvoiceEventType.Success,
            "invoice_completed" => UpdateInvoiceEventType.Success,
            "invoice_expired" => UpdateInvoiceEventType.Error,
            "invoice_failedToConfirm" => UpdateInvoiceEventType.Error,
            "invoice_declined" => UpdateInvoiceEventType.Error,
            _ => null
        };
    }

    private ServerSentEvent GetEvent(
        Invoice.Domain.Invoice invoice,
        string eventName,
        UpdateInvoiceEventType? eventType,
        string? eventMessage
    )
    {
        return new ServerSentEvent
        {
            Id = DateTime.Now.ToFileTimeUtc().ToString(),
            Type = eventName,
            Data = GetData(invoice, eventType, eventMessage)
        };
    }

    private IList<string> GetData(
        Invoice.Domain.Invoice invoice,
        UpdateInvoiceEventType? eventType,
        string? eventMessage
    )
    {
        var data = new Dictionary<string, object?>
        {
            { "invoiceId", invoice.Id },
            { "status", invoice.Status },
            { "eventType", eventType != null ? eventType.ToString()!.ToLower() : null },
            { "eventMessage", eventType != null ? eventMessage : null }
        };

        return new List<string> { _objectToJsonConverter.Execute(data)! };
    }
}