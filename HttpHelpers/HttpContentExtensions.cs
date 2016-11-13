using JsonHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HttpHelpers
{
    public static class HttpContentExtensions
    {
        public static async Task<ExpandoObject> AsExpandoAsync(this HttpContent content)
        {
            var obj = JsonConvert.DeserializeObject<ExpandoObject>(await content.ReadAsStringAsync(), CamelCaseToPascalSettings.GetSerializerSettings());

            return obj;
        }

        public static async Task<ExpandoObject[]> AsExpandoArrayAsync(this HttpContent content)
        {
            var obj = JsonConvert.DeserializeObject<ExpandoObject[]>(await content.ReadAsStringAsync(), CamelCaseToPascalSettings.GetSerializerSettings());

            return obj;
        }
    }
}
