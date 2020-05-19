using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cei.Api.Common.Models;
using Cei.Api.Common.Auth;
using Cei.Api.Academica.Services;

namespace Cei.Api.Academica.Controllers
{
    [ApiKeyAuth]
    [ApiController]
    [Route("api-academica/[controller]")]
    public class EncuestasRespuestasController : ControllerBase
    {
        private readonly ICrudEncuestaRespuestaService service;

        public EncuestasRespuestasController(ICrudEncuestaRespuestaService service)
        {
            this.service = service;
        }

        [HttpGet("{id:length(24)}", Name = "GetEncuestaRespuestaById")]
        public async Task<ActionResult> GetById(string id)
        {
            var encuesta = await this.service.GetByIdAsync(id);

            if (encuesta == null)
                return NotFound();

            return Ok(encuesta);
        }

        [HttpGet("estudiante/{estudainteId}")]
        public async Task<ActionResult> GetByEstudianteId(string estudainteId)
        {
            var encuesta = await this.service.GetAllAsync(r => r.EstudianteId == estudainteId);

            if (encuesta.Count == 0)
                return NotFound();

            return Ok(encuesta);
        }

        [HttpGet("encuesta/{encuestaId}")]
        public async Task<ActionResult> GetByEncuestaId(string encuestaId)
        {
            var encuesta = await this.service.GetAllAsync(r => r.EncuestaId == encuestaId);

            if (encuesta.Count == 0)
                return NotFound();

            return Ok(encuesta);
        }

        [HttpGet("tienerespuesta/{encuestaId:length(24)}/{estudainteId:length(24)}")]
        public async Task<ActionResult> HasRespuesta(string encuestaId, string estudainteId)
        {
            var hasRespuesta = await this.service.HasRespuestaAsync(encuestaId, estudainteId);

            return Ok(hasRespuesta);
        }

        [HttpGet("tienerespuesta/{encuestaId:length(24)}/{estudainteId:length(24)}/{materiaCodigo}")]
        public async Task<ActionResult> HasRespuestaMateriaAsync(
            string encuestaId, string estudainteId, string materiaCodigo)
        {
            var hasRespuesta = await this.service.HasRespuestaMateriaAsync(encuestaId, estudainteId, materiaCodigo);

            return Ok(hasRespuesta);
        }

        [HttpPost]
        public async Task<ActionResult> Create(EncuestaRespuesta respuesta)
        {
            var hasRespuesta = await this.service.HasRespuestaAsync(respuesta.EncuestaId, respuesta.EstudianteId);

            if (hasRespuesta)
                return BadRequest(new { message = "Ya respondiste la encuesta para esta materia" });

            respuesta.Fecha = DateTime.Now;

            await this.service.CreateAsync(respuesta);

            return CreatedAtRoute("GetEncuestaRespuestaById", new { id = respuesta.Id }, respuesta);
        }

        [HttpPost("conmateria")]
        public async Task<ActionResult> CreateWithMateria(EncuestaRespuesta respuesta)
        {
            if (!respuesta.Detalles.ContainsKey("materia_codigo"))
                return BadRequest(new { message = "La respuesta no contiene una materia" });

            var hasRespuesta = await this.service.HasRespuestaMateriaAsync(
                respuesta.EncuestaId, respuesta.EstudianteId, respuesta.Detalles["materia_codigo"]);

            if (hasRespuesta)
                return BadRequest(new { message = "Ya respondiste la encuesta para esta materia" });

            respuesta.Fecha = DateTime.Now;

            await this.service.CreateAsync(respuesta);

            return CreatedAtRoute("GetEncuestaRespuestaById", new { id = respuesta.Id }, respuesta);
        }
    }
}