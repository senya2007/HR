using ReviewMe.Helpers;
using ReviewMe.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ReviewMe
{
    public class DashboardStatProcessorService : IDashboardStatProcessorService
    {
        private static IAsyncLock _lock = new AsyncLock();

        IDashboardStatProcessorDAL dashboardDAL;

        public DashboardStatProcessorService(IDashboardStatProcessorDAL dashboard)
        {
            dashboardDAL = dashboard;
        }

        public async Task<int> AddHumanVisitors(string playerName, int value)
        {
            using (await _lock.LockAsync())
            {
                return await dashboardDAL.AddHumanVisitors(playerName, value);
            }
        }

        public async Task<int> GetVisitorsCount(string playerName)
        {
            using (await _lock.LockAsync())
            {
                return await dashboardDAL.GetVisitorsCount(playerName);
            }
        }

        public async Task DeleteVisitorsCount(string playerName)
        {
            using (await _lock.LockAsync())
            {
                await dashboardDAL.DeleteVisitorsCount(playerName);
            }
        }
    }
}