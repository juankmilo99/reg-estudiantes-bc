using Microsoft.EntityFrameworkCore;
using reg_estudiantes_bc.Data;
using reg_estudiantes_bc.DTOs;
using reg_estudiantes_bc.Models;

namespace reg_estudiantes_bc.Services
{
    public class InscripcionService : IInscripcionService
    {
        private readonly ApplicationDbContext _context;

        public InscripcionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InscripcionDto>> InscribirMateriasAsync(InscripcionCreateDto dto)
        {
            var estudiante = await _context.Estudiantes.FindAsync(dto.EstudianteId);
            if (estudiante == null)
                throw new InvalidOperationException("El estudiante no existe");

            if (dto.MateriasIds.Count > 3)
                throw new InvalidOperationException("Solo puede inscribir máximo 3 materias");

            var inscripcionesActuales = await _context.Inscripciones
                .Where(i => i.EstudianteId == dto.EstudianteId)
                .CountAsync();

            if (inscripcionesActuales + dto.MateriasIds.Count > 3)
                throw new InvalidOperationException("El estudiante ya tiene materias inscritas. Solo puede tener máximo 3 materias en total");

            var periodoActivo = await _context.Periodos
                .FirstOrDefaultAsync(p => p.Activo);

            if (periodoActivo == null)
                throw new InvalidOperationException("No hay un período activo para inscribirse");

            var profesoresActuales = await _context.Inscripciones
                .Where(i => i.EstudianteId == dto.EstudianteId)
                .Include(i => i.Materia)
                    .ThenInclude(m => m.ProfesoresMaterias.Where(pm => pm.PeriodoId == periodoActivo.Id))
                .SelectMany(i => i.Materia.ProfesoresMaterias
                    .Where(pm => pm.PeriodoId == periodoActivo.Id)
                    .Select(pm => pm.ProfesorId))
                .ToListAsync();

            var materiasAInscribir = await _context.Materias
                .Include(m => m.ProfesoresMaterias.Where(pm => pm.PeriodoId == periodoActivo.Id))
                .Where(m => dto.MateriasIds.Contains(m.Id))
                .ToListAsync();

            if (materiasAInscribir.Count != dto.MateriasIds.Count)
                throw new InvalidOperationException("Una o más materias no existen");

            var profesoresNuevos = materiasAInscribir
                .SelectMany(m => m.ProfesoresMaterias
                    .Where(pm => pm.PeriodoId == periodoActivo.Id)
                    .Select(pm => pm.ProfesorId))
                .ToList();

            if (profesoresActuales.Any(p => profesoresNuevos.Contains(p)))
                throw new InvalidOperationException("No puede inscribir materias con profesores que ya tiene asignados");

            var profesoresUnicos = profesoresNuevos.Distinct().ToList();
            if (profesoresUnicos.Count != profesoresNuevos.Count)
                throw new InvalidOperationException("No puede inscribir dos materias con el mismo profesor");

            var inscripcionesCreadas = new List<Inscripcion>();

            foreach (var materiaId in dto.MateriasIds)
            {
                var inscripcionExiste = await _context.Inscripciones
                    .AnyAsync(i => i.EstudianteId == dto.EstudianteId && i.MateriaId == materiaId);

                if (inscripcionExiste)
                    throw new InvalidOperationException($"Ya está inscrito en la materia con ID {materiaId}");

                var profesorMateriaId = await _context.ProfesoresMaterias
                    .Where(pm => pm.MateriaId == materiaId && pm.PeriodoId == periodoActivo.Id)
                    .Select(pm => pm.Id)
                    .FirstOrDefaultAsync();

                var inscripcion = new Inscripcion
                {
                    EstudianteId = dto.EstudianteId,
                    MateriaId = materiaId,
                    ProfesorMateriaId = profesorMateriaId != 0 ? profesorMateriaId : null
                };

                _context.Inscripciones.Add(inscripcion);
                inscripcionesCreadas.Add(inscripcion);
            }

            await _context.SaveChangesAsync();

            return await GetInscripcionesByEstudianteAsync(dto.EstudianteId);
        }

        public async Task<bool> DesinscribirMateriaAsync(int estudianteId, int materiaId)
        {
            var inscripcion = await _context.Inscripciones
                .FirstOrDefaultAsync(i => i.EstudianteId == estudianteId && i.MateriaId == materiaId);

            if (inscripcion == null)
                return false;

            _context.Inscripciones.Remove(inscripcion);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<InscripcionDto>> GetInscripcionesByEstudianteAsync(int estudianteId)
        {
            return await _context.Inscripciones
                .Where(i => i.EstudianteId == estudianteId)
                .Include(i => i.Estudiante)
                .Include(i => i.Materia)
                .Select(i => new InscripcionDto
                {
                    Id = i.Id,
                    EstudianteId = i.EstudianteId,
                    NombreEstudiante = i.Estudiante.Nombre,
                    MateriaId = i.MateriaId,
                    NombreMateria = i.Materia.Nombre
                })
                .ToListAsync();
        }
    }
}
