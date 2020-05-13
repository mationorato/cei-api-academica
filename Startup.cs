using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Cei.Api.Common.Services;
using Cei.Api.Common.Models;
using Cei.Api.Academica.Services;

namespace Cei.Api.Academica
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
            // Settings Config            
            services.Configure<CeiApiDB>(Configuration.GetSection(nameof(CeiApiDB)));
            services.Configure<CeiApiKey>(Configuration.GetSection(nameof(CeiApiKey)));

            // Settings Dependency Injection 
            services.AddSingleton<ICeiApiDB>(sp => sp.GetRequiredService<IOptions<CeiApiDB>>().Value);
            services.AddSingleton<ICeiApiKey>(sp => sp.GetRequiredService<IOptions<CeiApiKey>>().Value);

            // Data Access Dependency Injection 
            services.AddSingleton<ICrudService<Estudiante>, EstudianteService>();
            services.AddSingleton<ICrudService<Materia>, MateriaService>();
            services.AddSingleton<ICrudService<Curso>, CursoService>();
            services.AddSingleton<ICrudService<Encuesta>, EncuestaService>();
            services.AddSingleton<ICrudEncuestaRespuestaService, EncuestaRespuestaService>();

            // Controllers Services
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
