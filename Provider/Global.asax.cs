using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Provider
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly object BehaviorInitializationSyncObject = new object();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest(object sender, EventArgs e) {
            InitializeBehaviors();
        }

        private static void InitializeBehaviors() {
            if (DotNetOpenAuth.OpenId.Provider.Behaviors.PpidGeneration.PpidIdentifierProvider == null) {
                lock (BehaviorInitializationSyncObject) {
                    if (DotNetOpenAuth.OpenId.Provider.Behaviors.PpidGeneration.PpidIdentifierProvider == null) {
                        DotNetOpenAuth.OpenId.Provider.Behaviors.PpidGeneration.PpidIdentifierProvider = new Models.AnonymousIdentifierProvider();
                        DotNetOpenAuth.OpenId.Provider.Behaviors.GsaIcamProfile.PpidIdentifierProvider = new Models.AnonymousIdentifierProvider();
                    }
                }
            }
        }
    }
}