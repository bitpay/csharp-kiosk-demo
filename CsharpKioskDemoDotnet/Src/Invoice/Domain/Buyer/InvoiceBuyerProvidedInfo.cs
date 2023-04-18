// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.Invoice.Domain.Buyer;

public class InvoiceBuyerProvidedInfo
{
    public long Id { get; }
    public string? Name { get; set; }
    public string? PhoneNumber { get; set; }
    public string? SelectedTransactionCurrency { get; set; }
    public string? EmailAddress { get; set; }
    public string? SelectedWallet { get; set; }
    public string? Sms { get; set; }
    public bool? SmsVerified { get; set; }
}