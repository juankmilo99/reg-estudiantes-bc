using reg_estudiantes_bc.DTOs;

namespace reg_estudiantes_bc.Services
{
    public interface IProfesorService
    {
        Task<IEnumerable<ProfesorDto>> GetAllAsync();
        Task<ProfesorDto?> GetByIdAsync(int id);
    }
}
