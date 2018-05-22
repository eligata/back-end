using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.API.Data.Services;
using App.API.Helpers;
using APP.API.Data;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace App
{
    public class Startup
    {
        #region Fields and Properties

        private JwtConfigurator JwtConfigurator;

        #endregion

        #region CTOR

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtConfigurator = JWTConfigurator();
        }

        #endregion

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Configure JWT auth
            ConfigureJWTAuth(services);

            // Add application services.
            services.AddTransient(jwt => JwtConfigurator);
            services.AddTransient<TokenManager>();
            services.AddTransient<AccountService>();
            services.AddTransient<ProductService>();
            services.AddTransient<BasketService>();

            services.AddMvc();
            services.AddCors();
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });

            app.UseAuthentication();
            app.UseMvc();
        }

        // This method gets JWT config options form appConfig
        private JwtConfigurator JWTConfigurator()
        {
            // Get JWT options from app settings
            IConfigurationSection jwtOptions = Configuration.GetSection("JwtConfig");

            JwtConfigurator JwtConfigurator = new JwtConfigurator
            {
                SecretKey = jwtOptions[nameof(JwtConfigurator.SecretKey)],
                Issuer = jwtOptions[nameof(JwtConfigurator.Issuer)],
                Audience = jwtOptions[nameof(JwtConfigurator.Audience)],
                ValidFor = TimeSpan.FromMinutes(Convert.ToInt32(jwtOptions[nameof(JwtConfigurator.ValidFor)]))
            };

            JwtConfigurator.SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtConfigurator.SecretKey));
            JwtConfigurator.SigningCredentials = new SigningCredentials(JwtConfigurator.SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            return JwtConfigurator;
        }

        // Use this method to configure JWT Auth
        private void ConfigureJWTAuth(IServiceCollection services)
        {
            // Configure JwtConfigurator
            services.Configure<JwtConfigurator>(options =>
            {
                options.Issuer = JwtConfigurator.Issuer;
                options.Audience = JwtConfigurator.Audience;
                options.SigningCredentials = JwtConfigurator.SigningCredentials;
            });

            // Validation params
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = JwtConfigurator.Issuer,

                ValidateAudience = true,
                ValidAudience = JwtConfigurator.Audience,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = JwtConfigurator.SymmetricSecurityKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = JwtConfigurator.Issuer;
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // API user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AuthenticatedUser", policy => policy.RequireAuthenticatedUser());
            });
        }
    }
}
