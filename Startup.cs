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
            services.Configure<CeiApiDbConnection>(Configuration.GetSection(nameof(CeiApiDbConnection)));
            services.Configure<CeiApiKey>(Configuration.GetSection(nameof(CeiApiKey)));
            services.Configure<CeiApiDbCollection>(Configuration.GetSection(nameof(CeiApiDbCollection)));

            // Settings Dependency Injection 
            services.AddSingleton<ICeiApiDbConnection>(sp => sp.GetRequiredService<IOptions<CeiApiDbConnection>>().Value);
            services.AddSingleton<ICeiApiDbCollection>(sp => sp.GetRequiredService<IOptions<CeiApiDbCollection>>().Value);

            // Data Access Dependency Injection 
            services.AddSingleton<ICrudService<Estudiante>, EstudianteService>();
            services.AddSingleton<ICrudService<Materia>, MateriaService>();
            services.AddSingleton<ICrudService<Curso>, CursoService>();

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
