// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.UpdateInvoice;

public interface IAfterInvoiceUpdate
{
    void Execute(
        Invoice.Domain.Invoice invoice,
        string? eventName
    );
}