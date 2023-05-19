// Copyright 2023 BitPay.
// All rights reserved.

namespace CsharpKioskDemoDotnet.IntegrationTests.Donation.Infastructure.Ui.GetDonationForm
{
    public class GetDonationFormIntegrationTest : AbstractUiIntegrationTest
    {
        public GetDonationFormIntegrationTest(CustomWebApplicationFactory<Program> factory) 
        : base(factory, "Src/Donation/Infastructure/Ui/bitPayDesign.yaml")
        {
        }

        [Fact]
        public async Task GET_FormFieldsLoadedFromYamlFile_ProvidesAllFieldsInForm()
        {
            // given

            // when
            var result = await Get("/");

            // then
            var content = await HtmlHelpers.GetDocumentAsync(result);
            Assert.NotNull(content.QuerySelector("select[name='store']"));
            UnitTest.Equals(2, content.QuerySelectorAll("input[name='register']").Length);
            Assert.NotNull(content.QuerySelector("input[name='reg_transaction_no']"));
            Assert.NotNull(content.QuerySelector("input[name='price']"));

            Assert.NotNull(content.QuerySelector("input[name='buyerName']"));
            Assert.NotNull(content.QuerySelector("input[name='buyerAddress1']"));
            Assert.NotNull(content.QuerySelector("input[name='buyerAddress2']"));
            Assert.NotNull(content.QuerySelector("input[name='buyerLocality']"));
            Assert.NotNull(content.QuerySelector("select[name='buyerRegion']"));
            Assert.NotNull(content.QuerySelector("input[name='buyerPostalCode']"));
            Assert.NotNull(content.QuerySelector("input[name='buyerPhone']"));
            Assert.NotNull(content.QuerySelector("input[name='buyerEmail']"));
        }
    }
}