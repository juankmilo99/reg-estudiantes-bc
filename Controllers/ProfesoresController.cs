using Microsoft.AspNetCore.Mvc;
using reg_estudiantes_bc.DTOs;
using reg_estudiantes_bc.Services;

namespace reg_estudiantes_bc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesoresController : ControllerBase
    {
        private readonly IProfesorService _profesorService;

        public ProfesoresController(IProfesorService profesorService)
        {
            _profesorService = profesorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProfesorDto>>> GetAll()
        {
            var profesores = await _profesorService.GetAllAsync();
            return Ok(profesores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProfesorDto>> GetById(int id)
        {
            var profesor = await _profesorService.GetByIdAsync(id);
            if (profesor == null)
                return NotFound(new { message = "Profesor no encontrado" });

            return Ok(profesor);
        }
    }
}
