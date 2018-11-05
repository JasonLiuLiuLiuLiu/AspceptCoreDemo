using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AspceptCoreDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUserService, UserService>();
            services.AddMvc();
            services.AddDynamicProxy(config =>
            {
                //对GetUserName方法不生效
                config.NonAspectPredicates.AddMethod("GetUserName");

                //支持通配符,只对IRole开头的Servce有效
                //config.Interceptors.AddTyped<AuthenticateInterceptor>(Predicates.ForService("IRole*"));
                //支持通配符,只对Name结尾的方法有效

                config.Interceptors.AddTyped<AuthenticateInterceptor>();
            });

            return services.BuildAspectInjectorProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
