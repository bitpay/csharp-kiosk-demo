using BitPaySDK.Models.Invoice;
using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;
using CsharpKioskDemoDotnet.Invoice.Domain.Refund;
using CsharpKioskDemoDotnet.Shared;
using Moq;
using Newtonsoft.Json;

namespace CsharpKioskDemoDotnet.Tests.Invoice.Application.Features.Tasks.CreateInvoice;

public class InvoiceRefundFactoryTest : IGetBitPayInvoice
{
    private IGetBitPayInvoice GetBitPayInvoice => this;
    private IUnitTest UnitTest => this;

    [Fact]
    private void Create_BitPayInvoice_ReturnInvoiceRefund()
    {
        // given
        var bitPayInvoice = GetBitPayInvoice.Execute();

        // when
        var result = GetTestedClass().Create(bitPayInvoice);

        // then
        UnitTest.Equals(
            UnitTest.ToJson(result),
            UnitTest.GetDataFromFile("invoiceRefund.json")
        );
    }

    private InvoiceRefundFactory GetTestedClass()
    {
        return new InvoiceRefundFactory(
            new ObjectToJsonConverter(),
            GetInvoiceRefundInfoFactory()
        );
    }

    private InvoiceRefundInfoFactory GetInvoiceRefundInfoFactory()
    {
        var mock = new Mock<InvoiceRefundInfoFactory>();
        mock.Setup(e => e.Create(
                It.IsAny<InvoiceRefund>(),
                It.IsAny<RefundInfo>()
            ))
            .Returns(new InvoiceRefundInfo(new InvoiceRefund(), "BTC"));

        return mock.Object;
    }
}

internal class ObjectToJsonConverter : IObjectToJsonConverter
{
    public string? Execute(object? anyObject)
    {
        return anyObject == null
            ? null
            : JsonConvert.SerializeObject(anyObject);
    }
}