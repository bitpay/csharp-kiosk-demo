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