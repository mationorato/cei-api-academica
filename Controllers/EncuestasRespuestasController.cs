using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
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

        [HttpGet("encuesta/{estudainteId}")]
        public async Task<ActionResult> GetByEncuestaId(string estudainteId)
        {
            var encuesta = await this.service.GetAllAsync(r => r.EstudianteId == estudainteId);

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
            var respuestas = await this.service.GetAllAsync(
                r =>
                r.EncuestaId == encuestaId &&
                r.EstudianteId == estudainteId &&
                r.Detalles.ContainsKey("materia_codigo")
            );

            if (respuestas.Count == 0)
                return Ok(false);

            var tieneRespuestaMateria = respuestas.Any(
                r => r.Detalles["materia_codigo"] == materiaCodigo
            );

            return Ok(tieneRespuestaMateria);
        }

        [HttpPost]
        public async Task<ActionResult> Create(EncuestaRespuesta respuesta)
        {
            respuesta.Fecha = DateTime.Now;

            try
            {
                await this.service.CreateAsync(respuesta);
            }
            catch (MongoWriteException error)
            {
                return BadRequest(error.WriteError.Message);
            }

            return CreatedAtRoute("GetEncuestaRespuestaById", new { id = respuesta.Id }, respuesta);
        }
    }
}