using System.Globalization;
using CsharpKioskDemoDotnet.Shared.Domain;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Shared;

public class InvoiceDtoMapper : IConverter<InvoiceDto, Domain.Invoice>
{
    public InvoiceDto Execute(Domain.Invoice item)
    {
        return new InvoiceDto(
            id: item.Id,
            bitPayId: item.BitPayId,
            price: item.Price.ToString("#,###.00"),
            createdDate: item.CreatedDate.ToUniversalTime().ToString("yyyy-MM-dd HH:mm UTC"),
            bitPayOrderId: item.BitPayOrderId,
            status: item.Status,
            description: item.ItemDescription
        );
    }
}