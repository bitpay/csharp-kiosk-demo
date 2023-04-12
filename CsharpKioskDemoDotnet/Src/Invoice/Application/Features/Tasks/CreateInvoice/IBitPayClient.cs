namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public interface IBitPayClient
{
    public Task<BitPaySDK.Models.Invoice.Invoice> CreateInvoice(BitPaySDK.Models.Invoice.Invoice invoice);
}