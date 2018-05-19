using LightInject;
using ReviewMe.DAL;
using ReviewMe.Services;
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
            InitializeDB.Init();

            var container = new ServiceContainer();
            container.RegisterApiControllers();
            container.EnableWebApi(GlobalConfiguration.Configuration);

            RegisterServices(container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        private void RegisterServices(ServiceContainer container)
        {
            container.Register<IDashboardStatProcessorDAL, DashboardStatProcessorDAL>();
            container.Register<IDashboardStatProcessorService, DashboardStatProcessorService>();
            container.Register<ApplicationDbContext>();
        }
    }
}
