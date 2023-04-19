using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using universidad_mvc.Models;

namespace universidad_mvc.Data
{
    public class universidad_mvcContext : DbContext
    {
        public universidad_mvcContext (DbContextOptions<universidad_mvcContext> options)
            : base(options)
        {
        }

        public DbSet<universidad_mvc.Models.Carrera> Carrera { get; set; } = default!;

        public DbSet<universidad_mvc.Models.CarreraCurso>? CarreraCurso { get; set; }

        public DbSet<universidad_mvc.Models.Curso>? Curso { get; set; }

        public DbSet<universidad_mvc.Models.Estudiante>? Estudiante { get; set; }

        public DbSet<universidad_mvc.Models.Facultad>? Facultad { get; set; }

        public DbSet<universidad_mvc.Models.Grupo>? Grupo { get; set; }

        public DbSet<universidad_mvc.Models.Profesor>? Profesor { get; set; }

        public DbSet<universidad_mvc.Models.Universidad>? Universidad { get; set; }

        public DbSet<universidad_mvc.Models.EstudianteGrupo>? EstudianteGrupo { get; set; }
    }
}
