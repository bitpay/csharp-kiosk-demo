using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

namespace CsharpKioskDemoDotnet.Tests.Invoice.Application.Features.Tasks.CreateInvoice;

public class InvoiceBuyerProvidedInfoFactoryTest : IGetBitPayInvoice
{
    private IGetBitPayInvoice GetBitPayInvoice => this;
    private IUnitTest UnitTest => this;
    
    [Fact]
    private void Create_BitPayInvoice_ReturnInvoiceBuyerProvidedInfo()
    {
        // given
        var bitPayInvoice = GetBitPayInvoice.Execute();
    
        // when
        var result = GetTestedClass().Create(bitPayInvoice.BuyerProvidedInfo);
    
        // then
        UnitTest.Equals(
            UnitTest.ToJson(result),
            UnitTest.GetDataFromFile("invoiceBuyerProvidedInfo.json")
        );
    }
    
    private InvoiceBuyerProvidedInfoFactory GetTestedClass()
    {
        return new InvoiceBuyerProvidedInfoFactory();
    }
}