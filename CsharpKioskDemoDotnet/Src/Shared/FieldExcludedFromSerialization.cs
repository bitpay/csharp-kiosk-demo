namespace CsharpKioskDemoDotnet.Shared;

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
public sealed class FieldExcludedFromSerialization : Attribute
{
    
}