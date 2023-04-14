using CsharpKioskDemoDotnet.Invoice.Application.Features.Tasks.CreateInvoice;
using CsharpKioskDemoDotnet.Shared.BitPayProperties;
using Microsoft.Extensions.Options;

namespace CsharpKioskDemoDotnet.Tests.Invoice.Application.Features.Tasks.CreateInvoice;

public class GetValidatedParamsTest : IUnitTest
{
    private IUnitTest UnitTest => this;

    [Fact]
    private void Execute_DictionaryWithFields_ReturnDictionaryWithOnlyValidFields()
    {
        // given
        var requestParameters = new Dictionary<string, string?>
        {
            { "store", "store-1" },
            { "register", "2" },
            { "reg_transaction_no", "test" },
            { "price", "123" },
            { "fake", "123" }
        };

        // when
        var result = GetTestedClass().Execute(requestParameters);

        // then
        UnitTest.Equals(4, result.Count);
        UnitTest.Equals(true, result.ContainsKey("store"));
        UnitTest.Equals(true, result.ContainsKey("register"));
        UnitTest.Equals(true, result.ContainsKey("reg_transaction_no"));
        UnitTest.Equals(true, result.ContainsKey("price"));
    }

    [Fact]
    private void Execute_DictionaryWithoutRequiredPrice_ThrowMissingRequiredField()
    {
        // given
        var requestParameters = new Dictionary<string, string?>
        {
            { "store", "store-1" },
            { "register", "2" },
            { "reg_transaction_no", "test" },
        };

        // when
        var exception = Assert.Throws<MissingRequiredField>(() => GetTestedClass().Execute(requestParameters));

        // then
        UnitTest.Equals("price", exception.Field.Name!);
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