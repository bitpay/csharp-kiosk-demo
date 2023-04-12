using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

namespace CsharpKioskDemoDotnet.Tests.Invoice.Application.Features.Tasks.CreateInvoice;

public class InvoiceTransactionFactoryTest : IGetBitPayInvoice
{
    private IGetBitPayInvoice GetBitPayInvoice => this;
    private IUnitTest UnitTest => this;

    [Fact]
    private void Create_BitPayTransaction_ReturnInvoiceTransaction()
    {
        // given
        var bitPayInvoice = GetBitPayInvoice.Execute();

        // when
        var result = GetTestedClass().Create(
            new CsharpKioskDemoDotnet.Invoice.Domain.Invoice(),
            bitPayInvoice.Transactions[0]
        );

        // then
        UnitTest.Equals(
            UnitTest.ToJson(result),
            UnitTest.GetDataFromFile("invoiceTransaction.json")
        );
    }

    private InvoiceTransactionFactory GetTestedClass()
    {
        return new InvoiceTransactionFactory();
    }
}