namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Shared;

public static class ParseMilisecondsToDataTime
{
    public static DateTime Execute(string miliseconds)
    {
        return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            .AddMilliseconds(double.Parse(miliseconds))
            .ToLocalTime();
    }
}