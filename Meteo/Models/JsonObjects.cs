using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meteo.Models;


namespace Meteo.Models
{
    [JsonObject]
    public class JsonTodayCard {
		[JsonProperty]
        public int 	Temperature { get; set; }
		[JsonProperty]
        public float Humidity { get; set; }
		[JsonProperty]
        public int 	WindSpeed  { get; set; }
		[JsonProperty]
        public float Pressure { get; set; }
		[JsonProperty]
        public float Radiation { get; set; }
		[JsonProperty]
        public string WindDirection { get; set; }
        [JsonProperty]
        public string Description { get; set; }
        public MeteoStationCard ToCard()
        {
            MeteoStationCard card = new MeteoStationCard();
            card.DateTime = DateTime.Now;
            card.Temperature = this.Temperature;
            card.Humidity = this.Humidity;
            card.Pressure = this.Pressure;
            card.Radiation = this.Radiation;
            card.WindDirection = this.WindDirection;
            card.Description = this.Description;
            return card;
        }
    }
    [JsonObject]
    public class JsonCard
    {
        [JsonProperty("time_hour")]
        public int Time_hour { get; set; }
        [JsonProperty("time_min")]
        public int Time_min { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
        [JsonProperty("temperature")]
        public int Temperature { get; set; }
        [JsonProperty("humidity")]
        public float Humidity { get; set; }
        [JsonProperty("winddirection")]
        public string WindDirection { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }

        public JsonCard() { }
        public JsonCard(OpenWeatherCard card) {
            this.Humidity = card.Humidity;
            this.Description = card.Description;
            this.Date = card.DateTime.Day+"."+card.DateTime.Month;
            this.Time_hour = card.DateTime.Hour;
            this.Time_min = card.DateTime.Minute;
            this.Temperature = card.Temperature;
            this.WindDirection = card.WindDirection;
        }
        public JsonCard(MeteoStationCard card)
        {
            this.Humidity = card.Humidity;
            this.Description = card.Description;
            this.Date = card.DateTime.Day + "." + card.DateTime.Month;
            this.Time_hour = card.DateTime.Hour;
            this.Time_min = card.DateTime.Minute;
            this.Temperature = card.Temperature;
            this.WindDirection = card.WindDirection;
        }
    }
    [JsonObject]
    public class JsonTodayGraphic
    {
        [JsonProperty]
        public int Time_hour { get; set; }
        [JsonProperty]
        public int Time_min { get; set; }
        [JsonProperty]
        public int Temperature { get; set; }
    }

    //----Для проверок, фигня
   
    public class HtmlResult : ActionResult
    {
        private string htmlCode;
        public HtmlResult(string html)
        {
            htmlCode = html;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            string fullHtmlCode =  htmlCode;
            context.HttpContext.Response.Write(fullHtmlCode);
        }
    }
}