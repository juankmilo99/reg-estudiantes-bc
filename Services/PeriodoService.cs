using Microsoft.EntityFrameworkCore;
using reg_estudiantes_bc.Data;
using reg_estudiantes_bc.DTOs;
using reg_estudiantes_bc.Models;

namespace reg_estudiantes_bc.Services
{
    public class PeriodoService : IPeriodoService
    {
        private readonly ApplicationDbContext _context;

        public PeriodoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PeriodoDto>> GetAllAsync()
        {
            return await _context.Periodos
                .Select(p => new PeriodoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Activo = p.Activo,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .ToListAsync();
        }

        public async Task<PeriodoDto?> GetByIdAsync(int id)
        {
            var periodo = await _context.Periodos.FindAsync(id);
            if (periodo == null)
                return null;

            return new PeriodoDto
            {
                Id = periodo.Id,
                Nombre = periodo.Nombre,
                Activo = periodo.Activo,
                CreatedAt = periodo.CreatedAt,
                UpdatedAt = periodo.UpdatedAt
            };
        }

        public async Task<PeriodoDto?> GetActivoAsync()
        {
            var periodo = await _context.Periodos
                .FirstOrDefaultAsync(p => p.Activo);

            if (periodo == null)
                return null;

            return new PeriodoDto
            {
                Id = periodo.Id,
                Nombre = periodo.Nombre,
                Activo = periodo.Activo,
                CreatedAt = periodo.CreatedAt,
                UpdatedAt = periodo.UpdatedAt
            };
        }

        public async Task<PeriodoDto> CreateAsync(PeriodoCreateDto dto)
        {
            var periodo = new Periodo
            {
                Nombre = dto.Nombre,
                Activo = dto.Activo,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            if (dto.Activo)
            {
                await _context.Periodos
                    .Where(p => p.Activo)
                    .ForEachAsync(p => p.Activo = false);
            }

            _context.Periodos.Add(periodo);
            await _context.SaveChangesAsync();

            return new PeriodoDto
            {
                Id = periodo.Id,
                Nombre = periodo.Nombre,
                Activo = periodo.Activo,
                CreatedAt = periodo.CreatedAt,
                UpdatedAt = periodo.UpdatedAt
            };
        }

        public async Task<PeriodoDto?> UpdateAsync(int id, PeriodoUpdateDto dto)
        {
            var periodo = await _context.Periodos.FindAsync(id);
            if (periodo == null)
                return null;

            if (!string.IsNullOrWhiteSpace(dto.Nombre))
                periodo.Nombre = dto.Nombre;

            if (dto.Activo.HasValue && dto.Activo.Value && !periodo.Activo)
            {
                await _context.Periodos
                    .Where(p => p.Activo && p.Id != id)
                    .ForEachAsync(p => p.Activo = false);
                periodo.Activo = true;
            }
            else if (dto.Activo.HasValue)
            {
                periodo.Activo = dto.Activo.Value;
            }

            periodo.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var periodo = await _context.Periodos.FindAsync(id);
            if (periodo == null)
                return false;

            _context.Periodos.Remove(periodo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActivarPeriodoAsync(int id)
        {
            var periodo = await _context.Periodos.FindAsync(id);
            if (periodo == null)
                return false;

            await _context.Periodos
                .Where(p => p.Activo)
                .ForEachAsync(p => p.Activo = false);

            periodo.Activo = true;
            periodo.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
