using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;
using CsharpKioskDemoDotnet.Invoice.Domain.Payment;
using Moq;

namespace CsharpKioskDemoDotnet.Tests.Invoice.Application.Features.Tasks.CreateInvoice;

public class InvoicePaymentFactoryTest : IGetBitPayInvoice
{
    private IGetBitPayInvoice GetBitPayInvoice => this;
    private IUnitTest UnitTest => this;

    [Fact]
    private void Create_BitPayInvoice_ReturnInvoicePayment()
    {
        // given
        var bitPayInvoice = GetBitPayInvoice.Execute();

        // when
        var result = GetTestedClass().Create(bitPayInvoice);

        // then
        UnitTest.Equals(
            UnitTest.ToJson(result),
            UnitTest.GetDataFromFile("invoicePayment.json")
        );
    }

    private InvoicePaymentFactory GetTestedClass()
    {
        return new InvoicePaymentFactory(GetInvoicePaymentCurrencyFactory());
    }

    private InvoicePaymentCurrencyFactory GetInvoicePaymentCurrencyFactory()
    {
        var mock = new Mock<InvoicePaymentCurrencyFactory>();
        mock.Setup(e => e.Create(
                It.IsAny<string>(),
                It.IsAny<InvoicePayment>(),
                It.IsAny<BitPaySDK.Models.Invoice.Invoice>()
            ))
            .Returns(new InvoicePaymentCurrency(
                new InvoicePayment(),
                "BTC",
                new InvoicePaymentCurrencySupportedTransactionCurrency(),
                new InvoicePaymentCurrencyMinerFee()
            ));

        return mock.Object;
    }
}