// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Shared.BitPayProperties;

namespace CsharpKioskDemoDotnet.Shared.Form;

public class MissingRequiredFieldException : Exception
{
    public MissingRequiredFieldException(Field field)
    {
        Field = field;
    }

    public Field Field { get; init; }

    public override string Message { 
        get {
            return $"Field: {Field.Label} is required.";
        }
    }
}