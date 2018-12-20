using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Terraforming.Api.Authorization;
using Terraforming.Api.Database;

namespace Terraforming.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var secretKey = Configuration["Tokens:Key"];
            _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            dbConectionString = Configuration.GetConnectionString("DefaultConnection");
            context = new DataContext(dbConectionString);
        }
        private string dbConectionString { get; }
        private DataContext context { get; }
        public IConfiguration Configuration { get; }
        private readonly SymmetricSecurityKey _signingKey;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<DataContext>(p => context);
            services.AddTransient<IAuthenticationProvider, AuthenticationProvider>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;

                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = Configuration["Tokens:Issuer"],
                    ValidAudience = Configuration["Tokens:Issuer"],
                    IssuerSigningKey = _signingKey
                };
            });
            services.AddCors();
            services.AddMvc().AddJsonOptions(o => o.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc)
            .AddJsonOptions(o => o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
                app.UseDefaultFiles();
                app.Use(async (context, next) =>
                {
                    await next();

                    if (context.Response.StatusCode == 404)
                    {
                        context.Request.Path = "/index.html";
                        await next();
                    }
                });
            }
            app.UseMvc();
        }
    }
}
