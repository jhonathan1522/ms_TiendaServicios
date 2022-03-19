using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TiendaServicios.Api.Autor.Modelo;

namespace TiendaServicios.Api.Autor.Persistencia
{
    public class ContextoAutor: DbContext
    {
        public DbSet<AutorLibro> AutorLibro { get; set; }

        public DbSet<GradoAcademico> GradoAcademico { get; set; }

        public ContextoAutor(DbContextOptions<ContextoAutor> options)
        : base(options)
        {
        }

        public ContextoAutor()
        {
        }

        


    }
}
