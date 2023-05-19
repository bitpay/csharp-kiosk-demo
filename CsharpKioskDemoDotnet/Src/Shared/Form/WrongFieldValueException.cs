// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Shared.BitPayProperties;

namespace CsharpKioskDemoDotnet.Shared.Form
{
    public class WrongFieldValueException : Exception
    {
        public Field Field { get; init; }
        public object Value { get; init; }

        public WrongFieldValueException(Field field, object value)
        {
            Field = field;
            Value = value;
        }

        public override string Message { 
            get {
                return $"Field: {Field.Label} has wrong value: {Value}.";
            }
        }
    }
}