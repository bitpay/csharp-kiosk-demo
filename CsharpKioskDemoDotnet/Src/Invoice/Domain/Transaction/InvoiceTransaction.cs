/*
 * Copyright 2023 BitPay.
 * All rights reserved.
 */

using CsharpKioskDemoDotnet.Shared;

namespace CsharpKioskDemoDotnet.Invoice.Domain.Transaction;

public class InvoiceTransaction
{
    public long Id { get; }
    [FieldExcludedFromSerialization]
    private Invoice Invoice { get; }
    public double? Amount { get; set; }
    public int? Confirmations { get; set; }
    public DateTime? ReceivedTime { get; set; }
    public string? Txid { get; set; }
    
    public InvoiceTransaction(Invoice invoice)
    {
        Invoice = invoice;
    }

    internal InvoiceTransaction()
    {
    }
}