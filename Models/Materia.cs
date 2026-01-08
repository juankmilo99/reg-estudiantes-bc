using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reg_estudiantes_bc.Models
{
    [Table("materia")]
    public class Materia
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Column("creditos")]
        public int Creditos { get; set; } = 3;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Inscripcion> Inscripciones { get; set; } = [];
        public ICollection<ProfesorMateria> ProfesoresMaterias { get; set; } = [];
    }
}
