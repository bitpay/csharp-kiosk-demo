// Copyright 2023 BitPay.
// All rights reserved.

using System.Net;

using BitPay.Exceptions;

using Moq;

namespace CsharpKioskDemoDotnet.IntegrationTests.Donation.Infastructure.Ui.CreateDonation
{
    public class CreateDonationIntegrationTest: AbstractUiIntegrationTest
    {
        private const string Url = "/invoice";

        public CreateDonationIntegrationTest(CustomWebApplicationFactory<Program> factory) 
        : base(factory, "Src/Donation/Infastructure/Ui/bitPayDesign.yaml")
        {
        }

        [Fact]
        public void POST_BitPayInvoiceCreated_SaveInvoiceInDatabaseAndRedirectToBitPayUrl()
        {
            // given
            BitPay.Setup(e => e.CreateInvoice(It.IsAny<BitPay.Models.Invoice.Invoice>()))
                .ReturnsAsync(UnitTest.GetBitPayInvoice());

            // when
            var result = Post(
                new Dictionary<string, string>
                {
                    { "buyerName", "fake name" },
                    { "buyerAddress1", "fake address" },
                    { "buyerAddress2", "fake address 2" },
                    { "buyerLocality", "fake location" },
                    { "buyerRegion", "AK" },
                    { "buyerPostalCode", "10010" },
                    { "buyerPhone", "123456789" },
                    { "buyerEmail", "test@example.com" },
                    { "store", "store-1" },
                    { "register", "2" },
                    { "reg_transaction_no", "test" },
                    { "price", "123" }
                }
            );

            // then
            UnitTest.Equals(HttpStatusCode.MovedPermanently, result.StatusCode);
            Assert.Single(GetInvoiceRepository().FindAll());
        }

        [Fact]
        public void POST_CreatingBitPayInvoiceThrowException_DoNotSaveInvoiceInDatabase()
        {
            // given
            BitPay.Setup(e => e.CreateInvoice(It.IsAny<BitPay.Models.Invoice.Invoice>()))
                .Throws(new AggregateException(new InvoiceCreationException()));

            // when
            var result = Post(
                new Dictionary<string, string>
                {
                    { "buyerName", "fake name" },
                    { "buyerAddress1", "fake address" },
                    { "buyerAddress2", "fake address 2" },
                    { "buyerLocality", "fake location" },
                    { "buyerRegion", "AK" },
                    { "buyerPostalCode", "10010" },
                    { "buyerPhone", "123456789" },
                    { "buyerEmail", "test@example.com" },
                    { "store", "store-1" },
                    { "register", "2" },
                    { "reg_transaction_no", "test" },
                    { "price", "123" }
                }
            );

            // then
            Assert.Empty(GetInvoiceRepository().FindAll());
        }

        [Fact]
        public void POST_RequestMissingRequiredFields_DoNotSaveInvoiceInDatabase()
        {
            // given

            // when
            var result = Post(
                new Dictionary<string, string>
                {   
                    { "buyerName", "fake name" },
                    { "buyerAddress1", "fake address" },
                    { "buyerAddress2", "fake address 2" },
                    { "buyerRegion", "AK" },
                    { "buyerPostalCode", "10010" },
                    { "buyerPhone", "123456789" },
                    { "buyerEmail", "test@example.com" },
                    { "store", "store-1" },
                    { "register", "2" },
                    { "reg_transaction_no", "test" },
                    { "price", "123" }
                }
            );

            // then
            Assert.Empty(GetInvoiceRepository().FindAll());
        }

        [Fact]
        public void POST_RequestMissingRequiredPosData_DoNotSaveInvoiceInDatabase()
        {
            // given

            // when
            var result = Post(
                new Dictionary<string, string>
                {
                    { "buyerName", "fake name" },
                    { "buyerAddress1", "fake address" },
                    { "buyerAddress2", "fake address 2" },
                    { "buyerLocality", "fake location" },
                    { "buyerRegion", "AK" },
                    { "buyerPostalCode", "10010" },
                    { "buyerPhone", "123456789" },
                    { "buyerEmail", "test@example.com" },
                    { "store", "store-1" },
                    { "register", "2" },
                    { "price", "123" }
                }
            );

            // then
            Assert.Empty(GetInvoiceRepository().FindAll());
        }

        [Fact]
        public void POST_WrongFieldValue_DoNotSaveInvoiceInDatabase()
        {
            // given

            // when
            var result = Post(
                new Dictionary<string, string>
                {   
                    { "buyerName", "fake name" },
                    { "buyerAddress1", "fake address" },
                    { "buyerAddress2", "fake address 2" },
                    { "buyerLocality", "fake location" },
                    { "buyerRegion", "test" },
                    { "buyerPostalCode", "10010" },
                    { "buyerPhone", "123456789" },
                    { "buyerEmail", "test@example.com" },
                    { "store", "store-1" },
                    { "register", "2" },
                    { "reg_transaction_no", "test" },
                    { "price", "123" }
                }
            );

            // then
            Assert.Empty(GetInvoiceRepository().FindAll());
        }

        private HttpResponseMessage Post(Dictionary<string, string> request)
        {
            return PostForm(Url, request).Result;
        }
    }
}