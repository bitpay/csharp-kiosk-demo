// Copyright 2023 BitPay.
// All rights reserved.

using System.Text.RegularExpressions;

namespace CsharpKioskDemoDotnet.Shared.Form
{
    public class ZipCodeValidator : IValidator
    {
        public bool Execute(string value) {
            return Regex.Match(value, @"^\d{5}(?:[-\s]\d{4})?$").Success;
        }
    }
}