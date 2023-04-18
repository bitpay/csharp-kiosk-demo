// Copyright 2023 BitPay.
// All rights reserved.

using BitPay.Models.Invoice;

using CsharpKioskDemoDotnet.Invoice.Domain.Refund;
using CsharpKioskDemoDotnet.Shared;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class InvoiceRefundFactory
{
    private readonly IObjectToJsonConverter _objectToJsonConverter;
    private readonly InvoiceRefundInfoFactory _invoiceRefundInfoFactory;

    public InvoiceRefundFactory(
        IObjectToJsonConverter objectToJsonConverter,
        InvoiceRefundInfoFactory invoiceRefundInfoFactory
    )
    {
        _objectToJsonConverter = objectToJsonConverter;
        _invoiceRefundInfoFactory = invoiceRefundInfoFactory;
    }

    internal virtual InvoiceRefund Create(BitPay.Models.Invoice.Invoice bitPayInvoice)
    {
        var invoiceRefund = new InvoiceRefund
        {
            AddressesJson = _objectToJsonConverter.Execute(bitPayInvoice.RefundAddresses),
            AddressRequestPending = bitPayInvoice.RefundAddressRequestPending
        };

        invoiceRefund.AddRefundInfo(
            GetRefundInfo(
                invoiceRefund,
                bitPayInvoice.RefundInfo
            )
        );

        return invoiceRefund;
    }

    private ICollection<InvoiceRefundInfo> GetRefundInfo(
        InvoiceRefund invoiceRefund,
        ICollection<RefundInfo>? refundInfo
    )
    {
        if (refundInfo == null)
        {
            return new List<InvoiceRefundInfo>();
        }

        return refundInfo.Select(info => _invoiceRefundInfoFactory.Create(invoiceRefund, info))
            .ToList();
    }
}