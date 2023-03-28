namespace CsharpKioskDemoDotnet.Shared.Infrastructure;

public static class RouteConfiguration
{
    public static void Execute(WebApplication app)
    {
        app.MapControllerRoute(
            "default",
            "{controller=HttpGetInvoiceForm}/{action=Execute}"
        );
    }
}