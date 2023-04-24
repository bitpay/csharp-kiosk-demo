// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public interface IGetNotificationUrl
{
    string Execute(string uuid);
}