// Copyright 2023 BitPay.
// All rights reserved.

using System.Globalization;
using System.Text.RegularExpressions;

namespace CsharpKioskDemoDotnet.Shared.Form
{
    public class EmailValidator : IValidator
    {
        public bool Execute(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            try
            {
                // Normalize the domain
                value = Regex.Replace(
                    value,
                    @"(@)(.+)$",
                    DomainMapper,
                    RegexOptions.None,
                    TimeSpan.FromMilliseconds(200)
                );

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(value,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}