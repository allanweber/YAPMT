using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using YAPMT.Domain.Repositories;
using YAPMT.Domain.Services;
using YAPMT.Framework.Constants;
using YAPMT.Framework.Filters;
using YAPMT.Framework.Middlewares;
using YAPMT.Framework.Repositories;
using YAPMT.Infrastructure.Repositories;
using YAPMT.Infrastructure.Services;

namespace YAPMT.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PrincipalDbContext>(options =>
            {
                if (this.Environment.IsEnvironment("IntegrationTests"))
                {
                    options.UseInMemoryDatabase("IntegrationTests");
                }
                else
                {
                    options.UseMySQL(this.Configuration.GetConnectionString("RelationalConnection"));
                }
            });

            services.AddMvc().AddMvcOptions(setup => setup.Filters.Add<CommandResultFilterAttribute>());

            services.AddAutoMapper();

            services.AddMediatR();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1",
                info: new Info
                {
                    Title = "YAPMT",
                    Version = "v1",
                    Description = "YET ANOTHER PROJECT MANAGEMENT TOOL",
                    Contact = new Contact
                    {
                        Name = "Allan Cassiano Weber",
                        Url = "https:/github.com/allanweber"
                    }
                });

                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "YAPMT.xml");
                s.IncludeXmlComments(xmlPath);
            });

            services.AddCors(o => o.AddPolicy(AppConstants.ALLOWALLHEADERS, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IAssignmentRepository, AssignmentRepository>();

            services.AddScoped<IProjectService, ProjectService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseMvcWithDefaultRoute();

            app.UseCors(AppConstants.ALLOWALLHEADERS);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "YAPMT");
            });
        }
    }
}
