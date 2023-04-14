namespace CsharpKioskDemoDotnet.Shared.Logger.Infrastructure;

public class StdoutLogger : ILogger
{
    private readonly IObjectToJsonConverter _objectToJsonConverter;

    public StdoutLogger(IObjectToJsonConverter objectToJsonConverter)
    {
        _objectToJsonConverter = objectToJsonConverter;
    }

    public void Info(LogCode code, string message, Dictionary<string, object?> context)
    {
        PrintLog("INFO", code, message, context);
    }

    public void Error(LogCode code, string message, Dictionary<string, object?> context)
    {
        PrintLog("ERROR", code, message, context);
    }

    private void PrintLog(
        string level,
        LogCode code,
        string message,
        Dictionary<string, object?> context
    )
    {
        var json = _objectToJsonConverter.Execute(
            new Dictionary<string, object>
            {
                { "level", level },
                { "timestamp", ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds() },
                { "code", code },
                { "message", message },
                { "context", context }
            }
        );

        Console.WriteLine(json);
    }
}