using CsharpKioskDemoDotnet.Invoice.Application.Features.Shared;
using CsharpKioskDemoDotnet.Shared.BitPayProperties;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.GetInvoice;

public class InvoiceViewDto
{
    public Design Design { get; }
    public InvoiceDto Invoice { get; }

    public InvoiceViewDto(
        Design design,
        InvoiceDto invoice
    )
    {
        Design = design;
        Invoice = invoice;
    }
}