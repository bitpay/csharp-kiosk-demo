using CsharpKioskDemoDotnet.Invoice.Domain;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.UpdateInvoice;

public class UpdateInvoice
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly GetInvoiceWithUpdateData _getInvoiceWithUpdateData;
    private readonly ValidateUpdateData _validateUpdateData;
    private readonly IAfterInvoiceUpdate _afterInvoiceUpdate;
    private readonly UpdateInvoiceLogger _updateInvoiceLogger;

    public UpdateInvoice(
        IInvoiceRepository invoiceRepository,
        GetInvoiceWithUpdateData getInvoiceWithUpdateData,
        IAfterInvoiceUpdate afterInvoiceUpdate,
        ValidateUpdateData validateUpdateData,
        UpdateInvoiceLogger updateInvoiceLogger
    )
    {
        _invoiceRepository = invoiceRepository;
        _getInvoiceWithUpdateData = getInvoiceWithUpdateData;
        _afterInvoiceUpdate = afterInvoiceUpdate;
        _validateUpdateData = validateUpdateData;
        _updateInvoiceLogger = updateInvoiceLogger;
    }

    public void Execute(
        string invoiceUuid,
        Dictionary<string, object?> updateData
    )
    {
        try
        {
            _updateInvoiceLogger.LogIpnReceived(updateData);
            var invoice = _invoiceRepository.FindByUuid(invoiceUuid);
            _validateUpdateData.Execute(updateData, invoice);
            var invoiceUpdate = _getInvoiceWithUpdateData.Execute(updateData, invoice);
            _updateInvoiceLogger.LogIpnValidateSuccess(invoice);
            invoice.Update(invoiceUpdate);
            _invoiceRepository.Update(invoice);
            _updateInvoiceLogger.LogInvoiceUpdateSuccess(invoice);
            _afterInvoiceUpdate.Execute(invoice, (string?)updateData["eventName"]);
        }
        catch (InvoiceNotFound invoiceNotFound)
        {
            _updateInvoiceLogger.LogInvoiceUpdateFail(invoiceUuid);
            throw;
        }
        catch (ValidationInvoiceUpdateDataFailed validationInvoiceUpdateDataFailed)
        {
            _updateInvoiceLogger.LogIpnValidateFail(validationInvoiceUpdateDataFailed);
            throw;
        }
    }
}