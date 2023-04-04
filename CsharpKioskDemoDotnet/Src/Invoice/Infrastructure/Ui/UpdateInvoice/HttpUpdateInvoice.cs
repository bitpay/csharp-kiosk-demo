using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.UpdateInvoice;
using CsharpKioskDemoDotnet.Invoice.Domain;
using CsharpKioskDemoDotnet.Shared;
using Microsoft.AspNetCore.Mvc;

namespace CsharpKioskDemoDotnet.Invoice.Infrastructure.Ui.UpdateInvoice;

public class HttpUpdateInvoice : Controller
{
    private readonly Application.Features.Tasks.UpdateInvoice.UpdateInvoice _updateInvoice;
    private readonly IJsonToObjectConverter _jsonToObjectConverter;

    public HttpUpdateInvoice(
        Application.Features.Tasks.UpdateInvoice.UpdateInvoice updateInvoice,
        IJsonToObjectConverter jsonToObjectConverter
    )
    {
        _updateInvoice = updateInvoice;
        _jsonToObjectConverter = jsonToObjectConverter;
    }

    // POST: invoice/{uuid}
    [HttpPost("invoices/{uuid}")]
    public ActionResult Execute(
        string uuid,
        [FromBody] Dictionary<string, object> body
    )
    {
        try
        {
            _updateInvoice.Execute(uuid, GetData(body));

            return Ok();
        }
        catch (ValidationInvoiceUpdateDataFailed exception)
        {
            return BadRequest(exception.Errors);
        }
        catch (InvoiceNotFound)
        {
            return NotFound();
        }
    }

    private Dictionary<string, object?> GetData(
        Dictionary<string, object> body
    )
    {
        var data = _jsonToObjectConverter.Execute<Dictionary<string, object?>>(
            body["data"].ToString() ?? throw new InvalidOperationException()
        );

        var eventData = _jsonToObjectConverter.Execute<Dictionary<string, object?>>(
            body["event"].ToString() ?? null
        );

        if (eventData != null && eventData.TryGetValue("name", out var value))
        {
            data!.Add("eventName", value);
        }

        return data!;
    }
}