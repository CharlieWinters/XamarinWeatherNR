using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherApp
{
	public class InsightsEventManager
	{
		IInsightsRestService restService;

		public InsightsEventManager(IInsightsRestService service)
		{
			restService = service;
		}

		public Task SaveInsightsEventAsync(InsightsEvent item)
		{
			return restService.SaveInsightsEventAsync(item);
		}

	}
}