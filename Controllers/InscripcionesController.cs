using Microsoft.AspNetCore.Mvc;
using reg_estudiantes_bc.DTOs;
using reg_estudiantes_bc.Services;

namespace reg_estudiantes_bc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InscripcionesController : ControllerBase
    {
        private readonly IInscripcionService _inscripcionService;

        public InscripcionesController(IInscripcionService inscripcionService)
        {
            _inscripcionService = inscripcionService;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<InscripcionDto>>> InscribirMaterias([FromBody] InscripcionCreateDto dto)
        {
            try
            {
                var inscripciones = await _inscripcionService.InscribirMateriasAsync(dto);
                return Ok(inscripciones);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{estudianteId}/{materiaId}")]
        public async Task<ActionResult> DesinscribirMateria(int estudianteId, int materiaId)
        {
            var result = await _inscripcionService.DesinscribirMateriaAsync(estudianteId, materiaId);
            if (!result)
                return NotFound(new { message = "Inscripción no encontrada" });

            return NoContent();
        }

        [HttpGet("estudiante/{estudianteId}")]
        public async Task<ActionResult<IEnumerable<InscripcionDto>>> GetByEstudiante(int estudianteId)
        {
            var inscripciones = await _inscripcionService.GetInscripcionesByEstudianteAsync(estudianteId);
            return Ok(inscripciones);
        }
    }
}
