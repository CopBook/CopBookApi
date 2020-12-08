using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CopBookApi.Services
{
    public static class JSON
    {
        public static JsonSerializerSettings serializeSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        };

        public static string Stringify(dynamic obj)
        {
            return JsonConvert.SerializeObject(obj, serializeSettings);
        }

        public static T Parse<T>(string jsonData)
        {
            return JsonConvert.DeserializeObject<T>(jsonData);
        }
    }
}
