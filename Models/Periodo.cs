using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reg_estudiantes_bc.Models
{
    [Table("periodo")]
    public class Periodo
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("activo")]
        public bool Activo { get; set; } = false;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ProfesorMateria> ProfesoresMaterias { get; set; } = [];
    }
}
