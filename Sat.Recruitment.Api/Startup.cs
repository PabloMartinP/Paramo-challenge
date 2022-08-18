using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Builders;
using Sat.Recruitment.Api.Builders.Contracts;
using Sat.Recruitment.Api.Entity.Repositories;
using Sat.Recruitment.Api.Entity.Repositories.Contracts;
using Sat.Recruitment.Api.Model.Users.Request.Validations;
using Sat.Recruitment.Api.Services;
using Sat.Recruitment.Api.Services.CalculateTipUserStrategy;
using Sat.Recruitment.Api.Services.CalculateTipUserStrategy.Contracts;
using Sat.Recruitment.Api.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api
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
            //services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITipStrategyFactory, TipStrategyFactory>();

            //repositories                        
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDbUsers, CsvUsers>();

            //builders
            services.AddScoped<IUserBuilder, UserBuilder>();
            services.AddScoped<IResultBuilder, ResultBuilder>();


            services.AddTransient<CreateUserRequestValidator>();

            services.AddControllers();
            services.AddSwaggerGen();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
