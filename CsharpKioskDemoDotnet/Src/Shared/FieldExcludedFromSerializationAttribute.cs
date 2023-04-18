// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.Shared;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public sealed class FieldExcludedFromSerializationAttribute : Attribute
{

}