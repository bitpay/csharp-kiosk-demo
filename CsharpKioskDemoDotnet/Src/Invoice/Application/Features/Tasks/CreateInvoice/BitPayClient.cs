using BitPay;
using BitPay.Utils;
using Environment = BitPay.Environment;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class BitPayClient : Client, IBitPayClient
{
    private Client Client => this;
    
    public BitPayClient(PosToken token) : base(token)
    {
    }

    public BitPayClient(PosToken token, Environment environment) : base(token, environment)
    {
    }

    public BitPayClient(PrivateKey privateKey, AccessTokens accessTokens, Environment environment = Environment.Prod) : base(privateKey, accessTokens, environment)
    {
    }

    public BitPayClient(ConfigFilePath configFilePath, Environment environment = Environment.Prod) : base(configFilePath, environment)
    {
    }

    public BitPayClient(BitPay.Clients.IBitPayClient bitPayClient, string identity, AccessTokens accessTokens, IGuidGenerator guidGenerator) : base(bitPayClient, identity, accessTokens, guidGenerator)
    {
    }
    
    public virtual Task<BitPay.Models.Invoice.Invoice> CreateInvoice(BitPay.Models.Invoice.Invoice invoice)
    {
        return Client.CreateInvoice(invoice, invoice.Guid);
    }
}