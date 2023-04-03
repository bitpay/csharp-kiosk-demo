namespace CsharpKioskDemoDotnet.Shared.Infrastructure;

public class ObjectToJsonConverter : IObjectToJsonConverter
{
    public string? Execute(object? anyObject)
    {
        return anyObject == null 
            ? null 
            : Newtonsoft.Json.JsonConvert.SerializeObject(anyObject);
    }
}