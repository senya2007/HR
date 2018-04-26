using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ReviewMe
{
    public class DashboardStatProcessor 
    {
        public static Dictionary<string, int> _statisticData;

        public static async Task<bool> AddHumanVisitors(string playerName, int value)
        {
            using (var db = new ApplicationDbContext())            {
                var player = await db.Stores.SingleAsync(x => x.Name == playerName);
                player.HumanCount += value;                
                db.SaveChanges();
            }                           
            return true;
        }

        public static int GetVisitorsCount(string playerName)
        {
            using (var db = new ApplicationDbContext())
            {
                var player = db.Stores.FirstOrDefault(x => x.Name == playerName);
                return player.HumanCount;
            }
        }

        public static void DeleteVisitorsCount(string playerName)
        {
            using (var db = new ApplicationDbContext())
            {
                var player = db.Stores.FirstOrDefault(x => x.Name == playerName);
                player.HumanCount = 0;
                db.SaveChanges();
            }
        }
    }
}