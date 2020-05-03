using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cei.Api.Common.Models;
using Cei.Api.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cei.Api.Academica.Controllers
{
    [Route("api/academica/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly ICrudService<Estudiante> service;
        public EstudiantesController(ICrudService<Estudiante> estudianteService)
        {
            this.service = estudianteService;
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
    }
}