using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meteo.Models;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Net;
using System.Web.Script.Serialization;
using Meteo.Parse;

namespace Meteo.Controllers
{
    public class CardController : Controller
    {
        private CardContext db = new CardContext();

        public ActionResult Index()
        {
            //DateTime datatime= DateTime.Now;
            //List<OpenWeatherCard> listCards = dbForecast.OpenWeatherCards.ToList();
            //List<JsonCard> jsonCards=new List<JsonCard>(); 
            //foreach (var card in listCards)
            //{               
            //        jsonCards.Add(new JsonCard(card));
            //}

            return Json(db.OpenWeatherCards.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult IndexMeteo()
        {
            return Json(db.MeteoStationCards.ToList(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetTodaAndTodayGraphic()
        {
            return View();
        }
        
        public  ActionResult MeteoData()//string data)
        {
            string data = "r:533|t:15.09|h:84.17|pt:19.43|p:983.65|g:10.60=r:470|t:15.08|h:84.22|pt:19.41|p:983.53|g:6.62=r:477|t:15.11|h:84.61|pt:19.41|p:983.61|g:21.19=";  
            MeteoStationCard meteoStationCard = ParserMeteoData.ParseInJson(data);
            meteoStationCard.DateTime = DateTime.Now;
            meteoStationCard.Description = "-";
            meteoStationCard.WindDirection = "-";
            meteoStationCard.WindSpeed = 0;
            db.MeteoStationCards.Add(meteoStationCard);
            db.SaveChanges();
            return new EmptyResult();//new HtmlResult(data + "<br><br>" + JsonConvert.SerializeObject(todayCard));//Json(todayCard, JsonRequestBehavior.AllowGet).Data);  
        }

        public JsonCard OneDay(List<JsonCard> listCard) 
        {
            JsonCard day = new JsonCard();
            day.Date = listCard[0].Date;
            day.Description = listCard[0].Description;
            day.WindDirection = listCard[0].WindDirection;
            int maxCountWindDirection = 1;
            int maxCountDescription = 1;
            int count = 0;
            foreach (var card in listCard)
            {
                day.Humidity += card.Humidity / listCard.Count;
                day.Temperature += card.Temperature / listCard.Count;
                foreach (var item in listCard)
                {
                    count = 0;
                    if (item.WindDirection == card.WindDirection)
                        count++;
                }
                if (count > maxCountWindDirection)
                {
                    day.WindDirection = card.WindDirection;
                    maxCountWindDirection = count;
                }
                foreach (var item in listCard)
                {
                    count = 0;
                    if (item.Description == card.Description)
                        count++;
                }
                if (count > maxCountDescription)
                {
                    day.Description = card.Description;
                    maxCountDescription = count;
                }
            }
            return day;
        }

        public ActionResult GetFutureAndPast()
        {
            Package pocket = new Package();
            pocket.Past = new List<JsonCard>();
            pocket.Future = new List<JsonCard>();
            List<JsonCard> listCard=new List<JsonCard>();
            DateTime present = DateTime.Now;
            List<OpenWeatherCard> forecastCards=new List<OpenWeatherCard>();
            forecastCards = db.OpenWeatherCards.Where(x => 
                x.DateTime.Day!=present.Day &&
                x.DateTime >= present && 
                x.DateTime <= x.DateTime.AddDays(5)).ToList();
            List<MeteoStationCard> historyCards;
            historyCards= db.MeteoStationCards.Where(x => 
                x.DateTime.Day!=present.Day &&
                x.DateTime <= present && 
                x.DateTime > x.DateTime.AddDays(-5)).ToList();

            foreach (var card in forecastCards) 
            {
                listCard.Add(new JsonCard(card));
            }
            foreach (var card in historyCards)
            {
                listCard.Add(new JsonCard(card));
            }

            for (var counter = 0; counter < 5; counter++)
            {
                pocket.Future.Add(OneDay(listCard.Where(x => x.Date == (present.AddDays(counter + 1) + "." + present.Month)).ToList()));
                pocket.Past.Add(OneDay(listCard.Where(x => x.Date == (present.AddDays(counter - 1) + "." + present.Month)).ToList()));
            }
            return Json(pocket,JsonRequestBehavior.AllowGet);
        }

        public ActionResult OpenWeatherData()
        {
            List<OpenWeatherCard> openWeatherCards = ParserOpenWeatherData.ParseInJson().ToCard();
            foreach (var card in openWeatherCards)
            {
                OpenWeatherCard openWeatherCard = db.OpenWeatherCards.FirstOrDefault(x=>x.DateTime==card.DateTime);
                if (openWeatherCard == null)
                    db.OpenWeatherCards.Add(card);
                else
                {
                    openWeatherCard.Description = card.Description;
                    openWeatherCard.Humidity = card.Humidity;
                    openWeatherCard.Temperature = card.Temperature;
                    openWeatherCard.WindDirection = card.WindDirection;
                    db.Entry(openWeatherCard).State = EntityState.Modified;
                }
            }
            db.SaveChanges();
            //-----
            List<JsonCard> jsonCards = new List<JsonCard>();
            foreach (var card in openWeatherCards)
            {
                jsonCards.Add(new JsonCard(card));
            }
            //----- new EmptyResult();
            return Json(jsonCards, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}