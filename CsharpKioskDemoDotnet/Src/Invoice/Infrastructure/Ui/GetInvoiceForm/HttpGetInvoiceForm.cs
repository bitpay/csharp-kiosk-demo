// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;
using CsharpKioskDemoDotnet.Shared.BitPayProperties;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CsharpKioskDemoDotnet.Invoice.Infrastructure.Ui.GetInvoiceForm;

public class HttpGetInvoiceForm : Controller
{
    private readonly BitPayProperties _bitPayProperties;

    public HttpGetInvoiceForm(IOptions<BitPayProperties> bitPayPropertiesOption)
    {
        ArgumentNullException.ThrowIfNull(bitPayPropertiesOption);
        _bitPayProperties = bitPayPropertiesOption.Value;
    }

    [HttpGet]
    public IActionResult Execute()
    {
        return View(
            "/Src/Invoice/Infrastructure/Views/CreateInvoiceForm/Content.cshtml",
            new InvoiceFormDto(_bitPayProperties.Design)
            {
                Error = (string?)TempData["Error"]
            }
        );
    }
}