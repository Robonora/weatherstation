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

namespace Meteo.Controllers
{
    public class CardController : Controller
    {

        private CardContext db = new CardContext();

        //
        // GET: /Card/

        public ActionResult Index()
        {
            return View(db.Card.ToList());
        }

        //
        // GET: /Card/Details/5

        public ActionResult Details(int id = 0)
        {
            Card card = db.Card.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        //
        // GET: /Card/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Card/Create
        //r - осадки //нет у Наташи
        //t - температура на улице 
        //pt - температура внутри коробки метеостанции //непонятно куда
        //p - давление 
        //g - радиация
        //h - влажность
        //"temperature": "15",
        //            "humidity": "60",
        //            "wind_power": "3",
        //            "pressure": "345",
        //            "radiation": "18",
        //            "wind": "NE",
        //            "charact": "hazy"
        
        public ActionResult MeteoData(string data)
        {
            //string data = "r:533|t:15.09|h:84.17|pt:19.43|p:983.65|g:10.60=r:470|t:15.08|h:84.22|pt:19.41|p:983.53|g:6.62=r:477|t:15.11|h:84.61|pt:19.41|p:983.61|g:21.19=";
            string pattern = @"r:[0-9]+\|t:([0-9]+|[0-9]+\.[0-9]+)\|h:([0-9]+|[0-9]+\.[0-9]+)\|pt:([0-9]+|[0-9]+\.[0-9]+)\|p:([0-9]+|[0-9]+\.[0-9]+)\|g:([0-9]+|[0-9]+\.[0-9]+)=";
            string measurement, field;
            int countMeasurement=0;
            TodayCard todayCard = new TodayCard(){temperature=0,humidity=0,pressure=0,radiation=0};
            Regex regexPackage = new Regex(pattern);
            Regex regexTemperature = new Regex(@"t:([0-9]+\.[0-9]+|[0-9]+)");
            Regex regexHumidity = new Regex(@"h:([0-9]+\.[0-9]+|[0-9]+)");
            Regex regexPressure = new Regex(@"p:([0-9]+\.[0-9]+|[0-9]+)");
            Regex regexRadiation = new Regex(@"g:([0-9]+\.[0-9]+|[0-9]+)");
            Match match = regexPackage.Match(data);
            while (match.Success)
            {
                measurement = match.Value;
                field = regexTemperature.Match(measurement).Value.Remove(0,2);
                todayCard.temperature += (int)Convert.ToSingle(field, new CultureInfo("en-US"));
                field = regexHumidity.Match(measurement).Value.Remove(0,2);
                todayCard.humidity += Convert.ToSingle(field, new CultureInfo("en-US"));
                field = regexPressure.Match(measurement).Value.Remove(0,2);
                todayCard.pressure += Convert.ToSingle(field, new CultureInfo("en-US"));
                field = regexRadiation.Match(measurement).Value.Remove(0,2);
                todayCard.radiation += Convert.ToSingle(field, new CultureInfo("en-US"));
                match = match.NextMatch();
                countMeasurement++;
            }
            todayCard.temperature /=countMeasurement;
            todayCard.humidity=(float) Math.Round(todayCard.humidity / countMeasurement,2);
            todayCard.pressure=(float) Math.Round(todayCard.pressure / countMeasurement,2);
            todayCard.radiation = (float)Math.Round(todayCard.radiation / countMeasurement, 2);
            return new HtmlResult(data + "<br><br>" + JsonConvert.SerializeObject(todayCard));//Json(todayCard, JsonRequestBehavior.AllowGet).Data);
           // return RedirectToAction("Index");    
        }
        public void InsertCard(List<JsonCard> cards) {
            foreach (var card in cards)
            {
                Card current = new Card();

                current.DateTime = current.DateTime.AddSeconds(0);
                current.DateTime = current.DateTime.AddMinutes(card.Time_min);
                current.DateTime = current.DateTime.AddHours(card.Time_hour);
                current.DateTime = current.DateTime.AddDays(card.Date - current.DateTime.Day);
                current.DateTime = current.DateTime.AddMonths((Month.array[card.Month]) - current.DateTime.Month);
                current.DateTime = current.DateTime.AddYears(2015 - current.DateTime.Year);
                current.Humidity = card.Humidity;
                current.Charact = card.Charact;
                current.Temperature = card.Temperature;
                current.Wind = card.Wind;
                db.Card.Add(current);
            }
        }

        public JsonCard parsData(Card card){
            JsonCard cur = new JsonCard();
            cur.Humidity = card.Humidity;
            cur.Charact = card.Charact;
            cur.Date = card.DateTime.Day;
            //cur.Month = Month.array.FirstOrDefault(x => x.Value == card.DateTime.Month).Key;
            //cur.Month = Month.array.Keys.ElementAt(card.DateTime.Month);
            //Dictionary<string, int>.ValueCollection collection = Month.array.Values;
            cur.Month = Month.arraykek[card.DateTime.Month];
            cur.Time_hour = card.DateTime.Hour;
            cur.Time_min = card.DateTime.Minute;
            cur.Temperature = card.Temperature;
            cur.Wind = card.Wind;
            return cur;
        }

        public ActionResult GetFutureAndPast()
        {
            if (Month.array.Count() == 0)
            {
                Month.FillingMonth();
                Month.FillingMonthkek();
            }
            Package pocket = new Package();
            pocket.Past = new List<JsonCard>();
            pocket.Future = new List<JsonCard>();
            List<Card> ListCards = db.Card.ToList();
            DateTime Present = ListCards[4].DateTime;
            foreach (var card in ListCards) {
                if(card.DateTime < Present){
                    pocket.Past.Add(parsData(card));
                }
                if(card.DateTime > Present)
                    pocket.Future.Add(parsData(card));
            }
            return Json(pocket,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Insert()
        {

            string text = System.IO.File.ReadAllText(@"D:\учеба\WeatherStation\WeatherStation\info.json");
            if (Month.array.Count() == 0)
            {
                Month.FillingMonth();
                Month.FillingMonthkek();
            }
            Package package = new Package();
            package = JsonConvert.DeserializeObject<Package>(text);
            InsertCard(package.Future);
            InsertCard(package.Past);
            
            db.SaveChanges();
            return View();
        }
        //
        // GET: /Card/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Card card = db.Card.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        //
        // POST: /Card/Edit/5

        [HttpPost]
        public ActionResult Edit(Card card)
        {
            if (ModelState.IsValid)
            {
                db.Entry(card).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(card);
        }

        //
        // GET: /Card/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Card card = db.Card.Find(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        //
        // POST: /Card/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Card card = db.Card.Find(id);
            db.Card.Remove(card);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}