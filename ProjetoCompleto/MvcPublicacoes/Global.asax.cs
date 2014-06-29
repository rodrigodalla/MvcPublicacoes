using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using MvcPublicacoes.Persistencia;
using MvcPublicacoes.Models;
using System.Globalization;

namespace MvcPublicacoes
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Database.SetInitializer<PubContext>(new MvcPublicacoes.Persistencia.Config());
            //Database.SetInitializer(new MvcPublicacoes.Persistencia.EntidadesInicializacao());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<Entidades, Config>());
            //ModelBinders.Binders.Add(typeof(DateTime), new MyDateTimeModelBinder());
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }
       
        protected void Application_BeginRequest()
        {
            CultureInfo info = new CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.ToString());
            info.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            System.Threading.Thread.CurrentThread.CurrentCulture = info;
        }
    }
}