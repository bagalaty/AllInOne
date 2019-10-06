using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AllInOne.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Microsoft.IdentityModel.Tokens;
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


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                     .AddJwtBearer(options =>
                     {
                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidateIssuer = true,
                             ValidateAudience = true,
                             ValidateLifetime = true,
                             ValidateIssuerSigningKey = true,
                             ValidIssuer = Configuration["JwtSettings:Issuer"],
                             ValidAudience = Configuration["JwtSettings:Issuer"],
                             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:Key"]))
                         };
                     });

         

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddMvcCore()
      .AddJsonFormatters()
      .AddVersionedApiExplorer(
            options =>
            {
                  //The format of the version added to the route URL  
                  options.GroupNameFormat = "'v'VVV";
                  //Tells swagger to replace the version in the controller route  
                  options.SubstituteApiVersionInUrl = true;
            }); ;

            services.AddApiVersioning(options => options.ReportApiVersions = true);
            services.AddSwaggerGen(
                options =>
                {
            // Resolve the temprary IApiVersionDescriptionProvider service  
            var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

            // Add a swagger document for each discovered API version  
            foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerDoc(description.GroupName, new Info()
                        {
                            Title = $"{this.GetType().Assembly.GetCustomAttribute<AssemblyProductAttribute>().Product} {description.ApiVersion}",
                            Version = description.ApiVersion.ToString(),
                            Description = description.IsDeprecated ?
                            $"{this.GetType().Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description} - DEPRECATED" : 
                            this.GetType().Assembly.GetCustomAttribute<AssemblyDescriptionAttribute>().Description,
                            TermsOfService = "https://example.com/terms",
                            Contact = new Contact{Name = "Ahmed BaGaLaTy",Email = string.Empty,Url = "https://twitter.com/BaGaLaTy"},
                            License = new License{Name = "Use under LICX",Url = "https://example.com/license"}
                        });
                    }

                    options.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Name = "Authorization", Type = "apiKey" });


                    // Add a custom filter for settint the default values  
                    options.OperationFilter<SwaggerDefaultValues>();

            // Tells swagger to pick up the output XML document file  
            options.IncludeXmlComments(Path.Combine(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), $"{this.GetType().Assembly.GetName().Name}.xml"
                        ));

                });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="env">IHostingEnvironment</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
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

            app.UseAuthentication();

          

          //  IServiceCollection services = new ServiceCollection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
              

                //Build a swagger endpoint for each discovered API version  
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }

            });

            app.UseMvc();
        }
    }
}
