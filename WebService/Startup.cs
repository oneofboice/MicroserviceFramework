using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Http;
using WebService.App_Start;
using Microsoft.Owin;
using Owin;

//[assembly: OwinStartup(typeof(WebService.Startup))]

namespace WebService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            HttpConfiguration config = new HttpConfiguration();
            
            RouteConfig.Configure(config);
            AutofacConfig.Configure(config);
            SwaggerConfig.Configure(config);
            
            app.UseWebApi(config);
        }
    }
}