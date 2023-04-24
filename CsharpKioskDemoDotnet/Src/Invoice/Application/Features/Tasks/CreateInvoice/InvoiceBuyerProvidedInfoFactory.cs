// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Invoice.Domain.Buyer;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class InvoiceBuyerProvidedInfoFactory
{
    internal virtual InvoiceBuyerProvidedInfo Create(BitPay.Models.Invoice.InvoiceBuyerProvidedInfo buyerProvidedInfo)
    {
        return new InvoiceBuyerProvidedInfo
        {
            Name = buyerProvidedInfo.Name,
            PhoneNumber = buyerProvidedInfo.PhoneNumber,
            SelectedTransactionCurrency = buyerProvidedInfo.SelectedTransactionCurrency,
            EmailAddress = buyerProvidedInfo.EmailAddress,
            SelectedWallet = buyerProvidedInfo.SelectedWallet,
            Sms = null,
            SmsVerified = false
        };
    }
}