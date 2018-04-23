using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ReviewMe.Controllers
{
    [Route("api/home/")] 
    public class HomeController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Index()
        {
            return Ok("Api started");
        }

        private static IAsyncLock _lock = new AsyncLock();

        [HttpGet]
        [Route("add")]
        public async Task<IHttpActionResult> AddHumanVisitors(string player, int count)
        {          
            {
                await Task.Delay(1000);

                if (DashboardStatProcessor.AddHumanVisitors(player, count).Result)
                {
                    return Ok();
                }
            }
            return InternalServerError();
        }

        [HttpGet]
        [Route("visitors/count")]
        public int GetVisitorsCount(string player)
        {
            return DashboardStatProcessor.GetVisitorsCount(player);            
        }

        [HttpDelete]
        [Route("visitors/count")]
        public async void DeleteVisitorsCount(string player)
        {
            using (await _lock.LockAsync())
            {
                DashboardStatProcessor.GetVisitorsCount(player);
            }
        }
    }
}
