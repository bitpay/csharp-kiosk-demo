// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public interface IBitPayClient
{
    public Task<BitPay.Models.Invoice.Invoice> CreateInvoice(BitPay.Models.Invoice.Invoice invoice);
}