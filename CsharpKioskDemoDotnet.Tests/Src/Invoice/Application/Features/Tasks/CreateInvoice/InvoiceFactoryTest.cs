using BitPaySDK.Models.Invoice;
using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;
using CsharpKioskDemoDotnet.Invoice.Domain.Buyer;
using CsharpKioskDemoDotnet.Invoice.Domain.ItemizedDetail;
using CsharpKioskDemoDotnet.Invoice.Domain.Payment;
using CsharpKioskDemoDotnet.Invoice.Domain.Refund;
using CsharpKioskDemoDotnet.Shared;
using Moq;
using InvoiceBuyerProvidedInfo = CsharpKioskDemoDotnet.Invoice.Domain.Buyer.InvoiceBuyerProvidedInfo;

namespace CsharpKioskDemoDotnet.Tests.Invoice.Application.Features.Tasks.CreateInvoice;

public class InvoiceFactoryTest : IGetBitPayInvoice
{
    private IGetBitPayInvoice GetBitPayInvoice => this;
    private IUnitTest UnitTest => this;

    [Fact]
    private void Create_BitPayInvoice_ReturnInvoice()
    {
        // given
        var bitPayInvoice = GetBitPayInvoice.Execute();

        // when
        var result = GetTestedClass().Create(bitPayInvoice, "123-456-789");

        // then
        UnitTest.Equals(
            UnitTest.ToJson(result),
            UnitTest.GetDataFromFile("invoice.json"),
            new[] { "CreatedDate", "ExpirationTime" }
        );
    }

    private InvoiceFactory GetTestedClass()
    {
        return new InvoiceFactory(
            GetInvoicePaymentFactory(),
            GetInvoiceBuyerFactory(),
            GetInvoiceRefundFactory(),
            GetInvoiceTransactionFactory(),
            GetInvoiceItemizedDetailFactory()
        );
    }

    private InvoiceTransactionFactory GetInvoiceTransactionFactory()
    {
        var mock = new Mock<InvoiceTransactionFactory>();
        mock.Setup(e => e.Create(
                It.IsAny<CsharpKioskDemoDotnet.Invoice.Domain.Invoice>(),
                It.IsAny<BitPaySDK.Models.Invoice.InvoiceTransaction>()
            ))
            .Returns(
                new CsharpKioskDemoDotnet.Invoice.Domain.Transaction.InvoiceTransaction(
                    new Mock<CsharpKioskDemoDotnet.Invoice.Domain.Invoice>().Object
                )
            );

        return mock.Object;
    }

    private InvoiceItemizedDetailFactory GetInvoiceItemizedDetailFactory()
    {
        var mock = new Mock<InvoiceItemizedDetailFactory>();
        mock.Setup(e => e.Create(
                It.IsAny<CsharpKioskDemoDotnet.Invoice.Domain.Invoice>(),
                It.IsAny<ItemizedDetails>()
            ))
            .Returns(new InvoiceItemizedDetail(new Mock<CsharpKioskDemoDotnet.Invoice.Domain.Invoice>().Object));

        return mock.Object;
    }

    private InvoiceRefundFactory GetInvoiceRefundFactory()
    {
        var mock = new Mock<InvoiceRefundFactory>(
            Mock.Of<IObjectToJsonConverter>(),
            Mock.Of<InvoiceRefundInfoFactory>()
        );
        mock.Setup(e => e.Create(It.IsAny<BitPaySDK.Models.Invoice.Invoice>()))
            .Returns(new InvoiceRefund());

        return mock.Object;
    }

    private InvoiceBuyerFactory GetInvoiceBuyerFactory()
    {
        var mock = new Mock<InvoiceBuyerFactory>(Mock.Of<InvoiceBuyerProvidedInfoFactory>());
        mock.Setup(e => e.Create(
                It.IsAny<BitPaySDK.Models.Invoice.Invoice>(),
                It.IsAny<BitPaySDK.Models.Invoice.InvoiceBuyerProvidedInfo>()
            ))
            .Returns(new InvoiceBuyer(new InvoiceBuyerProvidedInfo()));

        return mock.Object;
    }

    private InvoicePaymentFactory GetInvoicePaymentFactory()
    {
        var mock = new Mock<InvoicePaymentFactory>(Mock.Of<InvoicePaymentCurrencyFactory>());
        mock.Setup(e => e.Create(It.IsAny<BitPaySDK.Models.Invoice.Invoice>()))
            .Returns(new InvoicePayment());

        return mock.Object;
    }
}