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
        private string strKey = string.Empty;
        private string strPwd = string.Empty;

        public void SetKey(string key, string password)
        {
            this.strKey = key;
            this.strPwd = password;
        }
        public async Task<string> fnDetectLnaguage(string inputData)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(PDefine.BASE_URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add(PDefine.X_HEADER_ID, this.strKey);
            client.DefaultRequestHeaders.Add(PDefine.X_HEADER_SCRETE, this.strPwd);
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("query", inputData)
            });
            var result = await client.PostAsync(PDefine.DETECT_API, content);            
            return await result.Content.ReadAsStringAsync();
        }
    }
}
