namespace CsharpKioskDemoDotnet.Shared;

public interface IJsonToObjectConverter
{
    T? Execute<T>(string? json);
}