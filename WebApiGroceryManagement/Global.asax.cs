using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApiGroceryManagement.Models;
namespace WebApiGroceryManagement
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        static Timer timer = new Timer();


        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //code for timer
            timer.Interval = new TimeSpan(1, 0, 0, 0).TotalMilliseconds;//one day
            timer.Elapsed += timer_Tick;
        }

        //code for timer
        private void timer_Tick(object sender, ElapsedEventArgs e)
        {
            //SEND Every day a push notification about the todays notes
            TimerServices.SendPushToAllNotes();
        }
    }
}
