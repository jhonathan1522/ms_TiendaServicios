using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaServicios.Api.Libro.Aplicacion;
using TiendaServicios.Api.Libro.Modelo;
using TiendaServicios.Api.Libro.Persistencia;
using Xunit;

namespace TiendaServicios.Api.Libro.Test
{
    public class LibrosServicesTest
    {

        private IEnumerable<LibreriaMaterial> ObtenerDataPrueba()
        {
            A.Configure<LibreriaMaterial>()
                .Fill(x => x.Titulo).AsArticleTitle()
                .Fill(x => x.LibreriaMaterialId, () => { return Guid.NewGuid(); });

            // metodo que se encarga de la ejecución de la data y devolverme la lista de elementos
            var lista = A.ListOf<LibreriaMaterial>(30);
            lista[0].LibreriaMaterialId = Guid.Empty;
            return lista;
        }

        private Mock<ContextoLibreria> CrearContexto() 
        {
            var dataPrueba = ObtenerDataPrueba().AsQueryable();
            var dbSet = new Mock<DbSet<LibreriaMaterial>>();

            // propiedades que debe tener toda clase de EF, al no estar trabajando con ella tengo que hacer manualmente el trabajo
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Provider).Returns(dataPrueba.Provider);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.Expression).Returns(dataPrueba.Expression);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.ElementType).Returns(dataPrueba.ElementType);
            dbSet.As<IQueryable<LibreriaMaterial>>().Setup(x => x.GetEnumerator()).Returns(dataPrueba.GetEnumerator);

;        }

        [Fact]
        public void GetLibros() {
            var mockContexto = new Mock<ContextoLibreria>();
            var mockMapper = new Mock<IMapper>();

            Consulta.Manejador Manejador = new Consulta.Manejador(mockContexto.Object, mockMapper.Object);
        }
    }
}
