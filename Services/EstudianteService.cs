using Cei.Api.Common.Services;
using Cei.Api.Common.Models;

namespace Cei.Api.Academica.Services
{
    public class EstudianteService : MongoCrudService<Estudiante>
    {
        public EstudianteService(
            ICeiApiDbConnection connection, ICeiApiDbCollection collections) :
            base(connection, collections.EstudianteCollectionName)
        {
        }
    }
}