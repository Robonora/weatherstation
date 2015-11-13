using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Meteo.Models
{
    //public class HistoryCardContext : DbContext
    //{
    //    public DbSet<HistoryCard> HistoryCards { get; set; }
    //}
    public class CardContext : DbContext
    {
        public DbSet<ForecastCard> ForecastCards { get; set; }
        public DbSet<HistoryCard> HistoryCards { get; set; }
    }
}