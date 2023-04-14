using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;
using CsharpKioskDemoDotnet.Invoice.Domain.Buyer;
using Moq;

namespace CsharpKioskDemoDotnet.Tests.Invoice.Application.Features.Tasks.CreateInvoice;

public class InvoiceBuyerFactoryTest : IGetBitPayInvoice
{
    private IGetBitPayInvoice GetBitPayInvoice => this;
    private IUnitTest UnitTest => this;

    [Fact]
    private void Create_BitPayInvoice_ReturnInvoiceBuyer()
    {
        // given
        var bitPayInvoice = GetBitPayInvoice.Execute();

        // when
        var result = GetTestedClass().Create(
            bitPayInvoice,
            new BitPay.Models.Invoice.InvoiceBuyerProvidedInfo()
        );

        // then
        UnitTest.Equals(
            UnitTest.ToJson(result),
            UnitTest.GetDataFromFile("invoiceBuyer.json")
        );
    }

    [Fact]
    private void Create_BitPayInvoiceWithoutBuyer_ReturnInvoiceBuyer()
    {
        // given
        var bitPayInvoiceJson = UnitTest.GetDataFromFile("bitPayInvoiceWithoutBuyer.json"); 
        var bitPayInvoice = UnitTest.ToObject<BitPay.Models.Invoice.Invoice>(bitPayInvoiceJson);

        // when
        var result = GetTestedClass().Create(
            bitPayInvoice,
            new BitPay.Models.Invoice.InvoiceBuyerProvidedInfo()
        );

        // then
        UnitTest.Equals(
            UnitTest.ToJson(result),
            UnitTest.GetDataFromFile("invoiceBuyerWithoutBitPayBuyer.json")
        );
    }

    private InvoiceBuyerFactory GetTestedClass()
    {
        var mock = new Mock<InvoiceBuyerProvidedInfoFactory>();
        mock.Setup(e => e.Create(It.IsAny<BitPay.Models.Invoice.InvoiceBuyerProvidedInfo>()))
            .Returns(new InvoiceBuyerProvidedInfo());

        return new InvoiceBuyerFactory(mock.Object);
    }
}