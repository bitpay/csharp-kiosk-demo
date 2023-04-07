using CsharpKioskDemoDotnet.Invoice.Domain.Transaction;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class InvoiceTransactionFactory
{
    internal virtual InvoiceTransaction Create(
        Domain.Invoice invoice,
        BitPaySDK.Models.Invoice.InvoiceTransaction transaction
    )
    {
        return new InvoiceTransaction(invoice)
        {
            Amount = transaction.Amount,
            Confirmations = Convert.ToInt32(transaction.Confirmations),
            ReceivedTime = transaction.ReceivedTime != null
                ? DateTime.Parse(transaction.ReceivedTime).ToLocalTime()
                : null,
            Txid = transaction.Txid
        };
    }
}