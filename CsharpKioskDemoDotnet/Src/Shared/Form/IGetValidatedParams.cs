// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.Shared.Form
{
    public interface IGetValidatedParams
    {
        Dictionary<string, string> Execute(Dictionary<string, string?> requestParameters);
    }
}