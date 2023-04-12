using BitPaySDK;
using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

namespace CsharpKioskDemoDotnet.Shared.Infrastructure;

public static class BitPayClientConfiguration
{
    public static void Execute(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IBitPayClient>(
            _ => new BitPayClient(builder.Configuration)
        );
    }
}