using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace ReviewMe
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            using(var db = new ApplicationDbContext())
            {
                foreach(var item in db.Players)
                {
                    item.HumanCount = 0;
                }

                db.SaveChanges();
            }
        }
    }
}
