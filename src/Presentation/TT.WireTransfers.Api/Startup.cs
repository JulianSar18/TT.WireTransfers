using Microsoft.OpenApi.Models;
using TT.WireTransfers.Api.Middleware;
using TT.WireTransfers.Application.Services;
using TT.WireTransfers.Infraestructure.Persistence;

namespace TT.WireTransfers.Api
{
    public class StartUp
    {
        public IConfiguration Configuration { get; }
        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });


            services.AddCors(options =>
            {
                options.AddPolicy("AllowApp", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WireTransfers API", Version = "v1" });
            });
            services.AddPersistence(Configuration);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(app =>
            {
                app.SwaggerEndpoint("/swagger/v1/swagger.json", "WireTransfers API V1");
            });
            app.UseCors("AllowApp");
            app.UseRouting();
            //app.UseMiddleware<ExceptionMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
