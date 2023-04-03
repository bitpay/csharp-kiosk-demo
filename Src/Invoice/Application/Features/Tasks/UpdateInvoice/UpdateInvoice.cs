using CsharpKioskDemoDotnet.Invoice.Domain;
using CsharpKioskDemoDotnet.Shared.Logger;
using ILogger = CsharpKioskDemoDotnet.Shared.Logger.ILogger;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.UpdateInvoice;

public class UpdateInvoice
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly GetInvoiceWithUpdateData _getInvoiceWithUpdateData;
    private readonly ValidateUpdateData _validateUpdateData;
    private readonly IAfterInvoiceUpdate _afterInvoiceUpdate;
    private readonly ILogger _logger;

    public UpdateInvoice(
        IInvoiceRepository invoiceRepository,
        GetInvoiceWithUpdateData getInvoiceWithUpdateData,
        IAfterInvoiceUpdate afterInvoiceUpdate,
        ValidateUpdateData validateUpdateData,
        ILogger logger
    )
    {
        _invoiceRepository = invoiceRepository;
        _getInvoiceWithUpdateData = getInvoiceWithUpdateData;
        _afterInvoiceUpdate = afterInvoiceUpdate;
        _validateUpdateData = validateUpdateData;
        _logger = logger;
    }

    public void Execute(
        string invoiceUuid,
        Dictionary<string, object?> updateData
    )
    {
        try
        {
            _logger.Info(
                LogCode.IPN_RECEIVED,
                "Received IPN",
                updateData
            );
            var invoice = _invoiceRepository.FindByUuid(invoiceUuid);
            _validateUpdateData.Execute(updateData, invoice);
            var invoiceUpdate = _getInvoiceWithUpdateData.Execute(updateData, invoice);
            _logger.Info(
                LogCode.IPN_VALIDATE_SUCCESS,
                "Successfully validated IPN",
                new Dictionary<string, object?>
                {
                    { "id", invoice.Id }
                }
            );
            invoice.Update(invoiceUpdate);
            _invoiceRepository.Update(invoice);
            _logger.Info(
                LogCode.INVOICE_UPDATE_SUCCESS,
                "Successfully updated invoice",
                new Dictionary<string, object?>
                {
                    { "id", invoice.Id }
                }
            );
            _afterInvoiceUpdate.Execute(invoice, (string?)updateData["eventName"]);
        }
        catch (InvoiceNotFound invoiceNotFound)
        {
            _logger.Error(
                LogCode.INVOICE_UPDATE_FAIL,
                "Failed to update invoice",
                new Dictionary<string, object?>
                {
                    { "uuid", invoiceUuid }
                }
            );
            throw;
        }
        catch (ValidationInvoiceUpdateDataFailed validationInvoiceUpdateDataFailed)
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
            throw;
        }
    }
}