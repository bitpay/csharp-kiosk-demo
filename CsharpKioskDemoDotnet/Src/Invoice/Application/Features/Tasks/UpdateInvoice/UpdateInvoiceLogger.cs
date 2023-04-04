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

    internal void LogIpnValidateFail(ValidationInvoiceUpdateDataFailed validationInvoiceUpdateDataFailed)
    {
        _logger.Error(
            LogCode.IPN_VALIDATE_FAIL,
            "Failed to validate IPN",
            new Dictionary<string, object?>
            {
                { "uuid", validationInvoiceUpdateDataFailed.Errors },
                { "stackTrace", validationInvoiceUpdateDataFailed.StackTrace }
            }
        );
    }

    internal void LogInvoiceUpdateFail(string invoiceUuid)
    {
        _logger.Error(
            LogCode.INVOICE_UPDATE_FAIL,
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
            LogCode.INVOICE_UPDATE_SUCCESS,
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
            LogCode.IPN_VALIDATE_SUCCESS,
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
            LogCode.IPN_RECEIVED,
            "Received IPN",
            updateData
        );
    }
}