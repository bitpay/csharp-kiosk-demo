// Copyright 2023 BitPay.
// All rights reserved.

using Microsoft.AspNetCore.Mvc;

namespace CsharpKioskDemoDotnet.Invoice.Infrastructure.Ui.CreateInvoice;

public class HttpCreateInvoice : Controller
{
    private readonly Application.Features.Tasks.CreateInvoice.CreateInvoice _createInvoice;

    public HttpCreateInvoice(Application.Features.Tasks.CreateInvoice.CreateInvoice createInvoice)
    {
        _createInvoice = createInvoice;
    }

    // POST: invoice
    [HttpPost("invoice")]
    public IActionResult Execute(Dictionary<string, string?> parameters)
    {
        try
        {
            var invoice = _createInvoice.Execute(parameters);

            return RedirectPermanent(invoice.BitPayUrl + "&context=mt");
        }
        catch (Exception exception)
        {
            TempData["Error"] = exception.Message;
            return RedirectToAction(nameof(Execute), "/");
        }
    }
}