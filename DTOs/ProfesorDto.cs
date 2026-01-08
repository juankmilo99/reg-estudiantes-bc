namespace reg_estudiantes_bc.DTOs
{
    public class ProfesorDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public List<MateriaBasicDto> Materias { get; set; } = [];
    }

    public class ProfesorBasicDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}
