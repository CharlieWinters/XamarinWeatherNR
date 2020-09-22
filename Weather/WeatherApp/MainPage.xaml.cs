using System;
using Xamarin.Forms;

namespace WeatherApp
{
    public partial class MainPage : ContentPage
    {
        RestService _restService;

        public MainPage()
        {
            InitializeComponent();
            _restService = new RestService();
        }

        async void OnGetWeatherButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_cityEntry.Text))
            {
                WeatherData weatherData = await _restService.GetWeatherData(GenerateRequestUri(Constants.OpenWeatherMapEndpoint));
                BindingContext = weatherData;
            }
        }

        string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?q={_cityEntry.Text}";
            requestUri += "&units=imperial"; // or units=metric
            requestUri += $"&APPID={Constants.OpenWeatherMapAPIKey}";

            InsightsEvent insightsEvent = new InsightsEvent();

            insightsEvent.ID = "123";
            insightsEvent.eventType = "XamarinEvent";
            insightsEvent.Notes = _cityEntry.Text;
            insightsEvent.deviceOS = Device.RuntimePlatform == Device.Android ? "Android" : "iOS";
            insightsEvent.runtimePlatform = Device.RuntimePlatform;
            insightsEvent.Done = true;
            App.InsightsManager.SaveInsightsEventAsync(insightsEvent);

            return requestUri;
        }
    }
}
