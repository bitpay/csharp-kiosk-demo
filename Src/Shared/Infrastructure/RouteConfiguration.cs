namespace CsharpKioskDemoDotnet.Shared.Infrastructure;

public static class RouteConfiguration
{
    public static void Execute(WebApplication app)
    {
        app.MapControllerRoute(
            "default",
            "{controller=HttpGetInvoiceForm}/{action=Execute}"
        );
        app.MapControllerRoute(
            name: "invoice",
            pattern: "invoice",
            defaults: new { controller = "HttpCreateInvoice", action = "Execute" }
        );
        app.MapControllerRoute(
            name: "invoices",
            pattern: "invoices",
            defaults: new { controller = "HttpGetInvoiceGrid", action = "Execute" }
        );
    }
}