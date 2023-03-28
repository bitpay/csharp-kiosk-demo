using CsharpKioskDemoDotnet.Invoice.Application.Features.Shared;
using CsharpKioskDemoDotnet.Invoice.Domain;
using CsharpKioskDemoDotnet.Shared.Logger;
using ILogger = CsharpKioskDemoDotnet.Shared.Logger.ILogger;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.GetInvoice;

public class GetInvoiceDto
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly InvoiceMapperDto _invoiceMapperDto;
    private readonly ILogger _logger;

    public GetInvoiceDto(
        IInvoiceRepository invoiceRepository,
        InvoiceMapperDto invoiceMapperDto,
        ILogger logger
    )
    {
        _invoiceRepository = invoiceRepository;
        _invoiceMapperDto = invoiceMapperDto;
        _logger = logger;
    }

    public InvoiceDto Execute(long invoiceId)
    {
        var invoice = _invoiceRepository.FindById(invoiceId);
        _logger.Info(
            LogCode.INVOICE_GET,
            "Loaded invoice",
            new Dictionary<string, object?>
            {
                { "id", invoice.Id }
            }
        );

        return _invoiceMapperDto.Execute(invoice);
    }
}