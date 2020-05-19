using Cei.Api.Common.Services;
using Cei.Api.Common.Models;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq;

namespace Cei.Api.Academica.Services
{
    public interface ICrudEncuestaRespuestaService : ICrudService<EncuestaRespuesta>
    {
        Task<bool> HasRespuestaAsync(string EncuestaId, string estudianteId);
        Task<bool> HasRespuestaMateriaAsync(string encuestaId, string estudianteId, string materiaCodigo);
    }

    public class EncuestaRespuestaService : MongoCrudService<EncuestaRespuesta>, ICrudEncuestaRespuestaService
    {
        public EncuestaRespuestaService(ICeiApiDB dbSettings) :
            base(dbSettings, dbSettings.Collections.EncuestaRespuesta)
        { }

        public Task<bool> HasRespuestaAsync(string encuestaId, string estudianteId) =>
            this.collection
                .Find(r => r.EncuestaId == encuestaId && r.EstudianteId == estudianteId)
                .AnyAsync();

        public async Task<bool> HasRespuestaMateriaAsync(string encuestaId, string estudianteId, string materiaCodigo)
        {
            var respuestas = await this.collection.Find(
                r =>
                r.EncuestaId == encuestaId &&
                r.EstudianteId == estudianteId &&
                r.Detalles.ContainsKey("materia_codigo")
            ).ToListAsync();

            if (respuestas.Count == 0)
                return false;

            var tieneRespuestaMateria = respuestas.Any(
                r => r.Detalles["materia_codigo"] == materiaCodigo
            );

            return tieneRespuestaMateria;
        }
    }
}