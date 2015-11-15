using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Collections;
using System.Globalization;

namespace Meteo.Models
{
    [JsonObject]
    public class OpenWeatherJson
    {
        [JsonProperty("list")]
        public List<ListWeather> ListWeather { get; set; }
        public List<ForecastCard> ToCard() {
            List<ForecastCard> forecastCards = new List<ForecastCard>();
            foreach (var weather in this.ListWeather)
            {
                ForecastCard card=new ForecastCard();
                card.DateTime = Convert.ToDateTime(weather.DateTime.Replace("-","."));             
                weather.Main.KelvinToCelsius();
                card.Temperature = (int)Convert.ToSingle(weather.Main.Temperature, new CultureInfo("en-US"));               
                card.Humidity = Convert.ToSingle(weather.Main.Humidity, new CultureInfo("en-US"));
                card.WindDirection = weather.Wind.DegreeToCardinal();
                card.Description = weather.Weather[0].Description;
                forecastCards.Add(card);                
            }
            return forecastCards;
        }
    }
    public class ListWeather
    {
        [JsonProperty("main")]
        public Main Main { get; set; }
        [JsonProperty("weather")]
        public Weather[] Weather { get; set; }
        [JsonProperty("wind")]
        public Wind Wind { get; set; }
        [JsonProperty("dt_txt")]
        public string DateTime { get; set; }
    }
    public class Main
    {
        [JsonProperty("temp")]
        public float Temperature { get; set; }
        [JsonProperty("humidity")]
        public float Humidity { get; set; }
        public void KelvinToCelsius() 
        {
            this.Temperature -= 273;
        }
    }
    public class Weather
    {
        [JsonProperty("main")]
        public string Description { get; set; }
    }
    public class Wind
    {
        [JsonProperty("deg")]
        public float WindDirectionInDegrees { get; set; }
        public string DegreeToCardinal()
        {
            string Cardinal = "";
            if (this.WindDirectionInDegrees > 337.5 && this.WindDirectionInDegrees <= 22.5)
                Cardinal += "N";
            else if (this.WindDirectionInDegrees > 22.5 && this.WindDirectionInDegrees <= 67.5)
                    Cardinal += "NE";
                 else if (this.WindDirectionInDegrees > 67.5 && this.WindDirectionInDegrees <= 112.5)
                         Cardinal += "E";
                    else if (this.WindDirectionInDegrees > 112.5 && this.WindDirectionInDegrees <= 157.5)
                            Cardinal += "SE";
                        else if (this.WindDirectionInDegrees > 157.5 && this.WindDirectionInDegrees <= 202.5)
                                Cardinal += "S";
                            else if (this.WindDirectionInDegrees > 202.5 && this.WindDirectionInDegrees <= 247.5)
                                    Cardinal += "SW";
                                else if (this.WindDirectionInDegrees > 247.5 && this.WindDirectionInDegrees <= 292.5)
                                        Cardinal += "W";
                                    else if (this.WindDirectionInDegrees > 292.5 && this.WindDirectionInDegrees <= 337.5)
                                            Cardinal += "NW";
            return Cardinal;
        }
    }
}