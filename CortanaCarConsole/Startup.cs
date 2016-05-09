using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;

[assembly: OwinStartup(typeof(CortanaCarConsole.Startup))]

namespace CortanaCarConsole
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            // Web API
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);

            // static files
            var fileSystem = new EmbeddedResourceFileSystem(typeof(Startup).Assembly, "CortanaCarConsole");

            app.UseDefaultFiles(new DefaultFilesOptions
            {
                DefaultFileNames = new[] { "index.html" }.ToList(),
                FileSystem = fileSystem
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileSystem = fileSystem,
                ServeUnknownFileTypes = true,
                OnPrepareResponse = context =>
                {
                    var headers = context.OwinContext.Response.Headers;
                    headers.Remove("Last-Modified");
                    headers.Remove("Etag");
                    headers.Remove("Server");
                    headers.Add("Pragma", new[] { "no-cache" });
                    headers.Add("Cache-control", new[] { "no-cache" });
                    headers.Add("Expires", new[] { "-1" });
                    headers.Add("X-Powered-By", new[] { "OWIN" });
                }
            });
        }
    }
}
