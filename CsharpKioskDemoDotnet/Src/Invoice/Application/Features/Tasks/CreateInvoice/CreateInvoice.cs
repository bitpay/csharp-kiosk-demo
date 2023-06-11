// Copyright 2023 BitPay.
// All rights reserved.

using BitPay.Exceptions;

using CsharpKioskDemoDotnet.Invoice.Domain;
using CsharpKioskDemoDotnet.Shared.Logger;
using CsharpKioskDemoDotnet.Shared.Form;

using ILogger = CsharpKioskDemoDotnet.Shared.Logger.ILogger;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class CreateInvoice
{
    private readonly IGetValidatedParams _getValidatedParams;
    private readonly CreateBitPayInvoice _createBitPayInvoice;
    private readonly InvoiceFactory _invoiceFactory;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly ILogger _logger;

    public CreateInvoice(
        IGetValidatedParams getValidatedParams,
        CreateBitPayInvoice createBitPayInvoice,
        InvoiceFactory invoiceFactory,
        IInvoiceRepository invoiceRepository,
        ILogger logger
    )
    {
        _getValidatedParams = getValidatedParams;
        _createBitPayInvoice = createBitPayInvoice;
        _invoiceFactory = invoiceFactory;
        _invoiceRepository = invoiceRepository;
        _logger = logger;
    }

    internal Domain.Invoice Execute(Dictionary<string, string?> requestParameters)
    {
        try
        {
            var validatedParams = _getValidatedParams.Execute(requestParameters);
            var uuid = Guid.NewGuid().ToString();
            var bitPayInvoice = _createBitPayInvoice.Execute(validatedParams, uuid);
            var invoice = _invoiceFactory.Create(bitPayInvoice, uuid);

            _invoiceRepository.Save(invoice);

            _logger.Info(
                LogCode.InvoiceCreateSuccess,
                "Successfully created invoice",
                new Dictionary<string, object?>
                {
                    { "id", invoice.Id }
                }
            );

            return invoice;
        }
        catch (InvoiceCreationException exception)
        {
            LogException(exception);
            throw exception.InnerException!;
        }
        catch (Exception exception)
        {
            LogException(exception);
            throw;
        }
    }

    private void LogException(Exception exception)
    {
        _logger.Error(
            LogCode.InvoiceCreateFail,
            "Failed to create invoice",
            new Dictionary<string, object?>
            {
                { "errorMessage", exception.Message },
                { "stackTrace", exception.StackTrace }
            }
        );
    }
}