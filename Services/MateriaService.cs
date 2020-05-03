using Cei.Api.Common.Services;
using Cei.Api.Common.Models;

namespace Cei.Api.Academica.Services
{
    public class MateriaService : MongoCrudService<Materia>
    {
        public MateriaService(
            ICeiApiDbConnection connection, ICeiApiDbCollection collections) :
            base(connection, collections.MateriaCollectionName)
        {
        }
    }
}