using Cei.Api.Common.Services;
using Cei.Api.Common.Models;

namespace Cei.Api.Academica.Services
{
    public class EncuestaService : MongoCrudService<Encuesta>
    {
        public EncuestaService(ICeiApiDB dbSettings) :
            base(dbSettings, dbSettings.Collections.Encuesta)
        {
        }
    }
}