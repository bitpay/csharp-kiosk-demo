// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Shared.Logger;

using ILogger = CsharpKioskDemoDotnet.Shared.Logger.ILogger;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.UpdateInvoice;

public class UpdateInvoiceLogger
{

    private readonly ILogger _logger;

    public UpdateInvoiceLogger(ILogger logger)
    {
        _logger = logger;
    }

    internal void LogIpnValidateFail(ValidationInvoiceUpdateDataFailedException validationInvoiceUpdateDataFailedException)
    {
        _logger.Error(
            LogCode.IpnValidateFail,
            "Failed to validate IPN",
            new Dictionary<string, object?>
            {
                { "uuid", validationInvoiceUpdateDataFailedException.Errors },
                { "stackTrace", validationInvoiceUpdateDataFailedException.StackTrace }
            }
        );
    }

    internal void LogInvoiceUpdateFail(string invoiceUuid)
    {
        _logger.Error(
            LogCode.InvoiceUpdateFail,
            "Failed to update invoice",
            new Dictionary<string, object?>
            {
                { "uuid", invoiceUuid }
            }
        );
    }

    internal void LogInvoiceUpdateSuccess(Domain.Invoice invoice)
    {
        _logger.Info(
            LogCode.InvoiceUpdateSuccess,
            "Successfully updated invoice",
            new Dictionary<string, object?>
            {
                { "id", invoice.Id }
            }
        );
    }

    internal void LogIpnValidateSuccess(Domain.Invoice invoice)
    {
        _logger.Info(
            LogCode.IpnValidateSuccess,
            "Successfully validated IPN",
            new Dictionary<string, object?>
            {
                { "id", invoice.Id }
            }
        );
    }

    internal void LogIpnReceived(Dictionary<string, object?> updateData)
    {
        _logger.Info(
            LogCode.IpnReceived,
            "Received IPN",
            updateData
        );
    }
}