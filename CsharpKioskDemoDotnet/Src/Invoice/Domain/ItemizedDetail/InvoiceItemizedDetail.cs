/*
 * Copyright 2023 BitPay.
 * All rights reserved.
 */

namespace CsharpKioskDemoDotnet.Invoice.Domain.ItemizedDetail;

public class InvoiceItemizedDetail
{
    public long Id { get; }
    public Invoice Invoice { get; set; }
    public double? Amount { get; set; }
    public string? Description { get; set; }
    public bool? IsFee { get; set; }
    
    public InvoiceItemizedDetail(Invoice invoice)
    {
        Invoice = invoice;
    }
    
    internal InvoiceItemizedDetail()
    {
    }
}