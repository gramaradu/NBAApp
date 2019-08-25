using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NBA.Utils.Requests
{
   public static class BuildRequests
    {
        public static async Task<string> Build(string endpoint)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("x-rapidapi-host", "free-nba.p.rapidapi.com");
                client.DefaultRequestHeaders.Add("x-rapidapi-key", "438a741aeemshd8665a964c6740ep1f1906jsnf3d76f534ee8");

                var content = await client.GetStringAsync(endpoint);
                if (!string.IsNullOrEmpty(content))
                {
                    return content;
                }
            }
            return null;
        }
    }
}
