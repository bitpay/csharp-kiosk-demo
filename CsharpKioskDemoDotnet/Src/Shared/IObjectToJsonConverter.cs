// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.Shared;

public interface IObjectToJsonConverter
{
    string? Execute(object anyObject);
}