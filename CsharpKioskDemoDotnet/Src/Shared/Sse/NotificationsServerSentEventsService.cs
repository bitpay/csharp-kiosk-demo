// Copyright 2023 BitPay.
// All rights reserved.

using Lib.AspNetCore.ServerSentEvents;

using Microsoft.Extensions.Options;

namespace CsharpKioskDemoDotnet.Shared.Sse;

public class NotificationsServerSentEventsService : ServerSentEventsService, INotificationsServerSentEventsService
{
    public NotificationsServerSentEventsService(IOptions<ServerSentEventsServiceOptions<NotificationsServerSentEventsService>> options)
        : base(options.ToBaseServerSentEventsServiceOptions())
    { }
}