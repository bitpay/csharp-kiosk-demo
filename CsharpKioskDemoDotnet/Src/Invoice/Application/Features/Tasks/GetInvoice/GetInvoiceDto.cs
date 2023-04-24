// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Invoice.Application.Features.Shared;
using CsharpKioskDemoDotnet.Invoice.Domain;
using CsharpKioskDemoDotnet.Shared.Logger;

using ILogger = CsharpKioskDemoDotnet.Shared.Logger.ILogger;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.GetInvoice;

public class GetInvoiceDto
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly InvoiceDtoMapper _invoiceDtoMapper;
    private readonly ILogger _logger;

    public GetInvoiceDto(
        IInvoiceRepository invoiceRepository,
        InvoiceDtoMapper invoiceDtoMapper,
        ILogger logger
    )
    {
        _invoiceRepository = invoiceRepository;
        _invoiceDtoMapper = invoiceDtoMapper;
        _logger = logger;
    }

    public InvoiceDto Execute(long invoiceId)
    {
        var invoice = _invoiceRepository.FindById(invoiceId);
        _logger.Info(
            LogCode.InvoiceGet,
            "Loaded invoice",
            new Dictionary<string, object?>
            {
                { "id", invoice.Id }
            }
        );

        return _invoiceDtoMapper.Execute(invoice);
    }
}