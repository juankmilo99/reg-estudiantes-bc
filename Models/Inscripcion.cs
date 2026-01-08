using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace reg_estudiantes_bc.Models
{
    [Table("inscripcion")]
    public class Inscripcion
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("estudiante_id")]
        public int EstudianteId { get; set; }

        [Required]
        [Column("materia_id")]
        public int MateriaId { get; set; }

        [ForeignKey("EstudianteId")]
        public Estudiante Estudiante { get; set; } = null!;

        [ForeignKey("MateriaId")]
        public Materia Materia { get; set; } = null!;
    }
}
