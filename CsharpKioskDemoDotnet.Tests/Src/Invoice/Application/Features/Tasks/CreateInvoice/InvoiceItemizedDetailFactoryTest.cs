using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

namespace CsharpKioskDemoDotnet.Tests.Invoice.Application.Features.Tasks.CreateInvoice;

public class InvoiceItemizedDetailFactoryTest : IGetBitPayInvoice
{
    private IGetBitPayInvoice GetBitPayInvoice => this;
    private IUnitTest UnitTest => this;
    
    [Fact]
    private void Create_BitPayInvoiceItemizedDetail_ReturnInvoiceItemizedDetail()
    {
        // given
        var bitPayInvoice = GetBitPayInvoice.Execute();
        var invoiceJson = UnitTest.GetDataFromFile("invoice.json");
        var invoice = UnitTest.ToObject<CsharpKioskDemoDotnet.Invoice.Domain.Invoice>(invoiceJson);

        // when
        var result = GetTestedClass().Create(
            invoice,
            bitPayInvoice.ItemizedDetails[0]
        );

        // then
        UnitTest.Equals(
            UnitTest.ToJson(result),
            UnitTest.GetDataFromFile("invoiceItemizedDetail.json")
        );
    }

    private InvoiceItemizedDetailFactory GetTestedClass()
    {
        return new InvoiceItemizedDetailFactory();
    }
}