// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.UpdateInvoice;

namespace CsharpKioskDemoDotnet.Tests.Invoice.Application.Features.Tasks.UpdateInvoice;

public class GetInvoiceWithUpdateDataTest : IUnitTest
{
    private IUnitTest UnitTest => this;

    [Fact]
    private void Execute_UpdateData_ReturnInvoice()
    {
        // given
        var invoiceJson = UnitTest.GetDataFromFile("invoice.json");
        var invoice = UnitTest.ToObject<CsharpKioskDemoDotnet.Invoice.Domain.Invoice>(invoiceJson);
        var updateDataJson = UnitTest.GetDataFromFile("updateData.json");
        var updateData = UnitTest.ToObject<Dictionary<string, object?>>(updateDataJson);

        // when
        var result = GetTestedClass().Execute(
            updateData,
            invoice
        );

        // then
        UnitTest.Equals(
            UnitTest.ToJson(result),
            UnitTest.GetDataFromFile("updatedInvoice.json"),
            new[] { "CreatedDate", "ExpirationTime" }
        );
    }

    private GetInvoiceWithUpdateData GetTestedClass()
    {
        return new GetInvoiceWithUpdateData(new JsonToObjectConverter());
    }
}