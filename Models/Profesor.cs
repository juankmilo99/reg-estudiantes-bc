using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reg_estudiantes_bc.Models
{
    [Table("profesor")]
    public class Profesor
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        public ICollection<ProfesorMateria> ProfesoresMaterias { get; set; } = [];
    }
}
