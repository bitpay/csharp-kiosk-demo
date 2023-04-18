// Copyright 2023 BitPay.
// All rights reserved.

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CsharpKioskDemoDotnet.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.CreateTable(
                name: "invoice_buyer_provided_info",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "varchar(255)", nullable: true),
                    phone_number = table.Column<string>(type: "varchar(255)", nullable: true),
                    selected_transaction_currency = table.Column<string>(type: "varchar(10)", nullable: true),
                    email_address = table.Column<string>(type: "varchar(255)", nullable: true),
                    selected_wallet = table.Column<string>(type: "varchar(255)", nullable: true),
                    sms = table.Column<string>(type: "varchar(255)", nullable: true),
                    sms_verified = table.Column<bool>(type: "BOOLEAN", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceBuyerProvidedInfo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "invoice_payment",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    amount_paid = table.Column<double>(type: "DOUBLE", nullable: true),
                    display_amount_paid = table.Column<double>(type: "DOUBLE", nullable: true),
                    underpaid_amount = table.Column<double>(type: "DOUBLE", nullable: true),
                    overpaid_amount = table.Column<double>(type: "DOUBLE", nullable: true),
                    non_pay_pro_payment_received = table.Column<bool>(type: "BOOLEAN", nullable: true),
                    universal_codes_payment_string = table.Column<string>(type: "varchar(255)", nullable: true),
                    universal_codes_verification_link = table.Column<string>(type: "varchar(255)", nullable: true),
                    transaction_currency = table.Column<string>(type: "varchar(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePayment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "invoice_payment_currency_miner_fee",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    satoshis_per_byte = table.Column<double>(type: "DOUBLE", nullable: true),
                    total_fee = table.Column<double>(type: "DOUBLE", nullable: true),
                    fiat_amount = table.Column<double>(type: "DOUBLE", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePaymentCurrencyMinerFee", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "invoice_payment_currency_supported_transaction_currency",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    enabled = table.Column<bool>(type: "BOOLEAN", nullable: true),
                    reason = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePaymentCurrencySupportedTransactionCurrency", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "invoice_refund",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    addresses_json = table.Column<string>(type: "TEXT", nullable: true),
                    address_request_pending = table.Column<bool>(type: "BOOLEAN", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceRefund", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "invoice_buyer",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "varchar(255)", nullable: true),
                    address1 = table.Column<string>(type: "varchar(255)", nullable: true),
                    address2 = table.Column<string>(type: "varchar(255)", nullable: true),
                    city = table.Column<string>(type: "varchar(255)", nullable: true),
                    region = table.Column<string>(type: "varchar(255)", nullable: true),
                    postal_code = table.Column<string>(type: "varchar(255)", nullable: true),
                    country = table.Column<string>(type: "varchar(2)", nullable: true),
                    email = table.Column<string>(type: "varchar(255)", nullable: true),
                    phone = table.Column<string>(type: "varchar(255)", nullable: true),
                    notify = table.Column<bool>(type: "BOOLEAN", nullable: true),
                    buyer_provided_email = table.Column<string>(type: "varchar(255)", nullable: true),
                    invoice_buyer_provided_info_id = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceBuyer", x => x.id);
                    table.ForeignKey(
                        name: "fk_ib_invoice_buyer_provided_info_id",
                        column: x => x.invoice_buyer_provided_info_id,
                        principalTable: "invoice_buyer_provided_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoice_payment_currency",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    invoice_payment_id = table.Column<long>(type: "INTEGER", nullable: false),
                    currency_code = table.Column<string>(type: "varchar(10)", nullable: false),
                    total = table.Column<string>(type: "varchar(255)", nullable: true),
                    subtotal = table.Column<string>(type: "varchar(255)", nullable: true),
                    display_total = table.Column<string>(type: "varchar(255)", nullable: true),
                    display_subtotal = table.Column<string>(type: "varchar(255)", nullable: true),
                    supported_transaction_currency_id = table.Column<long>(type: "INTEGER", nullable: false),
                    miner_fee_id = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePaymentCurrency", x => x.id);
                    table.ForeignKey(
                        name: "fk_ipc_miner_fee_id",
                        column: x => x.miner_fee_id,
                        principalTable: "invoice_payment_currency_miner_fee",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ipc_invoice_supported_transaction_currency_id",
                        column: x => x.supported_transaction_currency_id,
                        principalTable: "invoice_payment_currency_supported_transaction_currency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ipc_invoice_payment_id",
                        column: x => x.invoice_payment_id,
                        principalTable: "invoice_payment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoice_refund_info",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    invoice_refund_id = table.Column<long>(type: "INTEGER", nullable: false),
                    support_request = table.Column<string>(type: "varchar(255)", nullable: true),
                    currency_code = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceRefundInfo", x => x.id);
                    table.ForeignKey(
                        name: "fk_iri_invoice_refund_id",
                        column: x => x.invoice_refund_id,
                        principalTable: "invoice_refund",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoice",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    uuid = table.Column<string>(type: "varchar(255)", nullable: false),
                    pos_data = table.Column<string>(type: "TEXT", nullable: false),
                    price = table.Column<double>(type: "DOUBLE", nullable: false),
                    currency_code = table.Column<string>(type: "varchar(10)", nullable: false),
                    bitpay_id = table.Column<string>(type: "varchar(255)", nullable: false),
                    status = table.Column<string>(type: "varchar(255)", nullable: false),
                    created_date = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    bitpay_order_id = table.Column<string>(type: "varchar(255)", nullable: true),
                    expiration_time = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    facade_type = table.Column<string>(type: "varchar(255)", nullable: true),
                    bitpay_guid = table.Column<string>(type: "varchar(255)", nullable: true),
                    exception_status = table.Column<string>(type: "varchar(255)", nullable: true),
                    bitpay_url = table.Column<string>(type: "varchar(255)", nullable: true),
                    redirect_url = table.Column<string>(type: "varchar(255)", nullable: true),
                    close_url = table.Column<string>(type: "varchar(255)", nullable: true),
                    acceptance_window = table.Column<long>(type: "INTEGER", nullable: true),
                    token = table.Column<string>(type: "varchar(255)", nullable: true),
                    merchant_name = table.Column<string>(type: "varchar(255)", nullable: true),
                    item_description = table.Column<string>(type: "text", nullable: true),
                    bill_id = table.Column<string>(type: "varchar(255)", nullable: true),
                    target_confirmations = table.Column<int>(type: "INTEGER", nullable: true),
                    low_fee_detected = table.Column<bool>(type: "BOOLEAN", nullable: true),
                    auto_redirect = table.Column<bool>(type: "BOOLEAN", nullable: true),
                    shopper_user = table.Column<string>(type: "varchar(255)", nullable: true),
                    json_pay_pro_required = table.Column<bool>(type: "BOOLEAN", nullable: true),
                    bitpay_id_required = table.Column<bool>(type: "BOOLEAN", nullable: true),
                    is_cancelled = table.Column<bool>(type: "BOOLEAN", nullable: true),
                    transaction_speed = table.Column<string>(type: "varchar(255)", nullable: false),
                    invoice_payment_id = table.Column<long>(type: "INTEGER", nullable: false),
                    invoice_buyer_id = table.Column<long>(type: "INTEGER", nullable: false),
                    invoice_refund_id = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.id);
                    table.ForeignKey(
                        name: "fk_i_invoice_buyer_id",
                        column: x => x.invoice_buyer_id,
                        principalTable: "invoice_buyer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_i_invoice_payment_id",
                        column: x => x.invoice_payment_id,
                        principalTable: "invoice_payment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_i_invoice_refund_id",
                        column: x => x.invoice_refund_id,
                        principalTable: "invoice_refund",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoice_payment_currency_code",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    invoice_payment_currency_id = table.Column<long>(type: "INTEGER", nullable: false),
                    payment_code = table.Column<string>(type: "varchar(255)", nullable: false),
                    payment_code_url = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePaymentCurrencyCode", x => x.id);
                    table.ForeignKey(
                        name: "fk_ipcc_invoice_payment_currency_id",
                        column: x => x.invoice_payment_currency_id,
                        principalTable: "invoice_payment_currency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoice_payment_currency_exchange_rate",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    invoice_payment_currency_id = table.Column<long>(type: "INTEGER", nullable: false),
                    currency_code = table.Column<string>(type: "varchar(10)", nullable: false),
                    rate = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePaymentCurrencyExchangeRate", x => x.id);
                    table.ForeignKey(
                        name: "fk_ipcer_invoice_payment_currency_id",
                        column: x => x.invoice_payment_currency_id,
                        principalTable: "invoice_payment_currency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoice_refund_info_amount",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    invoice_refund_info_id = table.Column<long>(type: "INTEGER", nullable: false),
                    currency_code = table.Column<string>(type: "varchar(10)", nullable: false),
                    amount = table.Column<double>(type: "DOUBLE", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceRefundInfoAmount", x => x.id);
                    table.ForeignKey(
                        name: "fk_iria_info_amount_invoice_refund_info_id",
                        column: x => x.invoice_refund_info_id,
                        principalTable: "invoice_refund_info",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoice_itemized_detail",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    invoice_id = table.Column<long>(type: "INTEGER", nullable: false),
                    amount = table.Column<double>(type: "DOUBLE", nullable: true),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    is_fee = table.Column<bool>(type: "BOOLEAN", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItemizedDetail", x => x.id);
                    table.ForeignKey(
                        name: "fk_iid_invoice_id",
                        column: x => x.invoice_id,
                        principalTable: "invoice",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "invoice_transaction",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    invoice_id = table.Column<long>(type: "INTEGER", nullable: false),
                    amount = table.Column<double>(type: "DOUBLE", nullable: true),
                    confirmations = table.Column<int>(type: "INTEGER", nullable: true),
                    received_time = table.Column<DateTime>(type: "DATE", nullable: true),
                    txid = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceTransaction", x => x.id);
                    table.ForeignKey(
                        name: "fk_it_invoice_id",
                        column: x => x.invoice_id,
                        principalTable: "invoice",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_InvoiceBuyerId",
                table: "invoice",
                column: "invoice_buyer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_InvoicePaymentId",
                table: "invoice",
                column: "invoice_payment_id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_InvoiceRefundId",
                table: "invoice",
                column: "invoice_refund_id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceBuyer_InvoiceBuyerProvidedInfoId",
                table: "invoice_buyer",
                column: "invoice_buyer_provided_info_id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItemizedDetail_InvoiceId",
                table: "invoice_itemized_detail",
                column: "invoice_id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePaymentCurrency_InvoicePaymentId",
                table: "invoice_payment_currency",
                column: "invoice_payment_id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePaymentCurrency_MinerFeeId",
                table: "invoice_payment_currency",
                column: "miner_fee_id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePaymentCurrency_SupportedTransactionCurrencyId",
                table: "invoice_payment_currency",
                column: "supported_transaction_currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePaymentCurrencyCode_PaymentCurrencyId",
                table: "invoice_payment_currency_code",
                column: "invoice_payment_currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePaymentCurrencyExchangeRate_PaymentCurrencyId",
                table: "invoice_payment_currency_exchange_rate",
                column: "invoice_payment_currency_id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRefundInfo_InvoiceRefundId",
                table: "invoice_refund_info",
                column: "invoice_refund_id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceRefundInfoAmount_InvoiceRefundInfoId",
                table: "invoice_refund_info_amount",
                column: "invoice_refund_info_id");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceTransaction_InvoiceId",
                table: "invoice_transaction",
                column: "invoice_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            ArgumentNullException.ThrowIfNull(migrationBuilder);

            migrationBuilder.DropTable(name: "invoice_itemized_detail");
            migrationBuilder.DropTable(name: "invoice_payment_currency_code");
            migrationBuilder.DropTable(name: "invoice_payment_currency_exchange_rate");
            migrationBuilder.DropTable(name: "invoice_refund_info_amount");
            migrationBuilder.DropTable(name: "invoice_transaction");
            migrationBuilder.DropTable(name: "invoice_payment_currency");
            migrationBuilder.DropTable(name: "invoice_refund_info");
            migrationBuilder.DropTable(name: "invoice");
            migrationBuilder.DropTable(name: "invoice_payment_Currency_miner_fee");
            migrationBuilder.DropTable(name: "invoice_payment_currency_supported_transaction_currency");
            migrationBuilder.DropTable(name: "invoice_buyer");
            migrationBuilder.DropTable(name: "invoice_payment");
            migrationBuilder.DropTable(name: "invoice_refund");
            migrationBuilder.DropTable(name: "invoice_buyer_provided_info");
        }
    }
}