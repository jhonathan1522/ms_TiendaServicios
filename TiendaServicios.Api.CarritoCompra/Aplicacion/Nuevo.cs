using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.Modelo;
using TiendaServicios.Api.CarritoCompra.Persistencia;

namespace TiendaServicios.Api.CarritoCompra.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest {

            public DateTime? FechaCreacionSesion { get; set; }
            public List<string> ProductoLista { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {

            public readonly CarritoContexto _contexto;

            public Manejador(CarritoContexto contexto) {
                _contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion
                {
                    FechaCreacion = request.FechaCreacionSesion
                };

                _contexto.CarritoSesion.Add(carritoSesion);
                var valor = await _contexto.SaveChangesAsync();
                if (valor == 0)
                {
                    throw new Exception("No se pudo guardar el carrito de compras");
                }
                // obtenemos el id de la sesión
                int Id = carritoSesion.CarritoSesionId;
                foreach (var obj in request.ProductoLista) {
                    var detalleSesion = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = Id,
                        ProductoSeleccionado = obj
                    };
                    _contexto.CarritoSesionDetalle.Add(detalleSesion);
                }
                 valor = await _contexto.SaveChangesAsync();

                if (valor > 0) { 
                    return Unit.Value;
                }
                throw new Exception("No se pudo insertar el detalle del carrito de compras");

            }
        }
    }
}
