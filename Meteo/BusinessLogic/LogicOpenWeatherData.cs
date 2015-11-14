using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Meteo.Models;
using Meteo.Validation;
using System.Data;
using System.Data.Entity;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Net;
using System.Web.Script.Serialization;

namespace Meteo.BusinessLogic
{
    public static class LogicOpenWeatherData
    {
        public static bool UpdateData(CardContext db, List<ForecastCard> forecastCards)
        {
            foreach (var card in forecastCards)
            {
                if (DataValidation.AllValidation(card))
                {
                    ForecastCard forecastCard = db.ForecastCards.FirstOrDefault(x => x.DateTime == card.DateTime);
                    if (forecastCard == null)
                        db.ForecastCards.Add(card);
                    else
                    {
                        forecastCard.Description = card.Description;
                        forecastCard.Humidity = card.Humidity;
                        forecastCard.Temperature = card.Temperature;
                        forecastCard.WindDirection = card.WindDirection;
                        db.Entry(forecastCard).State = EntityState.Modified;
                    }
                }
            }
            try 
            {
                db.SaveChanges();
                return true;
            }
            catch(Exception ex){
                return false;
            };
        }
    }
}