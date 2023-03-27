using CsharpKioskDemoDotnet.Invoice.Application.Features.Shared;
using CsharpKioskDemoDotnet.Invoice.Domain;
using CsharpKioskDemoDotnet.Shared.Domain;
using CsharpKioskDemoDotnet.Shared.Logger;
using ILogger = CsharpKioskDemoDotnet.Shared.Logger.ILogger;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.GetInvoiceDtoGrid;

public class GetInvoiceDtoGrid
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly InvoiceMapperDto _invoiceMapperDto;
    private readonly ILogger _logger;

    public GetInvoiceDtoGrid(
        IInvoiceRepository invoiceRepository,
        InvoiceMapperDto invoiceMapperDto,
        ILogger logger
    )
    {
        _invoiceRepository = invoiceRepository;
        _invoiceMapperDto = invoiceMapperDto;
        _logger = logger;
    }

    public Page<InvoiceDto> Execute(EntityPageNumber entityPageNumber)
    {
        var pagedInvoice = _invoiceRepository.findAllPaginated(
            entityPageNumber,
            new EntityPageSize(10)
        );

        _logger.Info(
            LogCode.INVOICE_GRID_GET,
            "Loaded invoice grid",
            new Dictionary<string, object?>
            {
                { "page", pagedInvoice.CurrentPageNumber }
            }
        );

        return pagedInvoice.MapElementsToNewType(_invoiceMapperDto);
    }
}