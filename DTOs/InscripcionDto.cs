using System.ComponentModel.DataAnnotations;

namespace reg_estudiantes_bc.DTOs
{
    public class InscripcionCreateDto
    {
        [Required(ErrorMessage = "El ID de estudiante es requerido")]
        public int EstudianteId { get; set; }

        [Required(ErrorMessage = "Debe proporcionar las materias a inscribir")]
        [MinLength(1, ErrorMessage = "Debe seleccionar al menos 1 materia")]
        [MaxLength(3, ErrorMessage = "Solo puede seleccionar máximo 3 materias")]
        public List<int> MateriasIds { get; set; } = [];
    }

    public class InscripcionDto
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public string NombreEstudiante { get; set; } = string.Empty;
        public int MateriaId { get; set; }
        public string NombreMateria { get; set; } = string.Empty;
    }
}
