// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Shared.BitPayProperties;
using CsharpKioskDemoDotnet.Shared.Form;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CsharpKioskDemoDotnet.Donation.Infrastructure.Ui.GetDonationForm;

public class HttpGetDonationForm : Controller
{
    private readonly BitPayProperties _bitPayProperties;

    public HttpGetDonationForm(IOptions<BitPayProperties> bitPayPropertiesOption)
    {
        ArgumentNullException.ThrowIfNull(bitPayPropertiesOption);
        _bitPayProperties = bitPayPropertiesOption.Value;
    }

    [HttpGet]
    public IActionResult Execute()
    {
        return View(
            "/Src/Donation/Infrastructure/Views/DonationForm/Content.cshtml",
            new FormDto(_bitPayProperties.Design)
            {
                Error = (string?)TempData["Error"]
            }
        );
    }
}