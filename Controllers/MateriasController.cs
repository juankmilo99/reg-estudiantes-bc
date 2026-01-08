using Microsoft.AspNetCore.Mvc;
using reg_estudiantes_bc.DTOs;
using reg_estudiantes_bc.Services;

namespace reg_estudiantes_bc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MateriasController : ControllerBase
    {
        private readonly IMateriaService _materiaService;

        public MateriasController(IMateriaService materiaService)
        {
            _materiaService = materiaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MateriaDto>>> GetAll()
        {
            var materias = await _materiaService.GetAllAsync();
            return Ok(materias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MateriaDto>> GetById(int id)
        {
            var materia = await _materiaService.GetByIdAsync(id);
            if (materia == null)
                return NotFound(new { message = "Materia no encontrada" });

            return Ok(materia);
        }

        [HttpGet("disponibles/{estudianteId}")]
        public async Task<ActionResult<IEnumerable<MateriaDto>>> GetMateriasDisponibles(int estudianteId)
        {
            var materias = await _materiaService.GetMateriasDisponiblesAsync(estudianteId);
            return Ok(materias);
        }
    }
}
