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
        public JsonTodayCard (HistoryCard card)
        {
            this.Temperature = card.Temperature;
            this.Humidity = card.Humidity;
            this.Pressure = card.Pressure;
            this.Radiation = card.Radiation;
            this.WindDirection = card.WindDirection;
            this.Description = card.Description;
        }
    }
    [JsonObject]
    public class JsonCard
    {
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
        public JsonCard(ForecastCard card) {
            this.Humidity = card.Humidity;
            this.Description = card.Description;
            this.Date = card.DateTime.Day+"."+card.DateTime.Month;
            this.Temperature = card.Temperature;
            this.WindDirection = card.WindDirection;
        }
        public JsonCard(HistoryCard card)
        {
            this.Humidity = card.Humidity;
            this.Description = card.Description;
            this.Date = card.DateTime.Day + "." + card.DateTime.Month;
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

        public JsonTodayGraphic(ForecastCard card)
        {
            this.Time_hour = card.DateTime.Hour;
            this.Time_min = card.DateTime.Minute;
            this.Temperature = card.Temperature;
        }
        public JsonTodayGraphic(HistoryCard card)
        {
            this.Time_hour = card.DateTime.Hour;
            this.Time_min = card.DateTime.Minute;
            this.Temperature = card.Temperature;
        }
    }
    public class PackageTodayGraphicAndToday
    {
        [JsonProperty]
        public JsonTodayCard Today { get; set; }
        [JsonProperty]
        public List<JsonTodayGraphic> TodayGraphic { get; set; }

        public PackageTodayGraphicAndToday()
        {
            this.TodayGraphic = new List<JsonTodayGraphic>();

        }
    }
    public class PackageFutureAndPast
    {
        [JsonProperty]
        public List<JsonCard> Future { get; set; }
        [JsonProperty]
        public List<JsonCard> Past { get; set; }

        public PackageFutureAndPast()
        {
            this.Past = new List<JsonCard>();
            this.Future = new List<JsonCard>();

        }
    }
}