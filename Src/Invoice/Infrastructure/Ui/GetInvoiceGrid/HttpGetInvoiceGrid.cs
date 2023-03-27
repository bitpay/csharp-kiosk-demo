using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.GetInvoiceDtoGrid;
using CsharpKioskDemoDotnet.Shared.BitPayProperties;
using CsharpKioskDemoDotnet.Shared.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CsharpKioskDemoDotnet.Invoice.Infrastructure.Ui.GetInvoiceGrid;

public class HttpGetInvoiceGrid : Controller
{
    private readonly BitPayProperties _bitPayProperties;
    private readonly GetInvoiceDtoGrid _getInvoiceDtoGrid;

    public HttpGetInvoiceGrid(
        IOptions<BitPayProperties> bitPayPropertiesOption,
        GetInvoiceDtoGrid getInvoiceDtoGrid
    )
    {
        _getInvoiceDtoGrid = getInvoiceDtoGrid;
        _bitPayProperties = bitPayPropertiesOption.Value;
    }

    public IActionResult Execute(int page)
    {
        var grid = _getInvoiceDtoGrid.Execute(new EntityPageNumber(page));
        var gridDto = new InvoiceGridDto(
            design: _bitPayProperties.Design,
            grid: grid
        );
        
        return View(
            "/Src/Invoice/Infrastructure/Views/InvoiceGrid/Content.cshtml",
            gridDto
        );
    }
}