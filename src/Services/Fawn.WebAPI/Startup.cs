using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Fawn.DAL.Abstract;
using Fawn.DAL.EFCore;
using Fawn.DAL.EFCore.Contexts;
using Fawn.WebAPI.Infrastructure.Validation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using FluentValidation.AspNetCore;
using Fawn.WebAPI.Models.Goods;
using FluentValidation;
using static Fawn.WebAPI.Features.Goods.Post;

namespace Fawn.WebAPI
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
            services.AddMvc(c =>
            {
                c.Filters.Add(new ValidateModelStateFilter());
            })
            .AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<CreateNewGoodsCommandValidator>();
                fv.RegisterValidatorsFromAssemblyContaining<GoodsModelValidator>();
                fv.ImplicitlyValidateChildProperties = true;
                fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMediatR();

            services.AddAutoMapper();

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<FawnAppContext>(options =>
                {
                    options.UseNpgsql(
                        Configuration.GetConnectionString("FawnDbConnectionString"),
                        postgresOptions =>
                        {
                            postgresOptions.EnableRetryOnFailure(
                                maxRetryCount: 10,
                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorCodesToAdd: null);
                        });
                    options.ConfigureWarnings(b =>
                    {
                        b.Throw(RelationalEventId.QueryClientEvaluationWarning);
                    });
                });

            services.AddTransient<IOrdersRepository, OrdersRepository>();
            services.AddTransient<IGoodsRepository, GoodsRepository>();
            services.AddTransient<IOrderStatusRepository, OrdersRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("fawn-api", new Info
                {
                    Title = "Fawn API",
                    Version = "1"
                });

                // Set the comments path for the Swagger JSON and UI.
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    var xmlFile = $"{assembly.GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    if (File.Exists(xmlPath))
                    {
                        c.IncludeXmlComments(xmlPath);
                    }
                }

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/fawn-api/swagger.json", "Fawn API");
                });

                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
