using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ReviewMe.Services
{
    public interface IDashboardStatProcessorService
    {
        Task<int> AddHumanVisitors(string playerName, int value);
        Task<int> GetVisitorsCount(string playerName);
        Task DeleteVisitorsCount(string playerName);
    }
}