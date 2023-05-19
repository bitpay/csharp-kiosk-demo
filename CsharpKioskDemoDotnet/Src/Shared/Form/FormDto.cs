// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Shared.BitPayProperties;

namespace CsharpKioskDemoDotnet.Shared.Form;

public class FormDto
{
    public Design Design { get; }
    public string? Error { get; set; }

    public FormDto(Design design)
    {
        Design = design;
    }
}