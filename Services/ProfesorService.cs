using Microsoft.EntityFrameworkCore;
using reg_estudiantes_bc.Data;
using reg_estudiantes_bc.DTOs;

namespace reg_estudiantes_bc.Services
{
    public class ProfesorService : IProfesorService
    {
        private readonly ApplicationDbContext _context;

        public ProfesorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProfesorDto>> GetAllAsync()
        {
            return await _context.Profesores
                .Include(p => p.ProfesoresMaterias)
                    .ThenInclude(pm => pm.Materia)
                .Select(p => new ProfesorDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Materias = p.ProfesoresMaterias.Select(pm => new MateriaBasicDto
                    {
                        Id = pm.Materia.Id,
                        Nombre = pm.Materia.Nombre,
                        Creditos = pm.Materia.Creditos
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<ProfesorDto?> GetByIdAsync(int id)
        {
            var profesor = await _context.Profesores
                .Include(p => p.ProfesoresMaterias)
                    .ThenInclude(pm => pm.Materia)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (profesor == null)
                return null;

            return new ProfesorDto
            {
                Id = profesor.Id,
                Nombre = profesor.Nombre,
                Materias = profesor.ProfesoresMaterias.Select(pm => new MateriaBasicDto
                {
                    Id = pm.Materia.Id,
                    Nombre = pm.Materia.Nombre,
                    Creditos = pm.Materia.Creditos
                }).ToList()
            };
        }
    }
}
