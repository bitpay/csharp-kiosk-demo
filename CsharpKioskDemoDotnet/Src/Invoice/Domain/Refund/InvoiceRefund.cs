// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.Invoice.Domain.Refund;

public class InvoiceRefund
{
    public long Id { get; }
    public string? AddressesJson { get; set; }
    public bool? AddressRequestPending { get; set; }
    public ICollection<InvoiceRefundInfo> RefundInfo { get; } = new List<InvoiceRefundInfo>();

    public void AddRefundInfo(ICollection<InvoiceRefundInfo> invoiceRefundInfo)
    {
        ArgumentNullException.ThrowIfNull(invoiceRefundInfo);
        foreach (var item in invoiceRefundInfo)
        {
            RefundInfo.Add(item);
        }
    }
}