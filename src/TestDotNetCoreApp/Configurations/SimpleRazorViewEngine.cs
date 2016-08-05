using System.Collections.Generic;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TestDotNetCoreApp.Configurations
{
    public class SimpleRazorViewEngine : RazorViewEngine
    {
        public SimpleRazorViewEngine(IRazorPageFactoryProvider pageFactory, IRazorPageActivator pageActivator,
            HtmlEncoder htmlEncoder, IOptions<RazorViewEngineOptions> optionsAccessor, ILoggerFactory loggerFactory)
            : base(pageFactory, pageActivator, htmlEncoder, optionsAccessor, loggerFactory)
        {
            optionsAccessor.Value.ViewLocationFormats.Clear();
            optionsAccessor.Value.ViewLocationFormats.Add("~/wwwroot/app/components/{1}/{0}.html");
            optionsAccessor.Value.ViewLocationFormats.Add("~/wwwroot/app/{1}/{0}.html");
            optionsAccessor.Value.ViewLocationFormats.Add("~/wwwroot/app/{0}.html");
        }

    }
    public static class MyRazorViewEngineExtension
    {
        public static void ReplaceDefaultViewEngine(this IServiceCollection services)
        {
            services.Replace(ServiceDescriptor.Singleton<IRazorViewEngine, SimpleRazorViewEngine>());
        }
    }
}
