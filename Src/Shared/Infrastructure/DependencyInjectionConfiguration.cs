using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;
using CsharpKioskDemoDotnet.Invoice.Domain;
using CsharpKioskDemoDotnet.Invoice.Infrastructure.Domain;
using CsharpKioskDemoDotnet.Invoice.Infrastructure.Features.Tasks.CreateInvoice;
using CsharpKioskDemoDotnet.Shared.Logger.Infrastructure;

namespace CsharpKioskDemoDotnet.Shared.Infrastructure;

public static class DependencyInjectionConfiguration
{
    public static void Execute(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<CreateBitPayInvoice>();
        builder.Services.AddSingleton<IGetNotificationUrl, GetNotificationUrl>();
        builder.Services.AddSingleton<GetValidatedParams>();
        builder.Services.AddSingleton<InvoiceBuyerFactory>();
        builder.Services.AddSingleton<InvoiceBuyerProvidedInfoFactory>();
        builder.Services.AddSingleton<InvoiceFactory>();
        builder.Services.AddSingleton<InvoiceItemizedDetailFactory>();
        builder.Services.AddSingleton<InvoicePaymentCurrencyFactory>();
        builder.Services.AddSingleton<InvoicePaymentFactory>();
        builder.Services.AddSingleton<InvoiceRefundFactory>();
        builder.Services.AddSingleton<InvoiceRefundInfoFactory>();
        builder.Services.AddSingleton<InvoiceTransactionFactory>();
        builder.Services.AddSingleton<CsharpKioskDemoDotnet.Shared.Logger.ILogger, StdoutLogger>();
        builder.Services.AddSingleton<IObjectToJsonConverter, ObjectToJsonConverter>();

        builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        builder.Services.AddScoped<CreateInvoice>();
    }
    
}