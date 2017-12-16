using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    class NetUtils
    {
        /// <summary>
        /// Get请求返回值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<T> GetResult<T>(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var contentMessage = await client.GetAsync(url);
                T result = default(T);
                if (contentMessage.IsSuccessStatusCode)
                {
                    string content = await contentMessage.Content.ReadAsStringAsync();
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
                }
                return result;
            }
        }
    }
}
