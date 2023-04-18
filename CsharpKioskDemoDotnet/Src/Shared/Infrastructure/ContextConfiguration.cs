// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Invoice.Infrastructure.Domain;

using Microsoft.EntityFrameworkCore;

namespace CsharpKioskDemoDotnet.Shared.Infrastructure;

public abstract class ContextConfiguration
{
    public static void Execute(WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.AddDbContext<MvcInvoiceContext>(
            options => options.UseSqlite(
                builder.Configuration.GetConnectionString("MvcInvoiceContext")
                ?? throw new InvalidOperationException("Connection string 'MvcInvoiceContext' not found.")
            )
        );
    }
}