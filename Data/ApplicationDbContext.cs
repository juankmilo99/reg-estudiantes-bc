using Microsoft.EntityFrameworkCore;
using reg_estudiantes_bc.Models;

namespace reg_estudiantes_bc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<ProfesorMateria> ProfesoresMaterias { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<ProfesorMateria>(entity =>
            {
                entity.HasIndex(e => e.MateriaId).IsUnique();
            });

            modelBuilder.Entity<Inscripcion>(entity =>
            {
                entity.HasIndex(e => new { e.EstudianteId, e.MateriaId }).IsUnique();
            });
        }
    }
}
