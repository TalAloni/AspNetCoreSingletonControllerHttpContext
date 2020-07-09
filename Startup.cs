using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreSingletonControllerHttpContext
{
    public class Startup : IStartup
    {
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            List<Type> controllerTypes = new List<Type>();
            controllerTypes.Add(typeof(PingController));
            services.AddMvcCore().AddControllersAsServices();

            foreach (Type controllerType in controllerTypes)
            {
                // Note: replacing AddSingleton to AddTransient here will resolve the issue:
                services.AddSingleton(controllerType);
            }

            return services.BuildServiceProvider();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller}/{action}");
            });
        }
    }
}
