using CsharpKioskDemoDotnet.Shared.BitPayProperties;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class InvoiceFormDto
{
    public Design Design { get; }
    public string? Error { get; set; }

    public InvoiceFormDto(Design design)
    {
        Design = design;
    }
}