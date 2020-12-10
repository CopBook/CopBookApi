using CopBookApi.Filters.Logging;
using CopBookApi.Interfaces.Services.Auth;
using CopBookApi.Interfaces.Services.Logging;
using CopBookApi.Models.Services.Firebase;
using CopBookApi.Models.Services.Sidelog;
using CopBookApi.Services.Firebase;
using CopBookApi.Services.Sidelog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace CopBookApi
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
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://securetoken.google.com/copbook";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/copbook",
                        ValidateAudience = true,
                        ValidAudience = "copbook",
                        ValidateLifetime = true
                    };
                });
        
            services.AddControllers(options =>
            {
                options.Filters.Add<LoggingActionFilter>();
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CopBookApi", Version = "v1" });
            });

            services.AddHttpClient();

            services.Configure<FirebaseSettings>(
                Configuration.GetSection(nameof(FirebaseSettings)));
            services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<FirebaseSettings>>().Value);

            services.AddSingleton<IAuthenticationService, FirebaseAuthService>();

            services.Configure<SidelogServiceConfig>(
                Configuration.GetSection(nameof(SidelogServiceConfig)));
            services.AddSingleton(sp =>
                sp.GetRequiredService<IOptions<SidelogServiceConfig>>().Value);

            services.AddSingleton<ILoggingService, SidelogService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CopBookApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
