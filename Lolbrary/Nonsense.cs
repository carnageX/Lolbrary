using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Lolbrary
{
    public static class Nonsense
    {
        public enum NonsenseLength
        {
            Short,
            Medium,
            Long,
            VeryLong
        };

        public static async Task<string> GetNonsense(NonsenseLength length)
        {
            string returnString = String.Empty;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://loripsum.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(String.Format("api/{0}/{1}/{2}", 10, length.ToString().ToLower(), "plaintext"));
                if(response.IsSuccessStatusCode)
                {
                    var lorem = await response.Content.ReadAsStringAsync();
                    var loremBytes = Encoding.Default.GetBytes(lorem);
                    returnString = Convert.ToBase64String(loremBytes);
                }
            }
            
            return returnString;
        }
    }
}
