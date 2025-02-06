// Copyright 2023 BitPay.
// All rights reserved.

using System;
using System.Security.Cryptography;
using System.Text;

namespace CsharpKioskDemoDotnet.Invoice.Infrastructure.Features.Tasks.UpdateInvoice;

public class WebhookVerifier
{
    public bool Verify(string signingKey, string sigHeader, string webhookBody)
    {
        ArgumentNullException.ThrowIfNull(signingKey);
        ArgumentNullException.ThrowIfNull(sigHeader);
        ArgumentNullException.ThrowIfNull(webhookBody);

        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(signingKey));
        byte[] signatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(webhookBody));
        string calculated = Convert.ToBase64String(signatureBytes);
        bool match = sigHeader.Equals(calculated, StringComparison.Ordinal);

        return match;
    }
}
