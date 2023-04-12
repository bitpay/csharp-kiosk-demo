using System.Collections;
using CsharpKioskDemoDotnet.Invoice.Domain.Buyer;
using CsharpKioskDemoDotnet.Invoice.Domain.ItemizedDetail;
using CsharpKioskDemoDotnet.Invoice.Domain.Payment;
using CsharpKioskDemoDotnet.Invoice.Domain.Refund;
using CsharpKioskDemoDotnet.Invoice.Domain.Transaction;

namespace CsharpKioskDemoDotnet.Invoice.Domain;

public class Invoice
{
    public long Id { get; }
    public string Uuid { get; set; }
    public string PosData { get; set; }
    public double Price { get; set; }
    public string CurrencyCode { get; set; }
    public string BitPayId { get; set; }
    public string Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? BitPayOrderId { get; set; }
    public DateTime? ExpirationTime { get; set; }
    public string? FacadeType { get; set; }
    public string? BitPayGuid { get; set; }
    public string? ExceptionStatus { get; set; }
    public string? BitPayUrl { get; set; }
    public string? RedirectUrl { get; set; }
    public string? CloseUrl { get; set; }
    public long? AcceptanceWindow { get; set; }
    public string? Token { get; set; }
    public string? MerchantName { get; set; }
    public string? ItemDescription { get; set; }
    public string? BillId { get; set; }
    public int? TargetConfirmations { get; set; }
    public bool? LowFeeDetected { get; set; }
    public bool? AutoRedirect { get; set; }
    public string? ShopperUser { get; set; }
    public bool? JsonPayProRequired { get; set; }
    public bool? BitPayIdRequired { get; set; }
    public bool? IsCancelled { get; set; }
    public string TransactionSpeed { get; set; }
    public InvoicePayment InvoicePayment { get; set; }
    public InvoiceBuyer InvoiceBuyer { get; set; }
    public InvoiceRefund InvoiceRefund { get; set; }
    public ICollection<InvoiceTransaction> InvoiceTransactions { get; } = new List<InvoiceTransaction>();
    public ICollection<InvoiceItemizedDetail> InvoiceItemizedDetails { get; } = new List<InvoiceItemizedDetail>();

    public Invoice(
        string uuid,
        string posData,
        double price,
        string currencyCode,
        string bitPayId,
        string status,
        DateTime createdDate,
        string transactionSpeed,
        InvoicePayment invoicePayment,
        InvoiceBuyer invoiceBuyer,
        InvoiceRefund invoiceRefund
    )
    {
        Uuid = uuid;
        PosData = posData;
        Price = price;
        CurrencyCode = currencyCode;
        BitPayId = bitPayId;
        Status = status;
        CreatedDate = createdDate;
        TransactionSpeed = transactionSpeed;
        InvoicePayment = invoicePayment;
        InvoiceBuyer = invoiceBuyer;
        InvoiceRefund = invoiceRefund;
    }

    internal Invoice()
    {
    }

    public void AddInvoiceTransactions(ICollection<InvoiceTransaction> invoiceTransactions)
    {
        foreach (var item in invoiceTransactions)
        {
            InvoiceTransactions.Add(item);
        }
    }

    public void AddInvoiceItemizedDetails(ICollection<InvoiceItemizedDetail> invoiceItemizedDetails)
    {
        foreach (var item in invoiceItemizedDetails)
        {
            InvoiceItemizedDetails.Add(item);
        }
    }

    public void Update(Invoice invoice)
    {
        Price = invoice.Price;
        CurrencyCode = invoice.CurrencyCode;
        Status = invoice.Status;
        BitPayOrderId = invoice.BitPayOrderId;
        ExpirationTime = invoice.ExpirationTime;
        ExceptionStatus = invoice.ExceptionStatus;
        BitPayUrl = invoice.BitPayUrl;
        InvoicePayment.Update(invoice.InvoicePayment);
        InvoiceBuyer.Update(invoice.InvoiceBuyer);
    }
}