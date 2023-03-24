/*
 * Copyright 2023 BitPay.
 * All rights reserved.
 */

namespace CsharpKioskDemoDotnet.Invoice.Domain.Payment;

public class InvoicePaymentCurrencyMinerFee
{
    public long Id { get;}
    public double? SatoshisPerByte { get; set; }
    public double? TotalFee { get; set; }
    public double? FiatAmount { get; set; }
}