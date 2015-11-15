using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using NUnit.Framework;
using Meteo.Models;
using Meteo.Validation;

namespace Meteo.UnitTests
{
    [TestFixture]
    class ValidationTests
    {
        [Test]
        public void AllValidation_CorrectHistoryCard()
        {
            HistoryCard card = new HistoryCard() { Temperature = 15, 
                                                   Humidity = 84.33f, 
                                                   Pressure = 983.6f, 
                                                   Radiation = 12.8f, 
                                                   Description = "-", 
                                                   WindDirection = "NE", 
                                                   WindSpeed = 23 };
            Assert.IsTrue(DataValidation.AllValidation(card));
        }
        [Test]
        public void AllValidation_CorrectForecastCard()
        {
            ForecastCard card = new ForecastCard()
            {
                Temperature = 15,
                Humidity = 84.33f,
                Description = "-",
                WindDirection = "NE"
            };
            Assert.IsTrue(DataValidation.AllValidation(card));
        }
        [Test]
        public void AllValidation_HistoryCardIsNull()
        {
            HistoryCard card = null;
            Assert.IsFalse(DataValidation.AllValidation(card));
        }
        [Test]
        public void AllValidation_ForecastCardIsNull()
        {
            ForecastCard card = null;
            Assert.IsFalse(DataValidation.AllValidation(card));
        }
        [Test]
        public void TemperatureValidation_CorrectData()
        {
            Assert.IsTrue(DataValidation.TemperatureValidation(2));
        }
        [Test]
        public void TemperatureValidation_IncorrectData()
        {
            Assert.IsFalse(DataValidation.TemperatureValidation(88));
        }
        [Test]
        public void HumidityValidation_CorrectData()
        {
            Assert.IsTrue(DataValidation.HumidityValidation(88));
        }
        [Test]
        public void HumidityValidation_IncorrectData()
        {
            Assert.IsFalse(DataValidation.HumidityValidation(2));
        }
        [Test]
        public void PressureValidation_CorrectData()
        {
            Assert.IsTrue(DataValidation.PressureValidation(988));
        }
        [Test]
        public void PressureValidation_IncorrectData()
        {
            Assert.IsFalse(DataValidation.PressureValidation(2));
        }
        [Test]
        public void RadiationValidation_CorrectData()
        {
            Assert.IsTrue(DataValidation.RadiationValidation(2));
        }
        [Test]
        public void RadiationValidation_IncorrectData()
        {
            Assert.IsFalse(DataValidation.RadiationValidation(88));
        }
        [Test]
        public void WindDirectionValidation_CorrectData()
        {
            Assert.IsTrue(DataValidation.WindDirectionValidation("NE"));
        }
        [Test]
        public void WindDirectionValidation_IncorrectData()
        {
            Assert.IsFalse(DataValidation.WindDirectionValidation("88"));
        }
        [Test]
        public void WindDirectionValidation_IsNull()
        {
            Assert.IsFalse(DataValidation.WindDirectionValidation(null));
        }
        [Test]
        public void DescriptionValidation_CorrectData()
        {
            Assert.IsTrue(DataValidation.DescriptionValidation("Clear"));
        }
        [Test]
        public void DescriptionValidation_IncorrectData()
        {
            Assert.IsFalse(DataValidation.DescriptionValidation("88"));
        }
        [Test]
        public void DescriptionValidation_IsNull()
        {
            Assert.IsFalse(DataValidation.DescriptionValidation(null));
        }
    }
}
