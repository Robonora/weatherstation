using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meteo.Models;
using Newtonsoft.Json;

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

        [HttpPost]
        public ActionResult Create(Card card)
        {
            if (ModelState.IsValid)
            {
                db.Card.Add(card);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(card);
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
                current.Air = card.Air;
                current.Charact = card.Charact;
                current.Temperature = card.Temperature;
                current.Wind = card.Wind;
                db.Card.Add(current);
            }
        }

        public JsonCard parsData(Card card){
            JsonCard cur = new JsonCard();
            cur.Air = card.Air;
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

        public ActionResult GetData()
        {
            if (Month.array.Count() == 0)
            {
                Month.FillingMonth();
                Month.FillingMonthkek();
            }
            Package pocket = new Package();
            pocket.Past = new List<JsonCard>();
            pocket.All = new List<JsonCard>();
            pocket.Future = new List<JsonCard>();
            List<Card> Cards = db.Card.ToList();
            DateTime Present = Cards[7].DateTime;
            foreach (var card in Cards) {
                if(card.DateTime < Present){
                    pocket.Past.Add(parsData(card));
                }
                if(card.DateTime == Present)
                    pocket.All.Add(parsData(card));
                else
                    pocket.Future.Add(parsData(card));
            }
            return View(pocket);
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
            InsertCard(package.All);
            
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