using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SystemRD1.Api.Configurations;
using SystemRD1.Api.Configurations.Swagger;

namespace SystemRD1.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //Identity
            services.AddIdentityConfiguration(Configuration);

            //Automapper Configuration
            services.AddAutoMapper(typeof(Startup));

            //Api Configuration
            services.AddApiConfiguration();

            //Swagger
            services.AddSwaggerConfig();

            //DataBase Configuration
            services.AddDbConfiguration(Configuration);

            //Dependency Injection Configuration
            services.AddDependencyInjectionConfiguration();
            
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwaggerConfiguration(provider);

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
