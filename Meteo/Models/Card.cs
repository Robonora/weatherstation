using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Meteo.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        //[JsonProperty("time_hour")]
        //public int Time_hour {get;set;}
        //[JsonProperty("time_min")]
        //public int Time_min {get;set;}
        //[JsonProperty("date")]
        //public int Date {get;set;}
        //[JsonProperty("month")]
        //public string Month {get;set;}
        [Required]
        public DateTime DateTime{get;set;}
        [Required]
        public int Temperature{get;set;}
        [Required]
        public int Humidity { get; set; }
        [Required]
        public string Wind { get; set; }
        [Required]
        public string Charact{get;set;}
    }

    public class TodayCard {
		[JsonProperty]
        public int 	temperature { get; set; }
		[JsonProperty]
        public int 	humidity { get; set; }
		[JsonProperty]
        public int 	wind_power  { get; set; }
		[JsonProperty]
        public int 	pressure { get; set; }
		[JsonProperty]
        public int radiation { get; set; }
		[JsonProperty]
        public string wind { get; set; }
        [JsonProperty]
        public string charact { get; set; }
    }
    [JsonObject]
    public class JsonCard
    {
        [JsonProperty("time_hour")]
        public int Time_hour { get; set; }
        [JsonProperty("time_min")]
        public int Time_min { get; set; }
        [JsonProperty("date")]
        public int Date { get; set; }
        [JsonProperty("month")]
        public string Month { get; set; }
        [JsonProperty("temperature")]
        public int Temperature { get; set; }
        [JsonProperty("humidity")]
        public int Humidity { get; set; }
        [JsonProperty("wind")]
        public string Wind { get; set; }
        [JsonProperty("charact")]
        public string Charact { get; set; }
    }
    public class TodayGraphic
    {
        [JsonProperty]
        public int time_hour { get; set; }
        [JsonProperty]
        public int time_min { get; set; }
        [JsonProperty]
        public int temperature { get; set; }
    }
    public static class Month
    {
        public static Dictionary<string, int> array = new Dictionary<string, int>();
        public static Dictionary<int, string> arraykek = new Dictionary<int, string>();
        public static void FillingMonth()
        {
            array.Add("январь", 1);
            array.Add("февраль", 2);
            array.Add("март", 3);
            array.Add("апрель", 4);
            array.Add("май", 5);
            array.Add("июнь", 6);
            array.Add("июль", 7);
            array.Add("август", 8);
            array.Add("сентябрь", 9);
            array.Add("октябрь", 10);
            array.Add("ноябрь", 11);
            array.Add("декабрь", 12);
        }
        public static void FillingMonthkek()
        {
            arraykek.Add(1, "январь");
            arraykek.Add(2, "февраль");
            arraykek.Add(3, "март");
            arraykek.Add(4, "апрель");
            arraykek.Add(5, "май");
            arraykek.Add(6, "июнь");
            arraykek.Add(7, "июль");
            arraykek.Add(8, "август");
            arraykek.Add(9, "сентябрь");
            arraykek.Add(10, "октябрь");
            arraykek.Add(11, "ноябрь");
            arraykek.Add(12, "декабрь");
        }
    }
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