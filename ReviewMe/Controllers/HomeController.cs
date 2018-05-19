using ReviewMe.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ReviewMe.Controllers
{
    public class HomeController : ApiController
    {
        IDashboardStatProcessorService dashboardStatProcessor;

        public HomeController(IDashboardStatProcessorService dashboardStatProcessor)
        {
            this.dashboardStatProcessor = dashboardStatProcessor;
        }

        [HttpGet]
        public IHttpActionResult Index()
        {
            return Ok("Api started");
        }

        [HttpPost]
        [Route("visitors/add/")]
        public async Task<int> AddHumanVisitors(string storeName, int count)
        {
            return await dashboardStatProcessor.AddHumanVisitors(storeName, count);
        }

        [HttpGet]
        [Route("visitors/count/{storeName}")]
        public async Task<int> GetVisitorsCount(string storeName)
        {
            return await dashboardStatProcessor.GetVisitorsCount(storeName);
        }

        [HttpDelete]
        [Route("visitors/reset/{storeName}")]
        public async Task ResetVisitorsCountAsync(string storeName)
        {
            await dashboardStatProcessor.DeleteVisitorsCount(storeName);
        }
    }
}
