using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace WeatherApp
{
    public class RestService
    {
        HttpClient _client;

        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task<WeatherData> GetWeatherData(string query)
        {
            WeatherData weatherData = null;
            try
            {
                var response = await _client.GetAsync(query);
                InsightsEvent insightsEvent = new InsightsEvent();

                insightsEvent.deviceOS = Device.RuntimePlatform == Device.Android ? "Android" : "iOS";
                insightsEvent.runtimePlatform = Device.RuntimePlatform;
                insightsEvent.ID = "123";
                insightsEvent.Notes = "WeatherDataRequest";

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    weatherData = JsonConvert.DeserializeObject<WeatherData>(content);

                    
                    insightsEvent.eventType = "XamarinHTTPRequest";
                   
                    insightsEvent.responseCode = Convert.ToString(Convert.ToInt32(response.StatusCode));
                    insightsEvent.reasonPhrase = response.ReasonPhrase;
                    insightsEvent.Done = true;
                    
                }
                else
                {
                    insightsEvent.eventType = "XamarinHTTPRequestError";
                    insightsEvent.responseCode = "404";
                    insightsEvent.reasonPhrase = "City Not Found";
                }
                App.InsightsManager.SaveInsightsEventAsync(insightsEvent);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("\t\tERROR {0}", ex.Message);
            }

            
            return weatherData;
        }
    }
}
