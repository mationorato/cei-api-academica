using Cei.Api.Common.Services;
using Cei.Api.Common.Models;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Cei.Api.Academica.Services
{
    public interface ICrudEncuestaRespuestaService : ICrudService<EncuestaRespuesta>
    {
        Task<bool> HasRespuestaAsync(string EncuestaId, string estudianteId);
    }

    public class EncuestaRespuestaService : MongoCrudService<EncuestaRespuesta>, ICrudEncuestaRespuestaService
    {
        public EncuestaRespuestaService(ICeiApiDB dbSettings) :
            base(dbSettings, dbSettings.Collections.EncuestaRespuesta)
        { }

        public Task<bool> HasRespuestaAsync(string EncuestaId, string estudianteId) =>
            this.collection
                .Find(r => r.EncuestaId == EncuestaId && r.EstudianteId == estudianteId)
                .AnyAsync();
    }
}