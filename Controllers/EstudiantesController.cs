using Microsoft.AspNetCore.Mvc;
using reg_estudiantes_bc.DTOs;
using reg_estudiantes_bc.Services;

namespace reg_estudiantes_bc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController : ControllerBase
    {
        private readonly IEstudianteService _estudianteService;

        public EstudiantesController(IEstudianteService estudianteService)
        {
            _estudianteService = estudianteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstudianteDto>>> GetAll()
        {
            var estudiantes = await _estudianteService.GetAllAsync();
            return Ok(estudiantes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EstudianteDto>> GetById(int id)
        {
            var estudiante = await _estudianteService.GetByIdAsync(id);
            if (estudiante == null)
                return NotFound(new { message = "Estudiante no encontrado" });

            return Ok(estudiante);
        }

        [HttpPost]
        public async Task<ActionResult<EstudianteDto>> Create([FromBody] EstudianteCreateDto dto)
        {
            try
            {
                var estudiante = await _estudianteService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = estudiante.Id }, estudiante);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EstudianteDto>> Update(int id, [FromBody] EstudianteUpdateDto dto)
        {
            try
            {
                var estudiante = await _estudianteService.UpdateAsync(id, dto);
                if (estudiante == null)
                    return NotFound(new { message = "Estudiante no encontrado" });

                return Ok(estudiante);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _estudianteService.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = "Estudiante no encontrado" });

            return NoContent();
        }

        [HttpGet("{id}/materias-companeros")]
        public async Task<ActionResult<IEnumerable<MateriaConCompanerosDto>>> GetMateriasConCompaneros(int id)
        {
            var materias = await _estudianteService.GetMateriasConCompanerosAsync(id);
            return Ok(materias);
        }
    }
}
