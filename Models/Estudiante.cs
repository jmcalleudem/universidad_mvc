namespace universidad_mvc.Models
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Semestre { get; set; }
        public int CarreraId { get; set; }
        public Carrera Carrera { get; set; }
    }
}
