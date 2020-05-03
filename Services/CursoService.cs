using Cei.Api.Common.Services;
using Cei.Api.Common.Models;

namespace Cei.Api.Academica.Services
{
    public class CursoService : MongoCrudService<Curso>
    {
        public CursoService(
            ICeiApiDbConnection connection, ICeiApiDbCollection collections) :
            base(connection, collections.CursoCollectionName)
        {
        }
    }
}