using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Meteo.Models;
using System.Net;
using Newtonsoft.Json;

namespace Meteo.Parse
{
    public static class ParserOpenWeatherData
    {
        public static OpenWeatherJson ParseInJson() 
        {
            String urlGetFutureWeather = "http://api.openweathermap.org/data/2.5/forecast?q=Chelyabinsk,us&appid=ffd8265cc29853ce88591619a85d67f5";
            WebClient client = new WebClient();
            String jsonStringFutureWeather = client.DownloadString(urlGetFutureWeather);
            if (jsonStringFutureWeather.Contains("error")) return null;
            OpenWeatherJson openWeatherJson = new OpenWeatherJson();
            openWeatherJson = JsonConvert.DeserializeObject<OpenWeatherJson>(jsonStringFutureWeather);
            return openWeatherJson;
        }
    }
}