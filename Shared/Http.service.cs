using System.Net.Http;
using System.Threading.Tasks;

namespace ringba_test.Shared
{
    public static class Http
    {
        public async static Task<string> GetString(string URL)
        {
            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                using (HttpResponseMessage result = await client.GetAsync(URL))
                {
                    return await result.Content.ReadAsStringAsync();
                }
            }
        }
    }
}
