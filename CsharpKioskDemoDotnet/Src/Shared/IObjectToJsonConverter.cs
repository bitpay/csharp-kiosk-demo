namespace CsharpKioskDemoDotnet.Shared;

public interface IObjectToJsonConverter
{
    string? Execute(object anyObject);
}