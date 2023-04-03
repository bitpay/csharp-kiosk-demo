using CsharpKioskDemoDotnet.Shared.Sse;
using Lib.AspNetCore.ServerSentEvents;

namespace CsharpKioskDemoDotnet.Shared.Infrastructure;

public static class RouteConfiguration
{
    public static void Execute(WebApplication app)
    {
        app.MapControllerRoute(
            "default",
            "{controller=HttpGetInvoiceForm}/{action=Execute}"
        );
        app.MapServerSentEvents<NotificationsServerSentEventsService>("/stream-sse", new ServerSentEventsOptions
        {
            RequireAcceptHeader = true
        });
    }
}