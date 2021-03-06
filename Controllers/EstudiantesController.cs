using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cei.Api.Common.Auth;
using Cei.Api.Common.Models;
using Cei.Api.Common.Services;

namespace Cei.Api.Academica.Controllers
{
    [ApiKeyAuth]
    [ApiController]
    [Route("api-academica/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly ICrudService<Estudiante> service;
        public EstudiantesController(ICrudService<Estudiante> service)
        {
            this.service = service;
        }

        [HttpGet("{id:length(24)}", Name = "GetEstudianteById")]
        public async Task<ActionResult> GetById(string id)
        {
            var estudiante = await service.GetByIdAsync(id);

            if (estudiante == null)
                return NotFound();

            return Ok(estudiante);
        }

        [HttpGet("padron/{padron}")]
        public async Task<ActionResult> GetByPadron(int padron)
        {
            var estudiante = await service.GetFirstAsync(e => e.Padron == padron);

            if (estudiante == null)
                return NotFound();

            return Ok(estudiante);
        }

        [HttpGet("documento/{documento}")]
        public async Task<ActionResult> GetByDocumento(string documento)
        {
            var estudiante = await service.GetFirstAsync(e => e.Documento == documento);

            if (estudiante == null)
                return NotFound();

            return Ok(estudiante);
        }

        [HttpGet("buscar/{busqueda}")]
        public async Task<ActionResult> GetByTextSearch(string busqueda)
        {
            var estudiantes = await service.GetByTextSearch("\"" + busqueda + "\"");

            if (estudiantes.Count == 0)
                return NotFound();

            return Ok(estudiantes);
        }
    }
}