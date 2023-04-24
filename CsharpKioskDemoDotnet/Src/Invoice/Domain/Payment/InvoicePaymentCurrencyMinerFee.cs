// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.Invoice.Domain.Payment;

public class InvoicePaymentCurrencyMinerFee
{
    public long Id { get; init; }
    public decimal? SatoshisPerByte { get; set; }
    public decimal? TotalFee { get; set; }
    public decimal? FiatAmount { get; set; }
}