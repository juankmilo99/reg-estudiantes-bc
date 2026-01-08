using Microsoft.AspNetCore.Mvc;
using reg_estudiantes_bc.DTOs;
using reg_estudiantes_bc.Services;

namespace reg_estudiantes_bc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeriodosController : ControllerBase
    {
        private readonly IPeriodoService _periodoService;

        public PeriodosController(IPeriodoService periodoService)
        {
            _periodoService = periodoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeriodoDto>>> GetAll()
        {
            var periodos = await _periodoService.GetAllAsync();
            return Ok(periodos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PeriodoDto>> GetById(int id)
        {
            var periodo = await _periodoService.GetByIdAsync(id);
            if (periodo == null)
                return NotFound(new { message = "Periodo no encontrado" });

            return Ok(periodo);
        }

        [HttpGet("activo")]
        public async Task<ActionResult<PeriodoDto>> GetActivo()
        {
            var periodo = await _periodoService.GetActivoAsync();
            if (periodo == null)
                return NotFound(new { message = "No hay periodo activo" });

            return Ok(periodo);
        }

        [HttpPost]
        public async Task<ActionResult<PeriodoDto>> Create([FromBody] PeriodoCreateDto dto)
        {
            var periodo = await _periodoService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = periodo.Id }, periodo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PeriodoDto>> Update(int id, [FromBody] PeriodoUpdateDto dto)
        {
            var periodo = await _periodoService.UpdateAsync(id, dto);
            if (periodo == null)
                return NotFound(new { message = "Periodo no encontrado" });

            return Ok(periodo);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _periodoService.DeleteAsync(id);
            if (!result)
                return NotFound(new { message = "Periodo no encontrado" });

            return NoContent();
        }

        [HttpPost("{id}/activar")]
        public async Task<ActionResult> Activar(int id)
        {
            var result = await _periodoService.ActivarPeriodoAsync(id);
            if (!result)
                return NotFound(new { message = "Periodo no encontrado" });

            return Ok(new { message = "Periodo activado exitosamente" });
        }
    }
}
