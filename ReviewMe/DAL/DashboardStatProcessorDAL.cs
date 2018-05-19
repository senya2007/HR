using ReviewMe.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ReviewMe.DAL
{
    public class DashboardStatProcessorDAL : IDashboardStatProcessorDAL
    {
        ApplicationDbContext db;

        public DashboardStatProcessorDAL(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<int> AddHumanVisitors(string playerName, int value)
        {
            if (string.IsNullOrEmpty(playerName))
                throw new ArgumentNullException("Empty playerName");
            if (value < 0)
                throw new ArgumentOutOfRangeException("Empty value");

            var player = await db.Stores.SingleOrDefaultAsync(x => x.Name == playerName);
            if (player == null)
            {
                player = db.Stores.Add(new Models.Store() { Name = playerName, HumanCount = value });
            }
            else
            {
                player.HumanCount += value;
            }
            await db.SaveChangesAsync();

            return player.HumanCount;
        }

        public async Task<int> GetVisitorsCount(string playerName)
        {
            if (string.IsNullOrEmpty(playerName))
                throw new ArgumentNullException("Empty playerName");

            var player = await db.Stores.SingleOrDefaultAsync(x => x.Name == playerName);
            if (player == null)
            {
                throw new VisitorNotFoundException(playerName);
            }
            return player.HumanCount;
        }

        public async Task DeleteVisitorsCount(string playerName)
        {
            if (string.IsNullOrEmpty(playerName))
                throw new ArgumentNullException("Empty playerName");

            var player = await db.Stores.SingleOrDefaultAsync(x => x.Name == playerName);
            if (player == null)
            {
                throw new VisitorNotFoundException(playerName);
            }
            player.HumanCount = 0;
            await db.SaveChangesAsync();
        }
    }
}