// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;
using CsharpKioskDemoDotnet.Invoice.Domain.Refund;

namespace CsharpKioskDemoDotnet.Tests.Invoice.Application.Features.Tasks.CreateInvoice;

public class InvoiceRefundInfoFactoryTest : IGetBitPayInvoice
{
    private IGetBitPayInvoice GetBitPayInvoice => this;
    private IUnitTest UnitTest => this;

    [Fact]
    private void Create_BitPayRefundInfo_ReturnInvoiceRefundInfo()
    {
        // given
        var bitPayInvoice = GetBitPayInvoice.Execute();

        // when
        var result = GetTestedClass().Create(
            new InvoiceRefund(),
            bitPayInvoice.RefundInfo[0]
        );

        // then
        UnitTest.Equals(
            UnitTest.ToJson(result),
            UnitTest.GetDataFromFile("invoiceRefundInfo.json")
        );
    }

    private InvoiceRefundInfoFactory GetTestedClass()
    {
        return new InvoiceRefundInfoFactory();
    }
}