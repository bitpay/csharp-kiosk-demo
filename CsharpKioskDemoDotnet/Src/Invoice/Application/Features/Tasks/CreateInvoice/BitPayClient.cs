using BitPaySDK;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class BitPayClient : BitPay, IBitPayClient
{
    private BitPay BitPay => this;
    
    public BitPayClient(string environment, string privateKey, Env.Tokens tokens) : base(environment, privateKey, tokens)
    {
    }

    public BitPayClient(string ConfigFilePath) : base(ConfigFilePath)
    {
    }

    public BitPayClient(IConfiguration config) : base(config)
    {
    }
    
    public virtual Task<BitPaySDK.Models.Invoice.Invoice> CreateInvoice(BitPaySDK.Models.Invoice.Invoice invoice)
    {
        return BitPay.CreateInvoice(invoice);
    }
}