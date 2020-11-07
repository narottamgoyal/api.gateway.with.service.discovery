using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace api.gateway
{
    public class Constants
    {
        public const string Authority = "http://localhost:5000";
        public const string ApiResourceName = "api.portfolio.manager.v1";
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            //Action<IdentityServerAuthenticationOptions> options = o =>
            //{
            //    o.Authority = "http://localhost:5000";
            //    o.ApiName = "api.portfolio.manager.v1";
            //    o.SupportedTokens = SupportedTokens.Both;
            //    o.ApiSecret = "secret";
            //};

            var authenticationProviderKey = "TestKey";
            services.AddAuthentication()
                .AddIdentityServerAuthentication(authenticationProviderKey, options =>
                {
                    options.Authority = Constants.Authority;
                    options.RequireHttpsMetadata = false;
                    // ApiResourceName
                    options.ApiName = Constants.ApiResourceName;
                });
            services.AddOcelot();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();
            app.UseOcelot().Wait();
            app.UseMiddleware<CustomMiddleware>();
        }
    }
}
