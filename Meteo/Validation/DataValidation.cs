using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Meteo.Models;

namespace Meteo.Validation
{
    public static class DataValidation
    {
        public static bool AllValidation(ForecastCard forecastCard)
        {
            if (TemperatureValidation(forecastCard.Temperature) &&
                HumidityValidation(forecastCard.Humidity) &&
                WindDirectionValidation(forecastCard.WindDirection) &&
                DescriptionValidation(forecastCard.Description))
                return true;
            else
                return false;
        }
        public static bool AllValidation(HistoryCard historyCard)
        {
            if (TemperatureValidation(historyCard.Temperature) &&
                HumidityValidation(historyCard.Humidity) &&
                PressureValidation(historyCard.Pressure) &&
                RadiationValidation(historyCard.Radiation) &&
                WindDirectionValidation(historyCard.WindDirection) &&
                DescriptionValidation(historyCard.Description))
                return true;
            else
                return false;

        }
        public static bool TemperatureValidation(int temperature)
        {
            if (temperature < 45 && temperature > -45)
                return true;
            else
                return false;
        }
        public static bool HumidityValidation(float humidity)
        {
            if (humidity < 99 && humidity > 40)
                return true;
            else
                return false;
        }
        public static bool PressureValidation(float pressure)
        {
            if (pressure < 1050 && pressure > 950)
                return true;
            else
                return false;
        }
        public static bool RadiationValidation(float radiation)
        {
            if (radiation < 25 && radiation > 0)
                return true;
            else
                return false;
        }
        public static bool WindSpeedValidation(float windSpeed)
        {
            return true;
        }
        public static bool WindDirectionValidation(string windDirection)
        { 
            switch(windDirection)
            {
                case "N": return true;
                case "Ne": return true;
                case "E": return true;
                case "SE": return true;
                case "S": return true;
                case "SW": return true;
                case "W": return true;
                case "NW": return true;
                default: return false;
            }
        }
        public static bool DescriptionValidation(string description) 
        {
            switch(description)
            {
                case "Snow": return true;
                case "Clear": return true;
                case "Clouds": return true;
                case "Rain": return true;
                default: return false;
            }
        }
    }
}