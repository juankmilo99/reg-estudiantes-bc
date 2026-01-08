using Microsoft.EntityFrameworkCore;
using reg_estudiantes_bc.Data;
using reg_estudiantes_bc.DTOs;

namespace reg_estudiantes_bc.Services
{
    public class MateriaService : IMateriaService
    {
        private readonly ApplicationDbContext _context;

        public MateriaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MateriaDto>> GetAllAsync()
        {
            return await _context.Materias
                .Include(m => m.ProfesoresMaterias)
                    .ThenInclude(pm => pm.Profesor)
                .Include(m => m.ProfesoresMaterias)
                    .ThenInclude(pm => pm.Periodo)
                .Select(m => new MateriaDto
                {
                    Id = m.Id,
                    Nombre = m.Nombre,
                    Creditos = m.Creditos,
                    Profesor = m.ProfesoresMaterias.Select(pm => new ProfesorBasicDto
                    {
                        Id = pm.Profesor.Id,
                        Nombre = pm.Profesor.Nombre
                    }).FirstOrDefault(),
                    Periodo = m.ProfesoresMaterias.Select(pm => pm.Periodo != null ? new PeriodoBasicDto
                    {
                        Id = pm.Periodo.Id,
                        Nombre = pm.Periodo.Nombre,
                        Activo = pm.Periodo.Activo
                    } : null).FirstOrDefault()
                })
                .ToListAsync();
        }

        public async Task<MateriaDto?> GetByIdAsync(int id)
        {
            var materia = await _context.Materias
                .Include(m => m.ProfesoresMaterias)
                    .ThenInclude(pm => pm.Profesor)
                .Include(m => m.ProfesoresMaterias)
                    .ThenInclude(pm => pm.Periodo)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (materia == null)
                return null;

            var profesorMateria = materia.ProfesoresMaterias.FirstOrDefault();

            return new MateriaDto
            {
                Id = materia.Id,
                Nombre = materia.Nombre,
                Creditos = materia.Creditos,
                Profesor = profesorMateria != null ? new ProfesorBasicDto
                {
                    Id = profesorMateria.Profesor.Id,
                    Nombre = profesorMateria.Profesor.Nombre
                } : null,
                Periodo = profesorMateria?.Periodo != null ? new PeriodoBasicDto
                {
                    Id = profesorMateria.Periodo.Id,
                    Nombre = profesorMateria.Periodo.Nombre,
                    Activo = profesorMateria.Periodo.Activo
                } : null
            };
        }

        public async Task<IEnumerable<MateriaDto>> GetMateriasDisponiblesAsync(int estudianteId)
        {
            var materiasInscritas = await _context.Inscripciones
                .Where(i => i.EstudianteId == estudianteId)
                .Select(i => i.MateriaId)
                .ToListAsync();

            var profesoresDelEstudiante = await _context.Inscripciones
                .Where(i => i.EstudianteId == estudianteId)
                .Include(i => i.Materia)
                    .ThenInclude(m => m.ProfesoresMaterias)
                .SelectMany(i => i.Materia.ProfesoresMaterias.Select(pm => pm.ProfesorId))
                .ToListAsync();

            return await _context.Materias
                .Include(m => m.ProfesoresMaterias)
                    .ThenInclude(pm => pm.Profesor)
                .Include(m => m.ProfesoresMaterias)
                    .ThenInclude(pm => pm.Periodo)
                .Where(m => !materiasInscritas.Contains(m.Id) &&
                           !m.ProfesoresMaterias.Any(pm => profesoresDelEstudiante.Contains(pm.ProfesorId)))
                .Select(m => new MateriaDto
                {
                    Id = m.Id,
                    Nombre = m.Nombre,
                    Creditos = m.Creditos,
                    Profesor = m.ProfesoresMaterias.Select(pm => new ProfesorBasicDto
                    {
                        Id = pm.Profesor.Id,
                        Nombre = pm.Profesor.Nombre
                    }).FirstOrDefault(),
                    Periodo = m.ProfesoresMaterias.Select(pm => pm.Periodo != null ? new PeriodoBasicDto
                    {
                        Id = pm.Periodo.Id,
                        Nombre = pm.Periodo.Nombre,
                        Activo = pm.Periodo.Activo
                    } : null).FirstOrDefault()
                })
                .ToListAsync();
        }
    }
}
