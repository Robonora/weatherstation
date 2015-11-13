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

        public CardController() { }

        public ActionResult Index()
        {
            //DateTime datatime= DateTime.Now;
            //List<ForecastCard> listCards = dbForecast.ForecastCards.ToList();
            //List<JsonCard> jsonCards=new List<JsonCard>(); 
            //foreach (var card in listCards)
            //{               
            //        jsonCards.Add(new JsonCard(card));
            //}

            return Json(db.ForecastCards.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult IndexMeteo()
        {
            return Json(db.HistoryCards.ToList(), JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetTodayAndTodayGraphic()
        {
            return View();
        }
        
        public  ActionResult MeteoData()//string data)
        {
            string data = "r:533|t:15.09|h:84.17|pt:19.43|p:983.65|g:10.60=r:470|t:15.08|h:84.22|pt:19.41|p:983.53|g:6.62=r:477|t:15.11|h:84.61|pt:19.41|p:983.61|g:21.19=";  
            HistoryCard historyCard = ParserMeteoData.ParseInJson(data);
            historyCard.DateTime = DateTime.Now;
            historyCard.Description = "-";
            historyCard.WindDirection = "-";
            historyCard.WindSpeed = 0;
            //db.HistoryCards.Add(historyCard);
            //db.SaveChanges();
            return new EmptyResult();//new HtmlResult(data + "<br><br>" + JsonConvert.SerializeObject(todayCard));//Json(todayCard, JsonRequestBehavior.AllowGet).Data);  
        }

        public JsonCard OneDay(List<JsonCard> listCard) 
        {
            if (listCard.Count != 0)
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
            return null;
        }

        public ActionResult GetFutureAndPast()
        {
            Package pocket = new Package();
            pocket.Past = new List<JsonCard>();
            pocket.Future = new List<JsonCard>();
            List<JsonCard> listCard=new List<JsonCard>();
            DateTime present = DateTime.Now;
            DateTime EndDate = DateTime.Now.AddDays(5); /////////////////////////rename
            List<ForecastCard> forecastCards = db.ForecastCards.Where(x => 
                x.DateTime.Day!=present.Day &&
                x.DateTime >= present && 
                x.DateTime <= EndDate).ToList();
            EndDate = DateTime.Now.AddDays(-5);
            List<HistoryCard> historyCards = db.HistoryCards.Where(x => 
                x.DateTime.Day!=present.Day &&
                x.DateTime <= present && 
                x.DateTime > EndDate).ToList();

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
                DateTime day = present.AddDays(counter + 1 + counter);
                JsonCard card = OneDay(listCard.Where(x => x.Date == (present.AddDays(counter + 1 + counter).Day + "." + present.Month)).ToList());
                if (card!=null)
                    pocket.Future.Add(card);
                card = OneDay(listCard.Where(x => x.Date == (present.AddDays(counter - 1-counter).Day + "." + present.Month)).ToList());
                if (card != null)
                    pocket.Past.Add(card);
            }
            return Json(pocket,JsonRequestBehavior.AllowGet);
        }

        public ActionResult OpenWeatherData()
        {
            List<ForecastCard> forecastCardss = ParserOpenWeatherData.ParseInJson().ToCard();
            foreach (var card in forecastCardss)
            {
                ForecastCard forecastCards = db.ForecastCards.FirstOrDefault(x=>x.DateTime==card.DateTime);
                if (forecastCards == null)
                    db.ForecastCards.Add(card);
                else
                {
                    forecastCards.Description = card.Description;
                    forecastCards.Humidity = card.Humidity;
                    forecastCards.Temperature = card.Temperature;
                    forecastCards.WindDirection = card.WindDirection;
                    db.Entry(forecastCards).State = EntityState.Modified;
                }
            }
            db.SaveChanges();
            //-----
            List<JsonCard> jsonCards = new List<JsonCard>();
            foreach (var card in forecastCardss)
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