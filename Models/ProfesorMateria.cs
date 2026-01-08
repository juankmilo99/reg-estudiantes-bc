using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reg_estudiantes_bc.Models
{
    [Table("profesor_materia")]
    public class ProfesorMateria
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("profesor_id")]
        public int ProfesorId { get; set; }

        [Required]
        [Column("materia_id")]
        public int MateriaId { get; set; }

        [Column("periodo_id")]
        public int? PeriodoId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("ProfesorId")]
        public Profesor Profesor { get; set; } = null!;

        [ForeignKey("MateriaId")]
        public Materia Materia { get; set; } = null!;

        [ForeignKey("PeriodoId")]
        public Periodo? Periodo { get; set; }

        public ICollection<Inscripcion> Inscripciones { get; set; } = [];
    }
}
