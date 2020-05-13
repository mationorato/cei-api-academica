using Cei.Api.Common.Services;
using Cei.Api.Common.Models;

namespace Cei.Api.Academica.Services
{
    public class EstudianteService : MongoCrudService<Estudiante>
    {
        public EstudianteService(ICeiApiDB dbSettings) :
            base(dbSettings, dbSettings.Collections.Estudiante)
        {
        }
    }
}