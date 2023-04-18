// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.Invoice.Domain.Payment;

public class InvoicePayment
{
    public long Id { get; init; }
    public decimal? AmountPaid { get; set; }
    public string? DisplayAmountPaid { get; set; }
    public double? UnderpaidAmount { get; set; }
    public double? OverpaidAmount { get; set; }
    public bool? NonPayProPaymentReceived { get; set; }
    public string? UniversalCodesPaymentString { get; set; }
    public string? UniversalCodesVerificationLink { get; set; }
    public string? TransactionCurrency { get; set; }
    public ICollection<InvoicePaymentCurrency> PaymentCurrencies { get; } = new List<InvoicePaymentCurrency>();

    public void AddPaymentCurrencies(ICollection<InvoicePaymentCurrency> invoicePaymentTotal)
    {
        ArgumentNullException.ThrowIfNull(invoicePaymentTotal);
        foreach (var item in invoicePaymentTotal)
        {
            PaymentCurrencies.Add(item);
        }
    }

    public void Update(InvoicePayment invoicePayment)
    {
        ArgumentNullException.ThrowIfNull(invoicePayment);
        AmountPaid = invoicePayment.AmountPaid;
        TransactionCurrency = invoicePayment.TransactionCurrency;
    }
}