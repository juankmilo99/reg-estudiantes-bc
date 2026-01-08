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

        [ForeignKey("ProfesorId")]
        public Profesor Profesor { get; set; } = null!;

        [ForeignKey("MateriaId")]
        public Materia Materia { get; set; } = null!;
    }
}
