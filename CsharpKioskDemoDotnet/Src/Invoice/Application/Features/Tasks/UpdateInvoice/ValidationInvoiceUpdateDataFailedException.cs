// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.UpdateInvoice;

public class ValidationInvoiceUpdateDataFailedException : Exception
{
    public Dictionary<string, string> Errors { get; }

    public ValidationInvoiceUpdateDataFailedException(Dictionary<string, string> errors)
    {
        Errors = errors;
    }
}