namespace universidad_mvc.Models
{
    public class Facultad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int UniversidadId { get; set; }
        public Universidad Universidad { get; set; }
    }
}
