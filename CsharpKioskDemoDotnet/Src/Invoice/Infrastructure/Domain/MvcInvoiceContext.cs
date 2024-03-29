// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Invoice.Domain.Payment;

using Microsoft.EntityFrameworkCore;

namespace CsharpKioskDemoDotnet.Invoice.Infrastructure.Domain;

public class MvcInvoiceContext : DbContext
{
    public MvcInvoiceContext(DbContextOptions<MvcInvoiceContext> options) : base(options)
    {
    }

    public DbSet<Invoice.Domain.Invoice> Invoices { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Buyer.InvoiceBuyer", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER")
                .HasColumnName("id");

            b.Property<string>("Address1")
                .HasColumnType("TEXT")
                .HasColumnName("address1");

            b.Property<string>("Address2")
                .HasColumnType("TEXT")
                .HasColumnName("address2");

            b.Property<string>("BuyerProvidedEmail")
                .HasColumnType("TEXT")
                .HasColumnName("buyer_provided_email");

            b.Property<string>("City")
                .HasColumnType("TEXT")
                .HasColumnName("city");

            b.Property<string>("Country")
                .HasColumnType("TEXT")
                .HasColumnName("country");

            b.Property<string>("Email")
                .HasColumnType("TEXT")
                .HasColumnName("email");

            b.Property<long>("InvoiceBuyerProvidedInfoId")
                .HasColumnType("INTEGER")
                .HasColumnName("invoice_buyer_provided_info_id");

            b.Property<string>("Name")
                .HasColumnType("TEXT")
                .HasColumnName("name");

            b.Property<bool?>("Notify")
                .HasColumnType("INTEGER")
                .HasColumnName("notify");

            b.Property<string>("Phone")
                .HasColumnType("TEXT")
                .HasColumnName("phone");

            b.Property<string>("PostalCode")
                .HasColumnType("TEXT")
                .HasColumnName("postal_code");

            b.Property<string>("Region")
                .HasColumnType("TEXT")
                .HasColumnName("region");

            b.HasKey("Id");

            b.HasIndex("InvoiceBuyerProvidedInfoId");

