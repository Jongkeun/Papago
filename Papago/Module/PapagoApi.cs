using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Papago
{
    class PapagoApi
    {
        HttpClient client = new HttpClient();

        public void SetKey(string key, string password)
        {
            client.BaseAddress = new Uri(PDefine.BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add(PDefine.X_HEADER_ID, key);
            client.DefaultRequestHeaders.Add(PDefine.X_HEADER_SCRETE, password);
        }
        public async Task<string> fnDetectLnaguage(string inputData)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("query", inputData)
            });
            var result = await client.PostAsync(PDefine.DETECT_API, content);            
            return await result.Content.ReadAsStringAsync();
        }
    }
}
