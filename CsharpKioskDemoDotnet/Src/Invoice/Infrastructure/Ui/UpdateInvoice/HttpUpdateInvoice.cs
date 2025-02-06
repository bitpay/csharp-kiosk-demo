// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.UpdateInvoice;
using CsharpKioskDemoDotnet.Invoice.Domain;
using CsharpKioskDemoDotnet.Invoice.Infrastructure.Features.Tasks.UpdateInvoice;
using CsharpKioskDemoDotnet.Shared;
using CsharpKioskDemoDotnet.Shared.Infrastructure;
using CsharpKioskDemoDotnet.Shared.Logger;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

using ILogger = CsharpKioskDemoDotnet.Shared.Logger.ILogger;

namespace CsharpKioskDemoDotnet.Invoice.Infrastructure.Ui.UpdateInvoice;

public class HttpUpdateInvoice : Controller
{
    private readonly Application.Features.Tasks.UpdateInvoice.UpdateInvoice _updateInvoice;
    private readonly IJsonToObjectConverter _jsonToObjectConverter;
    private readonly WebhookVerifier _webhookVerifier;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;

    public HttpUpdateInvoice(
        Application.Features.Tasks.UpdateInvoice.UpdateInvoice updateInvoice,
        IJsonToObjectConverter jsonToObjectConverter,
        WebhookVerifier webhookVerifier,
        IConfiguration configuration,
        ILogger logger
    )
    {
        _updateInvoice = updateInvoice;
        _jsonToObjectConverter = jsonToObjectConverter;
        _webhookVerifier = webhookVerifier;
        _configuration = configuration;
        _logger = logger;
    }

    // POST: invoice/{uuid}
    [HttpPost("invoices/{uuid}")]
    public ActionResult Execute(
        string uuid,
        [FromHeader(Name = "x-signature")] string signature
    )
    {
        var token = _configuration["BitPay:Token"];
        using StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8);
        var rawBody = reader.ReadToEndAsync().GetAwaiter().GetResult();

        ArgumentNullException.ThrowIfNull(uuid);
        ArgumentNullException.ThrowIfNull(rawBody);
        ArgumentNullException.ThrowIfNull(token);

        var body = JsonConvert.DeserializeObject<Dictionary<string, object>>(rawBody);

        ArgumentNullException.ThrowIfNull(body);
        
        if (_webhookVerifier.Verify(token, signature, rawBody)) {
            try
            {
                _updateInvoice.Execute(uuid, GetData(body));

                return Ok();
            }
            catch (ValidationInvoiceUpdateDataFailedException exception)
            {
                return BadRequest(exception.Errors);
            }
            catch (InvoiceNotFoundException)
            {
                return NotFound();
            }
        } else {
            _logger.Error(
                LogCode.IpnSignatureVerificationFail,
                "Webhook signature verification failed",
                new Dictionary<string, object?>
                {
                    { "uuid", uuid }
                }
            );

            return Ok();
        }
    }

    private Dictionary<string, object?> GetData(Dictionary<string, object> body)
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
