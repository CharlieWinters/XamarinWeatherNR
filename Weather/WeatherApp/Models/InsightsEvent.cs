using System;
namespace WeatherApp

{
	public class InsightsEvent
	{
		public string ID { get; set; }

		public string eventType { get; set; }

		public string deviceOS { get; set; }

		public string runtimePlatform { get; set; }

		public string Notes { get; set; }

		public bool Done { get; set; }

		public string responseCode { get; set; }

		public string reasonPhrase { get; set; }
	}
}