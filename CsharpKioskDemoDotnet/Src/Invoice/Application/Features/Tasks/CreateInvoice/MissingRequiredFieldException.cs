// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Shared.BitPayProperties;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class MissingRequiredFieldException : Exception
{
    public MissingRequiredFieldException(Field field)
    {
        Field = field;
    }

    public Field Field { get; init; }
}