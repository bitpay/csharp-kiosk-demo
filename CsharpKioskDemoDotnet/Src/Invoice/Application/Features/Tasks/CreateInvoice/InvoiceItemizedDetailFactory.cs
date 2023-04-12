using BitPaySDK.Models.Invoice;
using CsharpKioskDemoDotnet.Invoice.Domain.ItemizedDetail;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class InvoiceItemizedDetailFactory
{
    internal InvoiceItemizedDetail Create(
        Domain.Invoice invoice,
        ItemizedDetails itemizedDetail
    )
    {
        return new InvoiceItemizedDetail(invoice)
        {
            Amount = itemizedDetail.Amount,
            Description = itemizedDetail.Description,
            IsFee = itemizedDetail.IsFee
        };
    }
}