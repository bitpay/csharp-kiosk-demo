// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;
using CsharpKioskDemoDotnet.Invoice.Domain.Payment;

namespace CsharpKioskDemoDotnet.Tests.Invoice.Application.Features.Tasks.CreateInvoice;

public class InvoicePaymentCurrencyFactoryTest : IGetBitPayInvoice
{
    private IGetBitPayInvoice GetBitPayInvoice => this;
    private IUnitTest UnitTest => this;

    [Fact]
    private void Create_BitPayInvoiceAndBtcCode_ReturnInvoicePaymentCurrency()
    {
        // given
        var bitPayInvoice = GetBitPayInvoice.Execute();

        // when
        var result = GetTestedClass().Create(
            "BTC",
            new InvoicePayment(),
            bitPayInvoice
        );

        // then
        UnitTest.Equals(
            UnitTest.ToJson(result),
            UnitTest.GetDataFromFile("invoicePaymentCurrency.json")
        );
    }

    private InvoicePaymentCurrencyFactory GetTestedClass()
    {
        return new InvoicePaymentCurrencyFactory();
    }
}