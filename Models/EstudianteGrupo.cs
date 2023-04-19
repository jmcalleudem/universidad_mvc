namespace universidad_mvc.Models
{
    public class EstudianteGrupo
    {
        public int Id { get; set; }
        public int Nota { get; set; }
        public int EstudianteId { get; set; }
        public Estudiante Estudiante { get; set; }
        public int GrupoId { get; set; }
        public Grupo Grupo { get; set; }
    }
}
