// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Invoice.Application.Features.Shared;
using CsharpKioskDemoDotnet.Invoice.Domain;
using CsharpKioskDemoDotnet.Shared.Domain;
using CsharpKioskDemoDotnet.Shared.Logger;

using ILogger = CsharpKioskDemoDotnet.Shared.Logger.ILogger;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.GetInvoiceDtoGrid;

public class GetInvoiceDtoGrid
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly InvoiceDtoMapper _invoiceDtoMapper;
    private readonly ILogger _logger;

    public GetInvoiceDtoGrid(
        IInvoiceRepository invoiceRepository,
        InvoiceDtoMapper invoiceDtoMapper,
        ILogger logger
    )
    {
        _invoiceRepository = invoiceRepository;
        _invoiceDtoMapper = invoiceDtoMapper;
        _logger = logger;
    }

    public Page<InvoiceDto> Execute(EntityPageNumber entityPageNumber)
    {
        var pagedInvoice = _invoiceRepository.FindAllPaginated(
            entityPageNumber,
            new EntityPageSize(10)
        );

        _logger.Info(
            LogCode.InvoiceGridGet,
            "Loaded invoice grid",
            new Dictionary<string, object?>
            {
                { "page", pagedInvoice.CurrentPageNumber }
            }
        );

        return pagedInvoice.MapElementsToNewType(_invoiceDtoMapper);
    }
}