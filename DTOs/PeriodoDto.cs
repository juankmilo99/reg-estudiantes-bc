using System.ComponentModel.DataAnnotations;

namespace reg_estudiantes_bc.DTOs
{
    public class PeriodoCreateDto
    {
        [Required(ErrorMessage = "El nombre del periodo es requerido")]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        public bool Activo { get; set; } = false;
    }

    public class PeriodoUpdateDto
    {
        [MaxLength(50)]
        public string? Nombre { get; set; }

        public bool? Activo { get; set; }
    }

    public class PeriodoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class PeriodoBasicDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }
}
