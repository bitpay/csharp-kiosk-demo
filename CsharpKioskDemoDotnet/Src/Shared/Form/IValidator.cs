// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.Shared.Form
{
    public interface IValidator
    {
        public bool Execute(string value);
    }
}