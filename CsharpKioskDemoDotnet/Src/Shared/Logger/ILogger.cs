// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.Shared.Logger;

public interface ILogger
{
    void Info(
        LogCode code,
        string message,
        Dictionary<string, object?> context
    );

    void Error(
        LogCode code,
        string message,
        Dictionary<string, object?> context
    );
}