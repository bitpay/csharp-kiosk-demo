using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CsharpKioskDemoDotnet.Shared.Infrastructure;

public class ObjectToJsonConverter : IObjectToJsonConverter
{
    private readonly JsonSerializerSettings _settings = new()
    {
        ContractResolver = new IgnoreFieldResolver(),
        Formatting = Formatting.Indented
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
        var customAttribute = (FieldExcludedFromSerialization)property.AttributeProvider!
            .GetAttributes(typeof(FieldExcludedFromSerialization), true)
            .FirstOrDefault();
        if (customAttribute != null)
        {
            property.Ignored = true;
        }

        return property;
    }
}