// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Donation.Application.Features.Tasks.CreateDonation;
using CsharpKioskDemoDotnet.Invoice.Application.Features.Shared;
using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;
using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.GetInvoice;
using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.GetInvoiceDtoGrid;
using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.UpdateInvoice;
using CsharpKioskDemoDotnet.Invoice.Domain;
using CsharpKioskDemoDotnet.Invoice.Infrastructure.Domain;
using CsharpKioskDemoDotnet.Invoice.Infrastructure.Features.Tasks.CreateInvoice;
using CsharpKioskDemoDotnet.Invoice.Infrastructure.Features.Tasks.UpdateInvoice;
using CsharpKioskDemoDotnet.Shared.Form;
using CsharpKioskDemoDotnet.Shared.Logger.Infrastructure;
using CsharpKioskDemoDotnet.Shared.Sse;

using Lib.AspNetCore.ServerSentEvents;

using Microsoft.Extensions.Options;

namespace CsharpKioskDemoDotnet.Shared.Infrastructure;

public static class DependencyInjectionConfiguration
{
    public static void Execute(WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.AddSingleton<CreateBitPayInvoice>();
        builder.Services.AddSingleton<IGetNotificationUrl, GetNotificationUrl>();
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
        builder.Services.AddSingleton<IJsonToObjectConverter, JsonToObjectConverter>();
        builder.Services.AddSingleton<InvoiceDtoMapper>();
        builder.Services.AddSingleton<GetInvoiceWithUpdateData>();
        builder.Services.AddSingleton<ValidateUpdateData>();
        builder.Services.AddSingleton<IAfterInvoiceUpdate, SendSseNotification>();
        builder.Services.AddSingleton<UpdateInvoiceLogger>();
        builder.Services.AddSingleton<IGetValidatedParams>(_ =>
        {
            var bitPayProperties = _.GetService<IOptions<BitPayProperties.BitPayProperties>>()!;
            if (string.Equals(bitPayProperties.Value.Mode, "donation", StringComparison.OrdinalIgnoreCase)) {
                return new Donation.Application.Features.Tasks.CreateDonation.GetValidatedParams(bitPayProperties);
            }

            return new Invoice.Application.Features.Tasks.CreateInvoice.GetValidatedParams(bitPayProperties);
        });

        builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        builder.Services.AddScoped<CreateInvoice>();
        builder.Services.AddScoped<GetInvoiceDtoGrid>();
        builder.Services.AddScoped<GetInvoiceDto>();
        builder.Services.AddScoped<UpdateInvoice>();

        builder.Services
            .AddServerSentEvents<INotificationsServerSentEventsService, NotificationsServerSentEventsService>(
                options =>
                {
                    options.KeepaliveMode = ServerSentEventsKeepaliveMode.Always;
                    options.KeepaliveInterval = 15;
                }
            );
    }
}