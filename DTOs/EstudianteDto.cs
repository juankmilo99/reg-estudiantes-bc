using System.ComponentModel.DataAnnotations;

namespace reg_estudiantes_bc.DTOs
{
    public class EstudianteCreateDto
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "El email no es válido")]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
    }

    public class EstudianteUpdateDto
    {
        [MaxLength(100)]
        public string? Nombre { get; set; }

        [EmailAddress(ErrorMessage = "El email no es válido")]
        [MaxLength(100)]
        public string? Email { get; set; }
    }

    public class EstudianteDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
        public List<MateriaBasicDto> Materias { get; set; } = [];
    }

    public class EstudianteBasicDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
