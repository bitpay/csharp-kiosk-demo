// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.Tests.Invoice.Application.Features.Tasks.CreateInvoice;

internal interface IGetBitPayInvoice : IUnitTest
{
    BitPay.Models.Invoice.Invoice Execute()
    {
        var bitPayInvoiceJson = GetDataFromFile("bitPayInvoice.json");

        return ToObject<BitPay.Models.Invoice.Invoice>(bitPayInvoiceJson);
    }
}