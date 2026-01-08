namespace reg_estudiantes_bc.DTOs
{
    public class MateriaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Creditos { get; set; }
        public ProfesorBasicDto? Profesor { get; set; }
    }

    public class MateriaBasicDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Creditos { get; set; }
    }

    public class MateriaConCompanerosDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Creditos { get; set; }
        public ProfesorBasicDto? Profesor { get; set; }
        public List<EstudianteBasicDto> Companeros { get; set; } = [];
    }
}
