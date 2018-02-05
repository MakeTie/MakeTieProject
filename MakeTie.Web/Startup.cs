using MakeTie.Bll.Interfaces;
using MakeTie.Bll.ProductProviders;
using MakeTie.Bll.Services;
using MakeTie.Bll.Utils;
using MakeTie.Bll.Utils.Interfaces;
using MakeTie.Web.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MakeTie.Web
{
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
            services.AddMvc();

            RegisterDependencies(services);
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

        private void RegisterDependencies(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IAssociationSettings, AssociationSettings>();
            services.AddTransient<IEBaySettings, EBaySettings>();

            services.AddTransient<IHttpUtil, HttpUtil>();
            services.AddTransient<ISentimentService, StubSetimentService>();
            services.AddTransient<IAssociationService, AssociationsService>();
            services.AddTransient<IProductProvider, EBayProductProvider>();
            services.AddTransient<IProductService, ProductService>();
        }
    }
}