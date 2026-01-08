using reg_estudiantes_bc.DTOs;

namespace reg_estudiantes_bc.Services
{
    public interface IEstudianteService
    {
        Task<IEnumerable<EstudianteDto>> GetAllAsync();
        Task<EstudianteDto?> GetByIdAsync(int id);
        Task<EstudianteDto> CreateAsync(EstudianteCreateDto dto);
        Task<EstudianteDto?> UpdateAsync(int id, EstudianteUpdateDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<MateriaConCompanerosDto>> GetMateriasConCompanerosAsync(int estudianteId);
    }
}
