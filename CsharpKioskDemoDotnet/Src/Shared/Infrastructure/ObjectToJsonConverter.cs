// Copyright 2023 BitPay.
// All rights reserved.

using System.Reflection;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CsharpKioskDemoDotnet.Shared.Infrastructure;

public class ObjectToJsonConverter : IObjectToJsonConverter
{
    private readonly JsonSerializerSettings _settings = new()
    {
        ContractResolver = new IgnoreFieldResolver()
    };

    public string? Execute(object? anyObject)
    {
        return anyObject == null
            ? null
            : JsonConvert.SerializeObject(anyObject, _settings);
    }
}

internal class IgnoreFieldResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(
        MemberInfo member,
        MemberSerialization memberSerialization
    )
    {
        var property = base.CreateProperty(member, memberSerialization);
#pragma warning disable CS8600
        Attribute? customAttribute = (FieldExcludedFromSerializationAttribute)property.AttributeProvider!
            .GetAttributes(typeof(FieldExcludedFromSerializationAttribute), true)
            .FirstOrDefault();
#pragma warning restore CS8600
        if (customAttribute != null)
        {
            property.Ignored = true;
        }

        return property;
    }
}