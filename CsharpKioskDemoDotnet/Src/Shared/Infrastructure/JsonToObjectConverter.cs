// Copyright 2023 BitPay.
// All rights reserved.

using Newtonsoft.Json;

namespace CsharpKioskDemoDotnet.Shared.Infrastructure;

public class JsonToObjectConverter : IJsonToObjectConverter
{
    public T? Execute<T>(string? json)
    {
        return json == null
            ? default
            : JsonConvert.DeserializeObject<T>(json);
    }
}