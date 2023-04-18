// Copyright 2023 BitPay.
// All rights reserved.

using System.Globalization;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Shared;

public static class ParseMillisecondsToDataTime
{
    public static DateTime Execute(string milliseconds)
    {
        return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            .AddMilliseconds(double.Parse(milliseconds, CultureInfo.CurrentCulture))
            .ToLocalTime();
    }
}