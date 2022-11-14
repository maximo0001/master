using Microsoft.OpenApi.Models;
namespace ProyectoFinalCoderHouse
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(policy =>
            {
                policy.AddDefaultPolicy(options =>
               options.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProyectoFinalCoderHouse", Version = "v1" });
            });
        }
        public void ConfigureServices(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
            }
        }

    }
}
