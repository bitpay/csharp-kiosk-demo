// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Invoice.Application.Features.Shared;

namespace CsharpKioskDemoDotnet.Tests.Invoice.Application.Features.Shared;

public class InvoiceDtoMapperTest : IUnitTest
{
    private IUnitTest UnitTest => this;

    [Fact]
    private void Execute_Invoice_ReturnInvoiceDto()
    {
        // given
        var invoiceJson = UnitTest.GetDataFromFile("invoice.json");
        var invoice = UnitTest.ToObject<CsharpKioskDemoDotnet.Invoice.Domain.Invoice>(invoiceJson);

        // when
        var result = GetTestedClass().Execute(invoice);

        // then
        UnitTest.Equals(
            UnitTest.ToJson(result),
            UnitTest.GetDataFromFile("invoiceDto.json")
        );
    }

    private InvoiceDtoMapper GetTestedClass()
    {
        return new InvoiceDtoMapper();
    }
}