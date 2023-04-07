using CsharpKioskDemoDotnet.Shared;
using Newtonsoft.Json;

namespace CsharpKioskDemoDotnet.Tests.Invoice.Application.Features.Tasks.UpdateInvoice;

internal class JsonToObjectConverter : IJsonToObjectConverter
{
    public T? Execute<T>(string? json)
    {
        return json == null
            ? default
            : JsonConvert.DeserializeObject<T>(json);
    }
}