using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AllInOne.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace AllInOne
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }


        private readonly ILogger<Startup> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {

            _logger.LogInformation("ConfigureServices called");

            services.AddDbContext<EntityContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    assembly => assembly.MigrationsAssembly(typeof(EntityContext).Assembly.FullName));
            });


            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddApiVersioning();
            services.AddApiVersioning(o => {
                //o.ApiVersionReader = new HeaderApiVersionReader("api-version");
                //o.ReportApiVersions = true;
                //o.DefaultApiVersion = new ApiVersion(1, 0);
                //o.Conventions.Controller<HelloWorldController>().HasApiVersion(new ApiVersion(2, 0));
                //o.Conventions.Controller<HelloWorld2Controller>().HasApiVersion(new ApiVersion(3, 0));
                // o.Conventions.Controller<HomeV2Controller>().HasApiVersion(new ApiVersion(3, 0));

            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "My API",
                    Version = "v1",
                    Description = "All In One API detail description",
                    TermsOfService = "https://example.com/terms",
                    Contact = new Contact
                    {
                        Name = "Ahmed BaGaLaTy",
                        Email = string.Empty,
                        Url = "https://twitter.com/BaGaLaTy"
                    },
                    License = new License
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    }
                });
                c.SwaggerDoc("v2", new Info
                {
                    Title = "My API 2",
                    Version = "v2",
                    Description = "All In One API detail description",
                    TermsOfService = "https://example.com/terms",
                    Contact = new Contact
                    {
                        Name = "Ahmed BaGaLaTy",
                        Email = string.Empty,
                        Url = "https://twitter.com/BaGaLaTy"
                    },
                    License = new License
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    }
                });
                c.SwaggerDoc("v3", new Info
                {
                    Title = "My API 3",
                    Version = "v3",
                    Description = "All In One API detail description",
                    TermsOfService = "https://example.com/terms",
                    Contact = new Contact
                    {
                        Name = "Ahmed BaGaLaTy",
                        Email = string.Empty,
                        Url = "https://twitter.com/BaGaLaTy"
                    },
                    License = new License
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    }
                });
                c.AddSecurityDefinition("Bearer",
                     new ApiKeyScheme
                     {
                         In = "header",
                         Name = "Authorization",
                         Type = "apiKey"
                     });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                    { "Bearer bogy",Enumerable.Empty<string>() },
                                        //{ "Bearer",Enumerable.Empty<string>() },

                    });
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="env">IHostingEnvironment</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            _logger.LogInformation("Configure called");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("ar-AR"),
                new CultureInfo("en-US")
            };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures,
            };

            app.UseRequestLocalization(options);


            app.UseMvc();

          //  IServiceCollection services = new ServiceCollection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                // note: need a temporary service provider here because one has not been created yet
                //var provider = services.BuildServiceProvider()
                //                       .GetRequiredService<IApiVersionDescriptionProvider>();

                //foreach (var description in provider.ApiVersionDescriptions)
                //{
                //    c.SwaggerEndpoint(
                //        $"/swagger/{description.GroupName}/swagger.json",
                //        description.GroupName.ToUpperInvariant());
                //}
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Values Api V1");
                //c.SwaggerEndpoint("/swagger/v2/swagger.json", "Values Api V2");
                //c.SwaggerEndpoint("/swagger/v3/swagger.json", "Values Api V3");


            });
        }
    }
}
