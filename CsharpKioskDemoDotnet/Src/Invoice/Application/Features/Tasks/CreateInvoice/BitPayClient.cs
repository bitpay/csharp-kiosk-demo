// Copyright 2023 BitPay.
// All rights reserved.

using BitPay;

using Environment = BitPay.Environment;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class BitPayClient : Client, IBitPayClient
{
    private Client Client => this;

    public BitPayClient(PosToken token, Environment environment) : base(token, environment)
    {
    }

    public virtual Task<BitPay.Models.Invoice.Invoice> CreateInvoice(BitPay.Models.Invoice.Invoice invoice)
    {
        ArgumentNullException.ThrowIfNull(invoice);
        return Client.CreateInvoice(invoice, invoice.Guid);
    }
}