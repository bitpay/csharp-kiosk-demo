// Copyright 2023 BitPay.
// All rights reserved.

using BitPay.Models.Invoice;

using CsharpKioskDemoDotnet.Invoice.Domain.Refund;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class InvoiceRefundInfoFactory
{
    internal virtual InvoiceRefundInfo Create(
        InvoiceRefund invoiceRefund,
        RefundInfo info
    )
    {
        var invoiceRefundInfo = new InvoiceRefundInfo(
            invoiceRefund: invoiceRefund,
            currencyCode: info.Currency
        )
        {
            SupportRequest = info.SupportRequest,
        };

        invoiceRefundInfo.AddRefundInfoAmounts(
            GetRefundInfoAmounts(
                invoiceRefundInfo,
                info.Amounts
            )
        );

        return invoiceRefundInfo;
    }

    private ICollection<InvoiceRefundInfoAmount> GetRefundInfoAmounts(
        InvoiceRefundInfo invoiceRefundInfo,
        Dictionary<string, decimal> infoAmounts
    )
    {
        return infoAmounts.Select(item => new InvoiceRefundInfoAmount(
                    invoiceRefundInfo: invoiceRefundInfo,
                    currencyCode: item.Key
                )
                {
                    Amount = item.Value
                }
            )
            .ToList();
    }
}