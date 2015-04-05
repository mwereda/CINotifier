using System.IO;
using System.Web.Hosting;
using System.Web.Http;
using CINotifier;
using Swashbuckle.Application;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace CINotifier
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "CINotifier");

                    ConfigureXmlDocumentation(c);
                })
                .EnableSwaggerUi();

        }

        private static void ConfigureXmlDocumentation(SwaggerDocsConfig config)
        {
            var documentationPath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath,
                @"bin\CINotifier.xml");
            if (File.Exists(documentationPath))
            {
                config.IncludeXmlComments(documentationPath);
            }
        }
    }
}