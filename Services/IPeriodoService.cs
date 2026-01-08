using reg_estudiantes_bc.DTOs;

namespace reg_estudiantes_bc.Services
{
    public interface IPeriodoService
    {
        Task<IEnumerable<PeriodoDto>> GetAllAsync();
        Task<PeriodoDto?> GetByIdAsync(int id);
        Task<PeriodoDto?> GetActivoAsync();
        Task<PeriodoDto> CreateAsync(PeriodoCreateDto dto);
        Task<PeriodoDto?> UpdateAsync(int id, PeriodoUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ActivarPeriodoAsync(int id);
    }
}
