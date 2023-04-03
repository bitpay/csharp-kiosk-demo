using CsharpKioskDemoDotnet.Shared.BitPayProperties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CsharpKioskDemoDotnet.Invoice.Infrastructure.Ui.Errors;

public class HttpGetNotFoundPage : Controller
{
    private readonly BitPayProperties _bitPayProperties;

    public HttpGetNotFoundPage(IOptions<BitPayProperties> bitPayPropertiesOption)
    {
        _bitPayProperties = bitPayPropertiesOption.Value;
    }

    // GET: 404
    [HttpGet("404")]
    public IActionResult Execute()
    {
        Response.StatusCode = 404;
        return View(
            "/Src/Invoice/Infrastructure/Views/Errors/404.cshtml",
            _bitPayProperties
        );
    }
}