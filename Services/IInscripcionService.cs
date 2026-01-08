using reg_estudiantes_bc.DTOs;

namespace reg_estudiantes_bc.Services
{
    public interface IInscripcionService
    {
        Task<IEnumerable<InscripcionDto>> InscribirMateriasAsync(InscripcionCreateDto dto);
        Task<bool> DesinscribirMateriaAsync(int estudianteId, int materiaId);
        Task<IEnumerable<InscripcionDto>> GetInscripcionesByEstudianteAsync(int estudianteId);
    }
}
