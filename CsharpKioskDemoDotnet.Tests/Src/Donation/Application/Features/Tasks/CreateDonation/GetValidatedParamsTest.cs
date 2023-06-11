// Copyright 2023 BitPay.
// All rights reserved.

using CsharpKioskDemoDotnet.Donation.Application.Features.Tasks.CreateDonation;
using CsharpKioskDemoDotnet.Shared.BitPayProperties;
using CsharpKioskDemoDotnet.Shared.Form;

using Microsoft.Extensions.Options;

namespace CsharpKioskDemoDotnet.Tests.Donation.Application.Features.Tasks.CreateDonation
{
    public class GetValidatedParamsTest : IUnitTest
    {
        private IUnitTest UnitTest => this;

        [Fact]
        private void Execute_DictionaryWithFields_ReturnDictionaryWithOnlyValidFields()
        {
            // given
            var requestParameters = new Dictionary<string, string?>
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
                { "price", "100" },
                { "fake", "123" }
            };

            // when
            var result = GetTestedClass().Execute(requestParameters);

            // then
            UnitTest.Equals(12, result.Count);
            UnitTest.Equals(true, result.ContainsKey("store"));
            UnitTest.Equals(true, result.ContainsKey("register"));
            UnitTest.Equals(true, result.ContainsKey("reg_transaction_no"));
            UnitTest.Equals(true, result.ContainsKey("buyerName"));
            UnitTest.Equals(true, result.ContainsKey("buyerAddress1"));
            UnitTest.Equals(true, result.ContainsKey("buyerAddress2"));
            UnitTest.Equals(true, result.ContainsKey("buyerLocality"));
            UnitTest.Equals(true, result.ContainsKey("buyerRegion"));
            UnitTest.Equals(true, result.ContainsKey("buyerPostalCode"));
            UnitTest.Equals(true, result.ContainsKey("buyerPhone"));
            UnitTest.Equals(true, result.ContainsKey("buyerEmail"));
        }

        [Fact]
        private void Execute_DictionaryWithoutRequiredPrice_ThrowMissingRequiredField()
        {
            // given
            var requestParameters = new Dictionary<string, string?>
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
            };

            // when
            var exception = Assert.Throws<MissingRequiredFieldException>(() => GetTestedClass().Execute(requestParameters));

            // then
            UnitTest.Equals("price", exception.Field.Name!);
        }

        [Fact]
        private void Execute_DictionaryWithoutRequiredPosData_ThrowMissingRequiredField()
        {
            // given
            var requestParameters = new Dictionary<string, string?>
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
                { "reg_transaction_no", "test" },
                { "price", "100" },
            };

            // when
            var exception = Assert.Throws<MissingRequiredFieldException>(() => GetTestedClass().Execute(requestParameters));

            // then
            UnitTest.Equals("register", exception.Field.Name!);
        }

        private GetValidatedParams GetTestedClass()
        {
            return new GetValidatedParams(GetBitPayPropertiesOption());
        }

        private IOptions<BitPayProperties> GetBitPayPropertiesOption()
        {
            var bitPayPropertiesJson = UnitTest.GetDataFromFile("bitPayProperties.json");
            var bitPayProperties = UnitTest.ToObject<BitPayProperties>(bitPayPropertiesJson);

            return Options.Create(bitPayProperties);
        }
    }
}