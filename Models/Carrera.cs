using universidad_mvc.Models;

namespace universidad_mvc.Models
{
    public class Carrera
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Nivel { get; set; }
        public int FacultadId { get; set; }
        public Facultad Facultad { get; set; }
    }
}
