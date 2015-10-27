using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Meteo.Models
{
    public class CardContext: DbContext
    {
        public DbSet<Card> Card { get; set; }
    }
}