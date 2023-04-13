using CsharpKioskDemoDotnet.Invoice.Domain.Buyer;
using InvoiceBuyerProvidedInfo = BitPay.Models.Invoice.InvoiceBuyerProvidedInfo;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class InvoiceBuyerFactory
{
    
    private readonly InvoiceBuyerProvidedInfoFactory _invoiceBuyerProvidedInfoFactory;

    public InvoiceBuyerFactory(InvoiceBuyerProvidedInfoFactory invoiceBuyerProvidedInfoFactory)
    {
        _invoiceBuyerProvidedInfoFactory = invoiceBuyerProvidedInfoFactory;
    }

    internal virtual InvoiceBuyer Create(
        BitPay.Models.Invoice.Invoice bitPayInvoice,
        InvoiceBuyerProvidedInfo buyerProvidedInfo
    )
    {
        var buyer = bitPayInvoice.Buyer;

        if (buyer == null) {
            return new InvoiceBuyer(_invoiceBuyerProvidedInfoFactory.Create(buyerProvidedInfo))
            {
                BuyerProvidedEmail = bitPayInvoice.BuyerProvidedEmail,
            };
        }

        return new InvoiceBuyer(_invoiceBuyerProvidedInfoFactory.Create(buyerProvidedInfo))
        {
            BuyerProvidedEmail = bitPayInvoice.BuyerProvidedEmail,
            Name = buyer.Name,
            Address1 = buyer.Address1,
            Address2 = buyer.Address2,
            City = buyer.Locality,
            Region = buyer.Region,
            PostalCode = buyer.PostalCode,
            Country = buyer.Country,
            Email = buyer.Email,
            Phone = buyer.Phone,
            Notify = buyer.Notify
        };
    }
}