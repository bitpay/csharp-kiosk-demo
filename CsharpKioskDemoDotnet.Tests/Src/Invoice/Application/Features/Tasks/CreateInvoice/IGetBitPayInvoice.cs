namespace CsharpKioskDemoDotnet.Tests.Invoice.Application.Features.Tasks.CreateInvoice;

internal interface IGetBitPayInvoice : IUnitTest
{
    BitPaySDK.Models.Invoice.Invoice Execute()
    {
        var bitPayInvoiceJson = GetDataFromFile("bitPayInvoice.json");

        return ToObject<BitPaySDK.Models.Invoice.Invoice>(bitPayInvoiceJson);
    }
}