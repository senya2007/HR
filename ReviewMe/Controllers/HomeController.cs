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
        private static IAsyncLock _lock = new AsyncLock();

        [HttpGet]
        public IHttpActionResult Index()
        {
            return Ok("Api started");
        }        

        [HttpGet]
        [Route("add")]
        public async Task<IHttpActionResult> AddHumanVisitors(string storeName, int count)
        {          
            {
                if (DashboardStatProcessor.AddHumanVisitors(storeName, count).Result)
                {
                    return Ok();
                }
            }
            return InternalServerError();
        }

        [HttpGet]
        [Route("visitors/count")]
        public int GetVisitorsCount(string storeName)
        {
            return DashboardStatProcessor.GetVisitorsCount(storeName);            
        }

        [HttpDelete]
        [Route("visitors/count")]
        public async void DeleteVisitorsCount(string storeName)
        {
            using (await _lock.LockAsync())
            {
                DashboardStatProcessor.GetVisitorsCount(storeName);
            }
        }
    }
}
