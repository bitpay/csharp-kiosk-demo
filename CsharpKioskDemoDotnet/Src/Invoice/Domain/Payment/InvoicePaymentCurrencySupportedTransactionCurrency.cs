/*
 * Copyright 2023 BitPay.
 * All rights reserved.
 */

namespace CsharpKioskDemoDotnet.Invoice.Domain.Payment;

public class InvoicePaymentCurrencySupportedTransactionCurrency
{
    public long Id { get; init;}
    public bool? Enabled { get; set; }
    public string? Reason { get; set; }

}