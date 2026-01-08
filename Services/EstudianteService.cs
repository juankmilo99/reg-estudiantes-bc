using Microsoft.EntityFrameworkCore;
using reg_estudiantes_bc.Data;
using reg_estudiantes_bc.DTOs;
using reg_estudiantes_bc.Models;

namespace reg_estudiantes_bc.Services
{
    public class EstudianteService : IEstudianteService
    {
        private readonly ApplicationDbContext _context;

        public EstudianteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EstudianteDto>> GetAllAsync()
        {
            return await _context.Estudiantes
                .Include(e => e.Inscripciones)
                    .ThenInclude(i => i.Materia)
                .Select(e => new EstudianteDto
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Email = e.Email,
                    CreatedAt = e.CreatedAt,
                    UpdatedAt = e.UpdatedAt,
                    Materias = e.Inscripciones.Select(i => new MateriaBasicDto
                    {
                        Id = i.Materia.Id,
                        Nombre = i.Materia.Nombre,
                        Creditos = i.Materia.Creditos
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<EstudianteDto?> GetByIdAsync(int id)
        {
            var estudiante = await _context.Estudiantes
                .Include(e => e.Inscripciones)
                    .ThenInclude(i => i.Materia)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (estudiante == null)
                return null;

            return new EstudianteDto
            {
                Id = estudiante.Id,
                Nombre = estudiante.Nombre,
                Email = estudiante.Email,
                CreatedAt = estudiante.CreatedAt,
                UpdatedAt = estudiante.UpdatedAt,
                Materias = estudiante.Inscripciones.Select(i => new MateriaBasicDto
                {
                    Id = i.Materia.Id,
                    Nombre = i.Materia.Nombre,
                    Creditos = i.Materia.Creditos
                }).ToList()
            };
        }

        public async Task<EstudianteDto> CreateAsync(EstudianteCreateDto dto)
        {
            var emailExists = await _context.Estudiantes.AnyAsync(e => e.Email == dto.Email);
            if (emailExists)
                throw new InvalidOperationException("El email ya está registrado");

            var estudiante = new Estudiante
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Estudiantes.Add(estudiante);
            await _context.SaveChangesAsync();

            return new EstudianteDto
            {
                Id = estudiante.Id,
                Nombre = estudiante.Nombre,
                Email = estudiante.Email,
                CreatedAt = estudiante.CreatedAt,
                UpdatedAt = estudiante.UpdatedAt,
                Materias = []
            };
        }

        public async Task<EstudianteDto?> UpdateAsync(int id, EstudianteUpdateDto dto)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
                return null;

            if (!string.IsNullOrWhiteSpace(dto.Nombre))
                estudiante.Nombre = dto.Nombre;

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var emailExists = await _context.Estudiantes
                    .AnyAsync(e => e.Email == dto.Email && e.Id != id);
                if (emailExists)
                    throw new InvalidOperationException("El email ya está registrado");

                estudiante.Email = dto.Email;
            }

            estudiante.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
                return false;

            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<MateriaConCompanerosDto>> GetMateriasConCompanerosAsync(int estudianteId)
        {
            var materias = await _context.Inscripciones
                .Where(i => i.EstudianteId == estudianteId)
                .Include(i => i.Materia)
                    .ThenInclude(m => m.ProfesoresMaterias)
                        .ThenInclude(pm => pm.Profesor)
                .Include(i => i.Materia)
                    .ThenInclude(m => m.ProfesoresMaterias)
                        .ThenInclude(pm => pm.Periodo)
                .Select(i => i.Materia)
                .ToListAsync();

            var result = new List<MateriaConCompanerosDto>();

            foreach (var materia in materias)
            {
                var companeros = await _context.Inscripciones
                    .Where(i => i.MateriaId == materia.Id && i.EstudianteId != estudianteId)
                    .Include(i => i.Estudiante)
                    .Select(i => new EstudianteBasicDto
                    {
                        Id = i.Estudiante.Id,
                        Nombre = i.Estudiante.Nombre
                    })
                    .ToListAsync();

                var profesorMateria = materia.ProfesoresMaterias.FirstOrDefault();

                result.Add(new MateriaConCompanerosDto
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
                    } : null,
                    Companeros = companeros
                });
            }

            return result;
        }
    }
}
