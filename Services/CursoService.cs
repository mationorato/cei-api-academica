using Cei.Api.Common.Services;
using Cei.Api.Common.Models;

namespace Cei.Api.Academica.Services
{
    public class CursoService : MongoCrudService<Curso>
    {
        public CursoService(ICeiApiDB dbSettings) :
            base(dbSettings, dbSettings.Collections.Curso)
        {
        }
    }
}