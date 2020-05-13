using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cei.Api.Common.Models;
using Cei.Api.Common.Services;
using Cei.Api.Common.Auth;

namespace Cei.Api.Academica.Controllers
{
    [ApiKeyAuth]
    [ApiController]
    [Route("api-academica/[controller]")]
    public class MateriasController : ControllerBase
    {

        private readonly ICrudService<Materia> service;

        public MateriasController(ICrudService<Materia> service)
        {
            this.service = service;
        }

        [HttpGet("{id:length(24)}", Name = "GetMateriaById")]
        public async Task<ActionResult> GetById(string id)
        {
            var materia = await this.service.GetByIdAsync(id);

            if (materia == null)
                return NotFound();

            return Ok(materia);
        }

        [HttpGet("codigo/{codigo}")]
        public async Task<ActionResult> GetByCodigo(string codigo)
        {
            if (!codigo.Contains("."))
                codigo = codigo.Insert(2, ".");

            var materia = await this.service.GetFirstAsync(m => m.Codigo == codigo);

            if (materia == null)
                return NotFound();

            return Ok(materia);
        }

        [HttpGet("buscar/{busqueda}")]
        public async Task<ActionResult> GetByTextSearch(
            string busqueda, bool expandir)
        {
            var materias = expandir ?
                await this.service.GetByTextSearch("\"" + busqueda + "\"") :
                await this.service.GetByTextSearch<Materia>(
                    busqueda, x => new Materia()
                    {
                        Id = x.Id,
                        Codigo = x.Codigo,
                        Nombre = x.Nombre
                    });

            if (materias.Count == 0)
                return NotFound();

            return Ok(materias);
        }
    }
}