﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Meteo.Models;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Meteo.ExternalData
{
    public static class ParserMeteoData
    {
        public static HistoryCard ParseInCard(string data)
        {
            string pattern = @"r:[0-9]+\|t:([0-9]+|[0-9]+\.[0-9]+)\|h:([0-9]+|[0-9]+\.[0-9]+)\|pt:([0-9]+|[0-9]+\.[0-9]+)\|p:([0-9]+|[0-9]+\.[0-9]+)\|g:([0-9]+|[0-9]+\.[0-9]+)=";
            string measurement, field;
            int countMeasurementTemperature = 0, countMeasurementHumidity = 0, countMeasurementPressure = 0, countMeasurementRadiation = 0;
            HistoryCard todayCard = new HistoryCard() { Temperature = 0, Humidity = 0, Pressure = 0, Radiation = 0 };
            Regex regexPackage = new Regex(pattern);
            Regex regexTemperature = new Regex(@"t:([0-9]+\.[0-9]+|[0-9]+)");
            Regex regexHumidity = new Regex(@"h:([0-9]+\.[0-9]+|[0-9]+)");
            Regex regexPressure = new Regex(@"p:([0-9]+\.[0-9]+|[0-9]+)");
            Regex regexRadiation = new Regex(@"g:([0-9]+\.[0-9]+|[0-9]+)");
            Match match = regexPackage.Match(data);
            while (match.Success)
            {
                measurement = match.Value;
                field = regexTemperature.Match(measurement).Value.Remove(0, 2);
                todayCard.Temperature += (int)Convert.ToSingle(field, new CultureInfo("en-US"));
                field = regexHumidity.Match(measurement).Value.Remove(0, 2);
                todayCard.Humidity += Convert.ToSingle(field, new CultureInfo("en-US"));
                field = regexPressure.Match(measurement).Value.Remove(0, 2);
                todayCard.Pressure += Convert.ToSingle(field, new CultureInfo("en-US"));
                field = regexRadiation.Match(measurement).Value.Remove(0, 2);
                todayCard.Radiation += Convert.ToSingle(field, new CultureInfo("en-US"));
                match = match.NextMatch();
                countMeasurementHumidity++;
            }
            todayCard.Temperature /= countMeasurementHumidity;
            todayCard.Humidity = (float)Math.Round(todayCard.Humidity / countMeasurementHumidity, 2);
            todayCard.Pressure = (float)Math.Round(todayCard.Pressure / countMeasurementHumidity, 2);
            todayCard.Radiation = (float)Math.Round(todayCard.Radiation / countMeasurementHumidity, 2);
            todayCard.Description = "-";
            todayCard.WindDirection = "-";
            todayCard.WindSpeed = 0;
            HistoryCard card = new HistoryCard() { Temperature = 15, Humidity = 84.33f, Pressure = 983.6f, Radiation = 12.8f };
            todayCard.Description = "-";
            todayCard.WindDirection = "-";
            todayCard.WindSpeed = 0;
            return todayCard;
        }
    }
}