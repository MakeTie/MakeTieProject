using System;
using AutoMapper;
using Google.Cloud.Language.V1;
using MakeTie.Bll.Interfaces;
using MakeTie.Bll.Mapping.Profiles;
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
            var environmentVariableName = Configuration["EntityAnalysisSettings:EnvironmentVariableName"];
            var googleCredentialsPath = Configuration["EntityAnalysisSettings:GoogleCredentialsPath"];
            Environment.SetEnvironmentVariable(environmentVariableName, googleCredentialsPath);

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
            RegisterConfiguration(services);
            RegisterMapping(services);
            RegisterServices(services);
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IProductProvider, EBayProductProvider>();
            services.AddTransient<IHttpUtil, HttpUtil>();
            services.AddTransient<IAssociationService, AssociationsService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IEntityAnalysisService, EntityAnalysisService>();
            services.AddSingleton(LanguageServiceClient.Create());
        }

        private void RegisterMapping(IServiceCollection services)
        {
            services.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductProfile(provider.GetService<IEBaySettings>()));
                cfg.AddProfile(new AnalysisProfile());
            }).CreateMapper());
        }

        private void RegisterConfiguration(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IAssociationSettings, AssociationSettings>();
            services.AddTransient<IEBaySettings, EBaySettings>();
        }
    }
}