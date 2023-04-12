using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.GetInvoice;
using CsharpKioskDemoDotnet.Invoice.Domain;
using CsharpKioskDemoDotnet.Shared.BitPayProperties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CsharpKioskDemoDotnet.Invoice.Infrastructure.Ui.GetInvoiceView;

public class HttpGetInvoiceView : Controller
{
    private readonly BitPayProperties _bitPayProperties;
    private readonly GetInvoiceDto _getInvoiceDto;
    
    public HttpGetInvoiceView(
        IOptions<BitPayProperties> bitPayPropertiesOption,
        GetInvoiceDto getInvoiceDto
    )
    {
        _getInvoiceDto = getInvoiceDto;
        _bitPayProperties = bitPayPropertiesOption.Value;
    }
    
    // GET: invoices/{invoiceId}
    [HttpGet("invoices/{invoiceId}")]
    public IActionResult Execute(int invoiceId)
    {
        try
        {
            var invoice = _getInvoiceDto.Execute(invoiceId);
            var invoiceViewDto = new InvoiceViewDto(
                design: _bitPayProperties.Design,
                invoice: invoice
            );
    
            return View(
                "/Src/Invoice/Infrastructure/Views/InvoiceView/Content.cshtml",
                invoiceViewDto
            );
        }
        catch (InvoiceNotFound exception)
        {
            return RedirectPermanent("/404");
        }
    }
}