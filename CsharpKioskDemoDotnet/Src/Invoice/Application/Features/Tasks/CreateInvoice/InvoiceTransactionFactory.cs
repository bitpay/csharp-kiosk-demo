// Copyright 2023 BitPay.
// All rights reserved.

using System.Globalization;

using CsharpKioskDemoDotnet.Invoice.Domain.Transaction;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class InvoiceTransactionFactory
{
    internal virtual InvoiceTransaction Create(
        Domain.Invoice invoice,
        BitPay.Models.Invoice.InvoiceTransaction transaction
    )
    {
        return new InvoiceTransaction(invoice)
        {
            Amount = transaction.Amount,
            Confirmations = Convert.ToInt32(transaction.Confirmations, CultureInfo.CurrentCulture),
            ReceivedTime = transaction.ReceivedTime != null
                ? DateTime.Parse(transaction.ReceivedTime, CultureInfo.CurrentCulture).ToLocalTime()
                : null,
            Txid = transaction.Txid
        };
    }
}