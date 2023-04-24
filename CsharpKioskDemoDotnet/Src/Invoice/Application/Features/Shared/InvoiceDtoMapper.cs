// Copyright 2023 BitPay.
// All rights reserved.

using System.Globalization;

using CsharpKioskDemoDotnet.Shared.Domain;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Shared;

public class InvoiceDtoMapper : IConverter<InvoiceDto, Domain.Invoice>
{
    public InvoiceDto Execute(Domain.Invoice item)
    {
        ArgumentNullException.ThrowIfNull(item);
        return new InvoiceDto(
            id: item.Id,
            bitPayId: item.BitPayId,
            price: item.Price.ToString("#,###.00", CultureInfo.CurrentCulture),
            createdDate: item.CreatedDate.ToUniversalTime().ToString("yyyy-MM-dd HH:mm UTC", new DateTimeFormatInfo()),
            bitPayOrderId: item.BitPayOrderId,
            status: item.Status,
            description: item.ItemDescription
        );
    }
}