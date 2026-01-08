using reg_estudiantes_bc.DTOs;

namespace reg_estudiantes_bc.Services
{
    public interface IMateriaService
    {
        Task<IEnumerable<MateriaDto>> GetAllAsync();
        Task<MateriaDto?> GetByIdAsync(int id);
        Task<IEnumerable<MateriaDto>> GetMateriasDisponiblesAsync(int estudianteId);
    }
}