            b.ToTable("invoice_buyer");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Buyer.InvoiceBuyerProvidedInfo", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER")
                .HasColumnName("id");

            b.Property<string>("EmailAddress")
                .HasColumnType("TEXT")
                .HasColumnName("email_address");

            b.Property<string>("Name")
                .HasColumnType("TEXT")
                .HasColumnName("name");

            b.Property<string>("PhoneNumber")
                .HasColumnType("TEXT")
                .HasColumnName("phone_number");

            b.Property<string>("SelectedTransactionCurrency")
                .HasColumnType("TEXT")
                .HasColumnName("selected_transaction_currency");

            b.Property<string>("SelectedWallet")
                .HasColumnType("TEXT")
                .HasColumnName("selected_wallet");

            b.Property<string>("Sms")
                .HasColumnType("TEXT")
                .HasColumnName("sms");

            b.Property<bool?>("SmsVerified")
                .HasColumnType("INTEGER")
                .HasColumnName("sms_verified");

            b.HasKey("Id");

            b.ToTable("invoice_buyer_provided_info");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Invoice", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER")
                .HasColumnName("id");

            b.Property<long?>("AcceptanceWindow")
                .HasColumnType("INTEGER")
                .HasColumnName("acceptance_window");

            b.Property<bool?>("AutoRedirect")
                .HasColumnType("INTEGER")
                .HasColumnName("auto_redirect");

            b.Property<string>("BillId")
                .HasColumnType("TEXT")
                .HasColumnName("bill_id");

            b.Property<string>("BitPayGuid")
                .HasColumnType("TEXT")
                .HasColumnName("bitpay_guid");

            b.Property<string>("BitPayId")
                .IsRequired()
                .HasColumnType("TEXT")
                .HasColumnName("bitpay_id");

            b.Property<bool?>("BitPayIdRequired")
                .HasColumnType("INTEGER")
                .HasColumnName("bitpay_id_required");

            b.Property<string>("BitPayOrderId")
                .HasColumnType("TEXT")
                .HasColumnName("bitpay_order_id");

            b.Property<string>("BitPayUrl")
                .HasColumnType("TEXT")
                .HasColumnName("bitpay_url");

            b.Property<string>("CloseUrl")
                .HasColumnType("TEXT")
                .HasColumnName("close_url");

            b.Property<DateTime>("CreatedDate")
                .HasColumnType("TEXT")
                .HasColumnName("created_date");

            b.Property<string>("CurrencyCode")
                .IsRequired()
                .HasColumnType("TEXT")
                .HasColumnName("currency_code");

            b.Property<string>("ExceptionStatus")
                .HasColumnType("TEXT")
                .HasColumnName("exception_status");

            b.Property<DateTime?>("ExpirationTime")
                .HasColumnType("TEXT")
                .HasColumnName("expiration_time");

            b.Property<string>("FacadeType")
                .HasColumnType("TEXT")
                .HasColumnName("facade_type");

            b.Property<long>("InvoiceBuyerId")
                .HasColumnType("INTEGER")
                .HasColumnName("invoice_buyer_id");

            b.Property<long>("InvoicePaymentId")
                .HasColumnType("INTEGER")
                .HasColumnName("invoice_payment_id");

            b.Property<long>("InvoiceRefundId")
                .HasColumnType("INTEGER")
                .HasColumnName("invoice_refund_id");

            b.Property<bool?>("IsCancelled")
                .HasColumnType("INTEGER")
                .HasColumnName("is_cancelled");

            b.Property<string>("ItemDescription")
                .HasColumnType("TEXT")
                .HasColumnName("item_description");

            b.Property<bool?>("JsonPayProRequired")
                .HasColumnType("INTEGER")
                .HasColumnName("json_pay_pro_required");

            b.Property<bool?>("LowFeeDetected")
                .HasColumnType("INTEGER")
                .HasColumnName("low_fee_detected");

            b.Property<string>("MerchantName")
                .HasColumnType("TEXT")
                .HasColumnName("merchant_name");

            b.Property<string>("PosData")
                .IsRequired()
                .HasColumnType("TEXT")
                .HasColumnName("pos_data");

            b.Property<decimal>("Price")
                .HasColumnType("REAL")
                .HasColumnName("price");

            b.Property<string>("RedirectUrl")
                .HasColumnType("TEXT")
                .HasColumnName("redirect_url");

            b.Property<string>("ShopperUser")
                .HasColumnType("TEXT")
                .HasColumnName("shopper_user");

            b.Property<string>("Status")
                .IsRequired()
                .HasColumnType("TEXT")
                .HasColumnName("status");

            b.Property<int?>("TargetConfirmations")
                .HasColumnType("INTEGER")
                .HasColumnName("target_confirmations");

            b.Property<string>("Token")
                .HasColumnType("TEXT")
                .HasColumnName("token");

            b.Property<string>("TransactionSpeed")
                .IsRequired()
                .HasColumnType("TEXT")
                .HasColumnName("transaction_speed");

            b.Property<string>("Uuid")
                .IsRequired()
                .HasColumnType("TEXT")
                .HasColumnName("uuid");

            b.HasKey("Id");

            b.HasIndex("InvoiceBuyerId");

            b.HasIndex("InvoicePaymentId");

            b.HasIndex("InvoiceRefundId");

            b.ToTable("invoice");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.ItemizedDetail.InvoiceItemizedDetail", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER")
                .HasColumnName("id");

            b.Property<double?>("Amount")
                .HasColumnType("REAL")
                .HasColumnName("amount");

            b.Property<string>("Description")
                .HasColumnType("TEXT")
                .HasColumnName("description");

            b.Property<long>("InvoiceId")
                .HasColumnType("INTEGER")
                .HasColumnName("invoice_id");

            b.Property<bool?>("IsFee")
                .HasColumnType("INTEGER")
                .HasColumnName("is_fee");

            b.HasKey("Id");

            b.HasIndex("InvoiceId");

            b.ToTable("invoice_itemized_detail");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Payment.InvoicePayment", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER")
                .HasColumnName("id");

            b.Property<decimal?>("AmountPaid")
                .HasColumnType("REAL")
                .HasColumnName("amount_paid");

            b.Property<string?>("DisplayAmountPaid")
                .HasColumnType("REAL")
                .HasColumnName("display_amount_paid");

            b.Property<bool?>("NonPayProPaymentReceived")
                .HasColumnType("INTEGER")
                .HasColumnName("non_pay_pro_payment_received");

            b.Property<double?>("OverpaidAmount")
                .HasColumnType("REAL")
                .HasColumnName("overpaid_amount");

            b.Property<string>("TransactionCurrency")
                .HasColumnType("TEXT")
                .HasColumnName("transaction_currency");

            b.Property<double?>("UnderpaidAmount")
                .HasColumnType("REAL")
                .HasColumnName("underpaid_amount");

            b.Property<string>("UniversalCodesPaymentString")
                .HasColumnType("TEXT")
                .HasColumnName("universal_codes_payment_string");

            b.Property<string>("UniversalCodesVerificationLink")
                .HasColumnType("TEXT")
                .HasColumnName("universal_codes_verification_link");

            b.HasKey("Id");

            b.ToTable("invoice_payment");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Payment.InvoicePaymentCurrency", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER")
                .HasColumnName("id");

            b.Property<string>("CurrencyCode")
                .IsRequired()
                .HasColumnType("TEXT")
                .HasColumnName("currency_code");

            b.Property<string>("DisplaySubtotal")
                .HasColumnType("TEXT")
                .HasColumnName("display_subtotal");

            b.Property<string>("DisplayTotal")
                .HasColumnType("TEXT")
                .HasColumnName("display_total");

            b.Property<long>("InvoicePaymentId")
                .HasColumnType("INTEGER")
                .HasColumnName("invoice_payment_id");

            b.Property<long>("MinerFeeId")
                .HasColumnType("INTEGER")
                .HasColumnName("miner_fee_id");

            b.Property<string>("Subtotal")
                .HasColumnType("TEXT")
                .HasColumnName("subtotal");

            b.Property<long>("SupportedTransactionCurrencyId")
                .HasColumnType("INTEGER")
                .HasColumnName("supported_transaction_currency_id");

            b.Property<string>("Total")
                .HasColumnType("TEXT")
                .HasColumnName("total");

            b.HasKey("Id");

            b.HasIndex("InvoicePaymentId");

            b.HasIndex("MinerFeeId");

            b.HasIndex("SupportedTransactionCurrencyId");

            b.ToTable("invoice_payment_currency");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Payment.InvoicePaymentCurrencyCode", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER")
                .HasColumnName("id");

            b.Property<string>("PaymentCode")
                .IsRequired()
                .HasColumnType("TEXT")
                .HasColumnName("payment_code");

            b.Property<string>("PaymentCodeUrl")
                .HasColumnType("TEXT")
                .HasColumnName("payment_code_url");

            b.Property<long>("PaymentCurrencyId")
                .HasColumnType("INTEGER")
                .HasColumnName("invoice_payment_currency_id");

            b.HasKey("Id");

            b.HasIndex("PaymentCurrencyId");

            b.ToTable("invoice_payment_currency_code");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Payment.InvoicePaymentCurrencyExchangeRate", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER")
                .HasColumnName("id");

            b.Property<string>("CurrencyCode")
                .IsRequired()
                .HasColumnType("TEXT")
                .HasColumnName("currency_code");

            b.Property<long>("PaymentCurrencyId")
                .HasColumnType("INTEGER")
                .HasColumnName("invoice_payment_currency_id");

            b.Property<string>("Rate")
                .IsRequired()
                .HasColumnType("TEXT")
                .HasColumnName("rate");

            b.HasKey("Id");

            b.HasIndex("PaymentCurrencyId");

            b.ToTable("invoice_payment_currency_exchange_rate");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Payment.InvoicePaymentCurrencyMinerFee", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER")
                .HasColumnName("id");

            b.Property<decimal?>("FiatAmount")
                .HasColumnType("REAL")
                .HasColumnName("fiat_amount");

            b.Property<decimal?>("SatoshisPerByte")
                .HasColumnType("REAL")
                .HasColumnName("satoshis_per_byte");

            b.Property<decimal?>("TotalFee")
                .HasColumnType("REAL")
                .HasColumnName("total_fee");

            b.HasKey("Id");

            b.ToTable("invoice_payment_currency_miner_fee");
        });

        modelBuilder.Entity(
            "CsharpKioskDemoDotnet.Invoice.Domain.Payment.InvoicePaymentCurrencySupportedTransactionCurrency", b =>
            {
                b.Property<long>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("INTEGER")
                    .HasColumnName("id");

                b.Property<bool?>("Enabled")
                    .HasColumnType("INTEGER")
                    .HasColumnName("enabled");

                b.Property<string>("Reason")
                    .HasColumnType("TEXT")
                    .HasColumnName("reason");

                b.HasKey("Id");

                b.ToTable("invoice_payment_currency_supported_transaction_currency");
            });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Refund.InvoiceRefund", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER")
                .HasColumnName("id");

            b.Property<bool?>("AddressRequestPending")
                .IsRequired()
                .HasColumnType("INTEGER")
                .HasColumnName("address_request_pending");

            b.Property<string>("AddressesJson")
                .IsRequired()
                .HasColumnType("TEXT")
                .HasColumnName("addresses_json");

            b.HasKey("Id");

            b.ToTable("invoice_refund");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Refund.InvoiceRefundInfo", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER")
                .HasColumnName("id");

            b.Property<string>("CurrencyCode")
                .IsRequired()
                .HasColumnType("TEXT")
                .HasColumnName("currency_code");

            b.Property<long>("InvoiceRefundId")
                .HasColumnType("INTEGER")
                .HasColumnName("invoice_refund_id");

            b.Property<string>("SupportRequest")
                .HasColumnType("TEXT")
                .HasColumnName("support_request");

            b.HasKey("Id");

            b.HasIndex("InvoiceRefundId");

            b.ToTable("invoice_refund_info");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Refund.InvoiceRefundInfoAmount", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER")
                .HasColumnName("id");

            b.Property<decimal?>("Amount")
                .HasColumnType("REAL")
                .HasColumnName("amount");

            b.Property<string>("CurrencyCode")
                .IsRequired()
                .HasColumnType("TEXT")
                .HasColumnName("currency_code");

            b.Property<long>("InvoiceRefundInfoId")
                .HasColumnType("INTEGER")
                .HasColumnName("invoice_refund_info_id");

            b.HasKey("Id");

            b.HasIndex("InvoiceRefundInfoId");

            b.ToTable("invoice_refund_info_amount");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Transaction.InvoiceTransaction", b =>
        {
            b.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("INTEGER")
                .HasColumnName("id");

            b.Property<decimal?>("Amount")
                .HasColumnType("REAL")
                .HasColumnName("amount");

            b.Property<int?>("Confirmations")
                .HasColumnType("INTEGER")
                .HasColumnName("confirmations");

            b.Property<long>("InvoiceId")
                .HasColumnType("INTEGER")
                .HasColumnName("invoice_id");

            b.Property<DateTime?>("ReceivedTime")
                .HasColumnType("TEXT")
                .HasColumnName("received_time");

            b.Property<string>("Txid")
                .HasColumnType("TEXT")
                .HasColumnName("txid");

            b.HasKey("Id");

            b.HasIndex("InvoiceId");

            b.ToTable("invoice_transaction");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Buyer.InvoiceBuyer", b =>
        {
            b.HasOne("CsharpKioskDemoDotnet.Invoice.Domain.Buyer.InvoiceBuyerProvidedInfo", "InvoiceBuyerProvidedInfo")
                .WithMany()
                .HasForeignKey("InvoiceBuyerProvidedInfoId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("InvoiceBuyerProvidedInfo");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Invoice", b =>
        {
            b.HasOne("CsharpKioskDemoDotnet.Invoice.Domain.Buyer.InvoiceBuyer", "InvoiceBuyer")
                .WithMany()
                .HasForeignKey("InvoiceBuyerId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("CsharpKioskDemoDotnet.Invoice.Domain.Payment.InvoicePayment", "InvoicePayment")
                .WithMany()
                .HasForeignKey("InvoicePaymentId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("CsharpKioskDemoDotnet.Invoice.Domain.Refund.InvoiceRefund", "InvoiceRefund")
                .WithMany()
                .HasForeignKey("InvoiceRefundId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("InvoiceBuyer");

            b.Navigation("InvoicePayment");

            b.Navigation("InvoiceRefund");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.ItemizedDetail.InvoiceItemizedDetail", b =>
        {
            b.HasOne("CsharpKioskDemoDotnet.Invoice.Domain.Invoice", "Invoice")
                .WithMany("InvoiceItemizedDetails")
                .HasForeignKey("InvoiceId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Invoice");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Payment.InvoicePaymentCurrency", b =>
        {
            b.HasOne("CsharpKioskDemoDotnet.Invoice.Domain.Payment.InvoicePayment", "InvoicePayment")
                .WithMany("PaymentCurrencies")
                .HasForeignKey("InvoicePaymentId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("CsharpKioskDemoDotnet.Invoice.Domain.Payment.InvoicePaymentCurrencyMinerFee", "MinerFee")
                .WithMany()
                .HasForeignKey("MinerFeeId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.HasOne("CsharpKioskDemoDotnet.Invoice.Domain.Payment.InvoicePaymentCurrencySupportedTransactionCurrency",
                    "SupportedTransactionCurrency")
                .WithMany()
                .HasForeignKey("SupportedTransactionCurrencyId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("InvoicePayment");

            b.Navigation("MinerFee");

            b.Navigation("SupportedTransactionCurrency");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Payment.InvoicePaymentCurrencyCode", b =>
        {
            b.HasOne("CsharpKioskDemoDotnet.Invoice.Domain.Payment.InvoicePaymentCurrency", "PaymentCurrency")
                .WithMany("CurrencyCodes")
                .HasForeignKey("PaymentCurrencyId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("PaymentCurrency");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Payment.InvoicePaymentCurrencyExchangeRate", b =>
        {
            b.HasOne("CsharpKioskDemoDotnet.Invoice.Domain.Payment.InvoicePaymentCurrency", "PaymentCurrency")
                .WithMany("ExchangeRates")
                .HasForeignKey("PaymentCurrencyId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("PaymentCurrency");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Refund.InvoiceRefundInfo", b =>
        {
            b.HasOne("CsharpKioskDemoDotnet.Invoice.Domain.Refund.InvoiceRefund", "InvoiceRefund")
                .WithMany("RefundInfo")
                .HasForeignKey("InvoiceRefundId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("InvoiceRefund");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Refund.InvoiceRefundInfoAmount", b =>
        {
            b.HasOne("CsharpKioskDemoDotnet.Invoice.Domain.Refund.InvoiceRefundInfo", "InvoiceRefundInfo")
                .WithMany("InvoiceRefundInfoAmounts")
                .HasForeignKey("InvoiceRefundInfoId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("InvoiceRefundInfo");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Transaction.InvoiceTransaction", b =>
        {
            b.HasOne("CsharpKioskDemoDotnet.Invoice.Domain.Invoice", "Invoice")
                .WithMany("InvoiceTransactions")
                .HasForeignKey("InvoiceId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            b.Navigation("Invoice");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Invoice", b =>
        {
            b.Navigation("InvoiceItemizedDetails");

            b.Navigation("InvoiceTransactions");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Payment.InvoicePayment",
            b => { b.Navigation("PaymentCurrencies"); });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Payment.InvoicePaymentCurrency", b =>
        {
            b.Navigation("CurrencyCodes");

            b.Navigation("ExchangeRates");
        });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Refund.InvoiceRefund",
            b => { b.Navigation("RefundInfo"); });

        modelBuilder.Entity("CsharpKioskDemoDotnet.Invoice.Domain.Refund.InvoiceRefundInfo",
            b => { b.Navigation("InvoiceRefundInfoAmounts"); });
    }
}