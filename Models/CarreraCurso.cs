namespace universidad_mvc.Models
{
    public class CarreraCurso
    {
        public int Id { get; set; }
        public int Semestre { get; set; }
        public int Creditos { get; set; }
        public int CarreraId { get; set; }
        public Carrera Carrera { get; set; }
        public int CursoId { get; set; }
        public Curso Curso { get; set; }
    }
}
