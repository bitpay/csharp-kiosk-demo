using BitPaySDK.Models.Invoice;
using CsharpKioskDemoDotnet.Invoice.Domain.ItemizedDetail;
using InvoiceTransaction = CsharpKioskDemoDotnet.Invoice.Domain.Transaction.InvoiceTransaction;

namespace CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;

public class InvoiceFactory
{
    private readonly InvoicePaymentFactory _invoicePaymentFactory;
    private readonly InvoiceBuyerFactory _invoiceBuyerFactory;
    private readonly InvoiceRefundFactory _invoiceRefundFactory;
    private readonly InvoiceTransactionFactory _invoiceTransactionFactory;
    private readonly InvoiceItemizedDetailFactory _invoiceItemizedDetailFactory;

    public InvoiceFactory(
        InvoicePaymentFactory invoicePaymentFactory,
        InvoiceBuyerFactory invoiceBuyerFactory,
        InvoiceRefundFactory invoiceRefundFactory,
        InvoiceTransactionFactory invoiceTransactionFactory,
        InvoiceItemizedDetailFactory invoiceItemizedDetailFactory
    )
    {
        _invoicePaymentFactory = invoicePaymentFactory;
        _invoiceBuyerFactory = invoiceBuyerFactory;
        _invoiceRefundFactory = invoiceRefundFactory;
        _invoiceTransactionFactory = invoiceTransactionFactory;
        _invoiceItemizedDetailFactory = invoiceItemizedDetailFactory;
    }

    internal Domain.Invoice Create(
        BitPaySDK.Models.Invoice.Invoice bitPayInvoice,
        string uuid
    )
    {
        var invoice = new Domain.Invoice(
            uuid: uuid,
            posData: bitPayInvoice.PosData,
            price: bitPayInvoice.Price,
            currencyCode: bitPayInvoice.Currency,
            bitPayId: bitPayInvoice.Id,
            status: bitPayInvoice.Status,
            createdDate: GetDateTime(bitPayInvoice.InvoiceTime),
            transactionSpeed: bitPayInvoice.TransactionSpeed,
            invoicePayment: _invoicePaymentFactory.Create(bitPayInvoice),
            invoiceBuyer: _invoiceBuyerFactory.Create(bitPayInvoice, bitPayInvoice.BuyerProvidedInfo),
            invoiceRefund: _invoiceRefundFactory.Create(bitPayInvoice)
        )
        {
            BitPayOrderId = bitPayInvoice.OrderId,
            ExpirationTime = GetDateTime(bitPayInvoice.ExpirationTime),
            FacadeType = "pos/invoice",
            BitPayGuid = bitPayInvoice.Guid,
            ExceptionStatus = bitPayInvoice.ExceptionStatus,
            BitPayUrl = bitPayInvoice.Url,
            RedirectUrl = bitPayInvoice.RedirectUrl,
            CloseUrl = bitPayInvoice.CloseURL,
            AcceptanceWindow = bitPayInvoice.AcceptanceWindow,
            Token = bitPayInvoice.Token,
            MerchantName = bitPayInvoice.MerchantName,
            ItemDescription = bitPayInvoice.ItemDesc,
            BillId = bitPayInvoice.BillId,
            TargetConfirmations = bitPayInvoice.TargetConfirmations,
            LowFeeDetected = Convert.ToBoolean(bitPayInvoice.LowFeeDetected),
            AutoRedirect = bitPayInvoice.AutoRedirect,
            ShopperUser = bitPayInvoice.Shopper.User,
            JsonPayProRequired = bitPayInvoice.JsonPayProRequired,
            BitPayIdRequired = bitPayInvoice.BitpayIdRequired,
            IsCancelled = bitPayInvoice.IsCancelled
        };

        invoice.AddInvoiceTransactions(
            GetInvoiceTransactions(
                invoice,
                bitPayInvoice.Transactions
            )
        );
        invoice.AddInvoiceItemizedDetails(
            GetInvoiceItemizedDetails(
                invoice,
                bitPayInvoice.ItemizedDetails
            )
        );

        return invoice;
    }

    private ICollection<InvoiceItemizedDetail> GetInvoiceItemizedDetails(
        Domain.Invoice invoice,
        ICollection<ItemizedDetails>? itemizedDetails
    )
    {
        return itemizedDetails == null
            ? new List<InvoiceItemizedDetail>()
            : itemizedDetails
                .Select(itemizedDetail => _invoiceItemizedDetailFactory.Create(invoice, itemizedDetail))
                .ToList();
    }

    private ICollection<InvoiceTransaction> GetInvoiceTransactions(
        Domain.Invoice invoice,
        ICollection<BitPaySDK.Models.Invoice.InvoiceTransaction> transactions
    )
    {
        return transactions.Select(transaction => _invoiceTransactionFactory.Create(invoice, transaction)).ToList();
    }

    private DateTime GetDateTime(long dateTime)
    {
        return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            .AddMilliseconds(dateTime)
            .ToLocalTime();
    }
}