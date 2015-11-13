﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Meteo.Models
{
    public class ForecastCard
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public int Temperature { get; set; }
        [Required]
        public float Humidity { get; set; }
        [Required]
        public string WindDirection { get; set; }
        [Required]
        public string Description { get; set; }
    }
    public class HistoryCard
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public int Temperature { get; set; }
        [Required]
        public float Humidity { get; set; }
        [Required]
        public float Pressure { get; set; }
        [Required]
        public float Radiation { get; set; }
        [Required]
        public float WindSpeed { get; set; }
        [Required]
        public string WindDirection { get; set; }
        [Required]
        public string Description { get; set; }
    }
}