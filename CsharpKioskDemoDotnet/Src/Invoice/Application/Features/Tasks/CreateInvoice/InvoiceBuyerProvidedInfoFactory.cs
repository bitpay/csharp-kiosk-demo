using CsharpKioskDemoDotnet.Invoice.Domain.Buyer;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class InvoiceBuyerProvidedInfoFactory
{
    internal virtual InvoiceBuyerProvidedInfo Create(BitPaySDK.Models.Invoice.InvoiceBuyerProvidedInfo buyerProvidedInfo)
    {
        return new InvoiceBuyerProvidedInfo
        {
            Name = buyerProvidedInfo.Name,
            PhoneNumber = buyerProvidedInfo.PhoneNumber,
            SelectedTransactionCurrency = buyerProvidedInfo.SetSelectedTransactionCurrency,
            EmailAddress = buyerProvidedInfo.EmailAddress,
            SelectedWallet = buyerProvidedInfo.SelectedWallet,
            Sms = null,
            SmsVerified = false
        };
    }
}