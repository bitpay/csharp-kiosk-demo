// Copyright 2023 BitPay.
// All rights reserved.

using System.ComponentModel;

namespace CsharpKioskDemoDotnet.Shared.Logger;

public enum LogCode
{
    [Description("INVOICE_GET")] InvoiceGet,
    [Description("INVOICE_GRID_GET")] InvoiceGridGet,
    [Description("INVOICE_CREATE_SUCCESS")] InvoiceCreateSuccess,
    [Description("INVOICE_CREATE_FAIL")] InvoiceCreateFail,
    [Description("INVOICE_UPDATE_SUCCESS")] InvoiceUpdateSuccess,
    [Description("INVOICE_UPDATE_FAIL")] InvoiceUpdateFail,
    [Description("IPN_RECEIVED")] IpnReceived,
    [Description("IPN_VALIDATE_SUCCESS")] IpnValidateSuccess,
    [Description("IPN_VALIDATE_FAIL")] IpnValidateFail,
    [Description("IPN_SIGNATURE_VERIFICATION_FAIL")] IpnSignatureVerificationFail
}

public static class DescriptionAttributeExtensions
{
    public static string GetEnumDescription(this Enum e)
    {
        ArgumentNullException.ThrowIfNull(e);
        var descriptionAttribute = e.GetType().GetMember(e.ToString())[0]
                .GetCustomAttributes(typeof(DescriptionAttribute), inherit: false)[0]
            as DescriptionAttribute;

        return descriptionAttribute!.Description;
    }
}
