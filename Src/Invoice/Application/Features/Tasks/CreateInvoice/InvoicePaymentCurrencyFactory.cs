using BitPaySDK.Models.Invoice;
using CsharpKioskDemoDotnet.Invoice.Domain.Payment;
using Newtonsoft.Json.Linq;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class InvoicePaymentCurrencyFactory
{
    internal InvoicePaymentCurrency Create(
        string currencyCode,
        InvoicePayment invoicePayment,
        BitPaySDK.Models.Invoice.Invoice bitPayInvoice
    )
    {
        var invoicePaymentTotal = new InvoicePaymentCurrency(
            invoicePayment: invoicePayment,
            currencyCode: currencyCode,
            supportedTransactionCurrency: GetSupportedTransactionCurrency(
                currencyCode,
                bitPayInvoice.SupportedTransactionCurrencies
            ),
            minerFee: GetMinerFee(currencyCode, bitPayInvoice.MinerFees)
        );

        invoicePaymentTotal.AddExchangeRates(
            GetExchangeRates(
                invoicePaymentTotal,
                bitPayInvoice.ExchangeRates
            )
        );

        return invoicePaymentTotal;
    }

    private ICollection<InvoicePaymentCurrencyExchangeRate> GetExchangeRates(
        InvoicePaymentCurrency invoicePaymentCurrency,
        JToken exchangeRates
    )
    {
        var allRates = exchangeRates.ToObject<Dictionary<string, Dictionary<string, double>>>();
        var ratesForCurrencyCode = allRates.GetValueOrDefault(
            invoicePaymentCurrency.CurrencyCode,
            new Dictionary<string, double>()
        );

        return ratesForCurrencyCode.Select(rate =>
            new InvoicePaymentCurrencyExchangeRate(
                paymentCurrency: invoicePaymentCurrency,
                currencyCode: rate.Key,
                rate: rate.Value.ToString()
            )
        ).ToList();
    }

    private InvoicePaymentCurrencyMinerFee GetMinerFee(
        string currencyCode,
        MinerFees minerFees
    )
    {
        var bitPayMinerFee = GetBitPayMinerFee(
            currencyCode,
            minerFees
        );

        if (bitPayMinerFee == null)
        {
            return new InvoicePaymentCurrencyMinerFee();
        }

        return new InvoicePaymentCurrencyMinerFee
        {
            SatoshisPerByte = bitPayMinerFee.SatoshisPerByte,
            TotalFee = bitPayMinerFee.TotalFee,
            FiatAmount = bitPayMinerFee.FiatAmount
        };
    }

    private MinerFeesItem? GetBitPayMinerFee(
        string currencyCode,
        MinerFees minerFees
    )
    {
        return currencyCode.ToLower() switch
        {
            "btc" => minerFees.Btc,
            "bch" => minerFees.Bch,
            "eth" => minerFees.Eth,
            "usdc" => minerFees.Usdc,
            "gusd" => minerFees.Gusd,
            "pax" => minerFees.Pax,
            "busd" => minerFees.Busd,
            "xrp" => minerFees.Xrp,
            "doge" => minerFees.Doge,
            "ltc" => minerFees.Ltc,
            "dai" => minerFees.Dai,
            "wbtc" => minerFees.Wbtc,
            "shib" => minerFees.Shib,
            _ => null
        };
    }

    private InvoicePaymentCurrencySupportedTransactionCurrency GetSupportedTransactionCurrency(
        string currencyCode,
        SupportedTransactionCurrencies supportedTransactionCurrencies
    )
    {
        var bitPaySupportedTransactionCurrency = GetBitPaySupportedTransactionCurrency(
            currencyCode,
            supportedTransactionCurrencies
        );

        if (bitPaySupportedTransactionCurrency == null)
        {
            return new InvoicePaymentCurrencySupportedTransactionCurrency
            {
                Enabled = false,
                Reason = null
            };
        }

        return new InvoicePaymentCurrencySupportedTransactionCurrency
        {
            Enabled = bitPaySupportedTransactionCurrency.Enabled,
            Reason = null
        };
    }

    private SupportedTransactionCurrency? GetBitPaySupportedTransactionCurrency(
        string currencyCode,
        SupportedTransactionCurrencies supportedTransactionCurrencies
    )
    {
        return currencyCode.ToLower() switch
        {
            "gusd" => supportedTransactionCurrencies.Gusd,
            "btc" => supportedTransactionCurrencies.Btc,
            "usdc" => supportedTransactionCurrencies.Usdc,
            "eth" => supportedTransactionCurrencies.Eth,
            "bch" => supportedTransactionCurrencies.Bch,
            "pax" => supportedTransactionCurrencies.Pax,
            _ => null
        };
    }
}