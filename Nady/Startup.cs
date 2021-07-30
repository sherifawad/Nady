using DataBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nady.Extensions;
using Nady.Middleware;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Nady
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public IConfiguration _Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                //// fix json cycle
                //options.OutputFormatters.RemoveType<SystemTextJsonOutputFormatter>();
                //options.OutputFormatters.Add(new SystemTextJsonOutputFormatter(new JsonSerializerOptions(JsonSerializerDefaults.Web)
                //{
                //    ReferenceHandler = ReferenceHandler.Preserve,
                //}));

                // Fix error "No route matches the supplied values" when using async suffix in controlleres methods name 
                options.SuppressAsyncSuffixInActionNames = false;
            });
            services.AddDbContext<IDatabaseContext, NadyDataContext>(x => x.UseSqlite(_Configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly(nameof(Infrastructure))));

            services.AddApplicationServices();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Nady", Version = "v1",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Sherif Awad",
                        Email = "shereifawad@gmail.com"
                    }
                });

                // genrate the xml docs that'll drive the swagger docs
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                //show controller methods name in swagger
                c.CustomOperationIds(apiDescriotion => apiDescriotion.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null);
            });

            //check if api service is running
            //AddSqlite Check for database is health 

            services.AddHealthChecks()
                //.AddSqlite(sqliteConnectionString: _Configuration.GetConnectionString("DefaultConnection"),
                //healthQuery: "SELECT 1;",
                //name: "Sqlite",
                //timeout: TimeSpan.FromSeconds(3),
                //tags: new[] { "ready" })
                .AddCheck<DatabaseHealthCheck>(
                name: "Sqlite",
                timeout: TimeSpan.FromSeconds(3),
                tags: new[] { "ready" });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nady v1");
                    c.DisplayOperationId();
                });
            }

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions { 
                    Predicate = check => check.Tags.Contains("ready"),
                    ResponseWriter = async(context, report) =>
                    {
                        var result = JsonSerializer.Serialize(
                            new
                            {
                                status = report.Status.ToString(),
                                checks = report.Entries.Select(entry => new
                                {
                                    name = entry.Key,
                                    status = entry.Value.Status.ToString(),
                                    exception = entry.Value.Exception != null ?entry.Value.Exception.Message : "None",
                                    duration = entry.Value.Duration.ToString()
                                })
                            }

                        );
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }

                });
                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions { Predicate = _ => false});
            });
        }
    }
}
