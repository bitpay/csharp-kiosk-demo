using BitPay;
using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;
using Environment = BitPay.Environment;

namespace CsharpKioskDemoDotnet.Shared.Infrastructure;

public static class BitPayClientConfiguration
{
    public static void Execute(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IBitPayClient>(
            _ => new BitPayClient(
                new PosToken(builder.Configuration.GetSection("BitPay:Token").Value),
                Environment.Test
            )
        );
    }
}