using CsharpKioskDemoDotnet.Invoice.Domain.Payment;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class InvoicePaymentFactory
{
    private readonly InvoicePaymentCurrencyFactory _invoicePaymentCurrencyFactory;

    public InvoicePaymentFactory(InvoicePaymentCurrencyFactory invoicePaymentCurrencyFactory)
    {
        _invoicePaymentCurrencyFactory = invoicePaymentCurrencyFactory;
    }

    internal InvoicePayment Create(BitPaySDK.Models.Invoice.Invoice bitPayInvoice)
    {
        var invoicePayment = new InvoicePayment
        {
            AmountPaid = bitPayInvoice.AmountPaid,
            DisplayAmountPaid = Convert.ToDouble(bitPayInvoice.DisplayAmountPaid),
            NonPayProPaymentReceived = bitPayInvoice.NonPayProPaymentReceived,
            UniversalCodesPaymentString = bitPayInvoice.UniversalCodes.PaymentString,
            UniversalCodesVerificationLink = bitPayInvoice.UniversalCodes.VerificationLink,
            TransactionCurrency = bitPayInvoice.TransactionCurrency,
            UnderpaidAmount = bitPayInvoice.UnderpaidAmount,
            OverpaidAmount = bitPayInvoice.OverpaidAmount
        };

        invoicePayment.AddPaymentCurrencies(
            GetInvoicePaymentCurrencies(
                invoicePayment,
                bitPayInvoice
            )
        );

        return invoicePayment;
    }

    private ICollection<InvoicePaymentCurrency> GetInvoicePaymentCurrencies(
        InvoicePayment invoicePayment,
        BitPaySDK.Models.Invoice.Invoice bitPayInvoice
    )
    {
        return bitPayInvoice.MinerFees.GetType().GetProperties()
            .Select(property => _invoicePaymentCurrencyFactory.Create(property.Name.ToUpper(), invoicePayment, bitPayInvoice))
            .ToList();
    }
}