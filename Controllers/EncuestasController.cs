using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cei.Api.Common.Models;
using Cei.Api.Common.Services;
using Cei.Api.Common.Auth;
using MongoDB.Driver;

namespace Cei.Api.Academica.Controllers
{
    [ApiKeyAuth]
    [ApiController]
    [Route("api-academica/[controller]")]
    public class EncuestasController : ControllerBase
    {
        private readonly ICrudService<Encuesta> service;

        public EncuestasController(ICrudService<Encuesta> service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var encuestas = await this.service.GetAllAsync(e => true);

            if (encuestas.Count == 0)
                return NotFound();

            return Ok(encuestas);
        }

        [HttpGet("{id:length(24)}", Name = "GetEncuestaById")]
        public async Task<ActionResult> GetById(string id)
        {
            var encuesta = await this.service.GetByIdAsync(id);

            if (encuesta == null)
                return NotFound();

            return Ok(encuesta);
        }

        [HttpGet("nombre/{nombre}")]
        public async Task<ActionResult> GetByNombre(string nombre)
        {
            var encuesta = await this.service.GetFirstAsync(
                e => e.Nombre == nombre
            );

            if (encuesta == null)
                return NotFound();

            return Ok(encuesta);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Encuesta encuesta)
        {
            await this.service.CreateAsync(encuesta);

            return CreatedAtRoute("GetEncuestaById", new { id = encuesta.Id }, encuesta);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult> Update(string id, Encuesta encuesta)
        {
            if (id != encuesta.Id)
                return BadRequest(
                    new { message = "El id de la ruta no se corresponde con el del modelo enviado" }
                );

            var result = await this.service.UpdateAsync(id, encuesta);
            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }
    }
}