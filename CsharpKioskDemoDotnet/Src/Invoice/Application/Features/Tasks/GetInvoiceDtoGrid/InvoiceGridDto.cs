// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Invoice.Application.Features.Shared;
using CsharpKioskDemoDotnet.Shared.BitPayProperties;
using CsharpKioskDemoDotnet.Shared.Domain;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.GetInvoiceDtoGrid;

public class InvoiceGridDto
{
    public Design Design { get; }
    public Page<InvoiceDto> Grid { get; }

    public InvoiceGridDto(
        Design design,
        Page<InvoiceDto> grid
    )
    {
        Design = design;
        Grid = grid;
    }
}