using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherApp
{ 
    public interface IInsightsRestService
    {

        Task SaveInsightsEventAsync(InsightsEvent item);


    }
}

