namespace universidad_mvc.Models
{
    public class Grupo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Semestre { get; set; }
        public bool Activo { get; set; }
        public int CarreraCursoId { get; set; }
        public int CantMaxEstudiantes { get; set; }
        public CarreraCurso CarreraCurso { get; set; }
        public int? ProfesorId { get; set; }
        public Profesor Profesor { get; set; }
    }
}
