// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.Shared;

public interface IJsonToObjectConverter
{
    T? Execute<T>(string? json);
}