// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Shared.Sse;

using Lib.AspNetCore.ServerSentEvents;

using Microsoft.Extensions.Options;

namespace CsharpKioskDemoDotnet.Shared.Infrastructure;

public static class RouteConfiguration
{
    public static void Execute(WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        app.MapServerSentEvents<NotificationsServerSentEventsService>("/stream-sse", new ServerSentEventsOptions
        {
            RequireAcceptHeader = true
        });

        var bitPayProperties = app.Services.GetService<IOptions<BitPayProperties.BitPayProperties>>()!;
        if (string.Equals(bitPayProperties.Value.Mode, "donation", StringComparison.OrdinalIgnoreCase))
        {
            app.MapControllerRoute(
                "default",
                "{controller=HttpGetDonationForm}/{action=Execute}"
            );
            return;
        }

        app.MapControllerRoute(
            "default",
            "{controller=HttpGetInvoiceForm}/{action=Execute}"
        );
    }
}