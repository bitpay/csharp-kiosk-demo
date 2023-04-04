using BitPaySDK;

namespace CsharpKioskDemoDotnet.Shared.Infrastructure;

public static class BitPayClientConfiguration
{
    public static void Execute(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton(new BitPay(builder.Configuration));
    }
}