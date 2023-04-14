using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.UpdateInvoice;

namespace CsharpKioskDemoDotnet.Tests.Invoice.Application.Features.Tasks.UpdateInvoice;

public class ValidateUpdateDataTest : IUnitTest
{
    private IUnitTest UnitTest => this;

    [Fact]
    private void Execute_ValidData_DoNotThrowAnyException()
    {
        // given
        var invoiceJson = UnitTest.GetDataFromFile("invoice.json");
        var invoice = UnitTest.ToObject<CsharpKioskDemoDotnet.Invoice.Domain.Invoice>(invoiceJson);
        var updateDataJson = UnitTest.GetDataFromFile("updateData.json");
        var updateData = UnitTest.ToObject<Dictionary<string, object?>>(updateDataJson);

        // when
        GetTestedClass().Execute(
            updateData,
            invoice
        );

        // then
        // Do not throw any error
    }

    [Fact]
    private void Execute_InvalidData_ThrowValidationInvoiceUpdateDataFailedException()
    {
        // given
        var invoiceJson = UnitTest.GetDataFromFile("invoice.json");
        var invoice = UnitTest.ToObject<CsharpKioskDemoDotnet.Invoice.Domain.Invoice>(invoiceJson);
        var updateDataJson = UnitTest.GetDataFromFile("invalidUpdateData.json");
        var updateData = UnitTest.ToObject<Dictionary<string, object?>>(updateDataJson);

        // when
        var exception = Assert.Throws<ValidationInvoiceUpdateDataFailed>(
            () => GetTestedClass().Execute(updateData, invoice)
        );

        // then
        UnitTest.Equals(19, exception.Errors.Count);
    }

    private ValidateUpdateData GetTestedClass()
    {
        return new ValidateUpdateData(new JsonToObjectConverter());
    }
}