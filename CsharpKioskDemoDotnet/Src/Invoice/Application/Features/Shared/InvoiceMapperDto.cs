using System.Globalization;
using CsharpKioskDemoDotnet.Shared.Domain;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Shared;

public class InvoiceMapperDto : IConverter<InvoiceDto, Domain.Invoice>
{
    public InvoiceDto Execute(Domain.Invoice item)
    {
        return new InvoiceDto(
            id: item.Id,
            bitPayId: item.BitPayId,
            price: item.Price.ToString("#,###.00#"),
            createdDate: item.CreatedDate.ToUniversalTime().ToString(CultureInfo.InvariantCulture),
            bitPayOrderId: item.BitPayOrderId,
            status: item.Status,
            description: item.ItemDescription
        );
    }
}