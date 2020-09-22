using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace WeatherApp
{
    public class InsightsRestService : IInsightsRestService
    {
        HttpClient client;

        public List<InsightsEvent> Items { get; private set; }

        public InsightsRestService()
        {
#if DEBUG
            client = new HttpClient(DependencyService.Get<IHttpClientHandlerService>().GetInsecureHandler());
            client.DefaultRequestHeaders.Add("X-Insert-Key", Constants.InsightsKey);
#else
            client = new HttpClient();
#endif
        }

        public async Task SaveInsightsEventAsync(InsightsEvent item)
        {
            Uri uri = new Uri(string.Format(Constants.InsightsUrl, string.Empty));

            try
            {
                string json = JsonConvert.SerializeObject(item);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;

                response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tInsightsEvent successfully saved.");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
        }

    }
}
