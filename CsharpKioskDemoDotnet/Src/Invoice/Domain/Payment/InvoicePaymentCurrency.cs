/*
 * Copyright 2023 BitPay.
 * All rights reserved.
 */

using CsharpKioskDemoDotnet.Shared;

namespace CsharpKioskDemoDotnet.Invoice.Domain.Payment;

public class InvoicePaymentCurrency
{
    public long Id { get; init; }
    [FieldExcludedFromSerialization]
    public InvoicePayment InvoicePayment { get; set; }
    public string CurrencyCode { get; set; }
    public string? Total { get; set; }
    public string? Subtotal { get; set; }
    public string? DisplayTotal { get; set; }
    public string? DisplaySubtotal { get; set; }
    public ICollection<InvoicePaymentCurrencyExchangeRate> ExchangeRates { get; } = new List<InvoicePaymentCurrencyExchangeRate>();
    public ICollection<InvoicePaymentCurrencyCode> CurrencyCodes { get; } = new List<InvoicePaymentCurrencyCode>();
    public InvoicePaymentCurrencySupportedTransactionCurrency SupportedTransactionCurrency { get; set; }
    public InvoicePaymentCurrencyMinerFee MinerFee { get; set; }

    public InvoicePaymentCurrency(
        InvoicePayment invoicePayment,
        string currencyCode,
        InvoicePaymentCurrencySupportedTransactionCurrency supportedTransactionCurrency,
        InvoicePaymentCurrencyMinerFee minerFee
    )
    {
        InvoicePayment = invoicePayment;
        CurrencyCode = currencyCode;
        SupportedTransactionCurrency = supportedTransactionCurrency;
        MinerFee = minerFee;
    }
    
#pragma warning disable CS8618
    internal InvoicePaymentCurrency()
#pragma warning restore CS8618
    {
    }

    public void AddExchangeRates(ICollection<InvoicePaymentCurrencyExchangeRate> paymentExchangeRates)
    {
        foreach (var item in paymentExchangeRates)
        {
            ExchangeRates.Add(item);
        }
    }

    public void AddPaymentCodes(ICollection<InvoicePaymentCurrencyCode> invoicePaymentCodes)
    {
        foreach (var item in invoicePaymentCodes)
        {
            CurrencyCodes.Add(item);
        }
    }
}