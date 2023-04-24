// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

namespace CsharpKioskDemoDotnet.Invoice.Infrastructure.Features.Tasks.CreateInvoice;

public class GetNotificationUrl : IGetNotificationUrl
{

    private readonly IConfiguration _configuration;

    public GetNotificationUrl(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Execute(string uuid)
    {
        return $"{_configuration.GetValue<string>("AppUrl")}/invoices/{uuid}";
    }
}