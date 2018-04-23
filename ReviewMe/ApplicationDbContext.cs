using ReviewMe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ReviewMe
{
    public class ApplicationDbContext: DbContext
    {
        public DbSet<Player> Players { get; set; }

        public ApplicationDbContext(): base("ReviewMe")
        {

        }
    }
}