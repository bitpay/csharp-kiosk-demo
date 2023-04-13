using System.Reflection;
using CsharpKioskDemoDotnet.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CsharpKioskDemoDotnet.Tests;

public class IgnoreFieldResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);
#pragma warning disable CS8600
        var customAttribute = (FieldExcludedFromSerialization)property.AttributeProvider!
            .GetAttributes(typeof(FieldExcludedFromSerialization), true)
            .FirstOrDefault();
#pragma warning restore CS8600
        if (customAttribute != null)
        {
            property.Ignored = true;
        }
        
        return property;
    }
}