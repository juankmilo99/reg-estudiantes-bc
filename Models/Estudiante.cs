using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reg_estudiantes_bc.Models
{
    [Table("estudiante")]
    public class Estudiante
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Inscripcion> Inscripciones { get; set; } = [];
    }
}
