// Copyright 2023 BitPay.
// All rights reserved.

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CsharpKioskDemoDotnet.Tests;

interface IUnitTest
{
    string ToJson(object anyObject)
    {
        return JsonConvert.SerializeObject(anyObject, GetSettings());
    }

    T ToObject<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json, GetSettings())!;
    }

    private JsonSerializerSettings GetSettings()
    {
        return new JsonSerializerSettings
        {
            ContractResolver = new IgnoreFieldResolver(),
            Formatting = Formatting.Indented
        };
    }

    string GetDataFromFile(string fileName)
    {
        var path = GetType().Namespace!
            .Replace("CsharpKioskDemoDotnet.Tests.", "")
            .Replace(".", "/");

        var pathname = Directory.GetCurrentDirectory() + "/Src/" + path + "/" + fileName;

        using var r = new StreamReader(pathname);
        return r.ReadToEnd();
    }

    void Equals(
        object expected,
        object actual
    )
    {
        Assert.Equivalent(
            expected,
            actual,
            strict: false
        );
    }

    void Equals(
        string expected,
        string actual,
        string[] propertiesToIgnore
    )
    {
        var expectedObject = JObject.Parse(actual);
        var actualObject = JObject.Parse(expected);

        var expectedObjectFiltered = FilterProperties(expectedObject, propertiesToIgnore);
        var actualObjectFiltered = FilterProperties(actualObject, propertiesToIgnore);

        Equals(ToJson(expectedObjectFiltered), ToJson(actualObjectFiltered));
    }

    private JObject FilterProperties(JObject obj, string[] propertiesToIgnore)
    {
        var newObj = new JObject();

        foreach (var property in obj.Properties())
        {
            if (!propertiesToIgnore.Contains(property.Name))
            {
                newObj.Add(property.Name, property.Value);
            }
        }

        return newObj;
    }
}