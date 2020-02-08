using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Helper;
using VideoClub.Model;
using VideoClub.Repository;

namespace VideoClub.Service
{
    public class AlquilerService
    {
        public const double PELICULAS_PRECIO = 2.00;
        public const double VIDEOJUEGOS_PRECIO = 4.00;
        public const int DIA_EXTRAVIADO_RECLAMADO = 7;

        private AlquilerRepo alquilerRepo;
        private ProductoRepo productoRepo;
        private ClienteRepo clienteRepo;

        public AlquilerService()
        {
            this.alquilerRepo = new AlquilerRepo();
            this.clienteRepo = new ClienteRepo();
            this.productoRepo = new ProductoRepo();
        }

        public void checkRegresoTarde()
        {
            foreach (Alquiler al in mostrarTodos())
            {
                TimeSpan dateSpan = DateTime.Now - al.fechaDeAlquiler;
                Cliente c = clienteRepo.getClienteConID(al.clienteCodigo);
                Producto p = productoRepo.getProductoConID(al.productoCodigo);

                if (al.estado == AlquilerEstado.Alquilado)
                {
                    if ((dateSpan.Days - al.tiempoDeAlquiler) >= 1 && (dateSpan.Days - al.tiempoDeAlquiler) < DIA_EXTRAVIADO_RECLAMADO)
                    {
                        c.estado = ClienteEstado.Bloqueado;
                        al.tiempoDeAlquiler += (dateSpan.Days - al.tiempoDeAlquiler);
                        al.totalPago = calcularPago(p, al);
                    }

                    else if ((dateSpan.Days - al.tiempoDeAlquiler) >= DIA_EXTRAVIADO_RECLAMADO)
                    {
                        c.estado = ClienteEstado.Bloqueado;
                        al.estado = AlquilerEstado.Extraviado;
                        p.informarExtraviado();
                        informarAProductoRepo(p, al.productoCodigo);
                    }
                }

                actualizarAlquiler(al, al.alquilerCodigo);
                clienteRepo.actualizarCliente(c, al.clienteCodigo); 
            }
        }

        public List<Alquiler> mostrarTodos()
        {
            return alquilerRepo.getTodos();
        }

        public int anadirAlquiler(int productoID, int clienteID, DateTime fecha, int tiempo)
        {
            Cliente c = clienteRepo.getClienteConID(clienteID);
            Producto p = productoRepo.getProductoConID(productoID);

            if (c.estado == ClienteEstado.Activo)
            {
                if (p.disponible > 0)
                {
                    Alquiler al = new Alquiler();

                    al.clienteCodigo = clienteID;
                    al.productoCodigo = productoID;
                    al.fechaDeAlquiler = fecha;
                    al.tiempoDeAlquiler = tiempo;
                    al.descuento = calcularDescuento(c);
                    al.totalPago = calcularPago(p, al);
                    al.estado = AlquilerEstado.Alquilado;

                    p.anadirAlquiler();
                    informarAProductoRepo(p, al.productoCodigo);

                    return alquilerRepo.anadirAlquiler(al);
                } 
                else
                {
                    throw new Exception("Este producto no esta disponible.");
                }
            } 
            else
            {
                throw new Exception("Este cliente esta bloqueado.");
            }
        }

        public void regresarAlquiler(int id)
        {
            Alquiler al = mostrarAlquilerConID(id);
            Cliente c = clienteRepo.getClienteConID(al.clienteCodigo);
            Producto p = productoRepo.getProductoConID(al.productoCodigo);
            TimeSpan dateSpan = DateTime.Now - al.fechaDeAlquiler;

            if (al.estado == AlquilerEstado.Extraviado)
            {
                p.regresarExtraviado();
            }
            else
            {
                p.regresarAlquiler();
            }

            if ((dateSpan.Days - al.tiempoDeAlquiler) >= 1)
            {
                al.tiempoDeAlquiler += (dateSpan.Days - al.tiempoDeAlquiler);
                al.totalPago = calcularPago(p, al);
            }

            al.estado = AlquilerEstado.Regresado;

            if (c.estado == ClienteEstado.Bloqueado)
            {
                c.estado = ClienteEstado.Activo;
            }

            actualizarAlquiler(al, al.alquilerCodigo);
            clienteRepo.actualizarCliente(c, al.clienteCodigo);
            informarAProductoRepo(p, al.productoCodigo);
        }

        private double calcularDescuento(Cliente c)
        {
            double result = 0.0;

            List<Alquiler> alquileresPasadaMes = new List<Alquiler>();

            foreach (Alquiler alquiler in getAlquileresConClienteID(c.clienteCodigo))
            {
                int mesDiff = Math.Abs((DateTime.Now.Year * 12 + DateTime.Now.Month ) - (alquiler.fechaDeAlquiler.Year * 12 + alquiler.fechaDeAlquiler.Month));
                if (mesDiff == 1) 
                {
                    alquileresPasadaMes.Add(alquiler);
                }
            }
            
            if (c.VIP == true)
            {
                if (alquileresPasadaMes.Count >= 5 && alquileresPasadaMes.Count < 15)
                {
                    result = 0.1;
                } 
                else if (alquileresPasadaMes.Count >= 15 && alquileresPasadaMes.Count < 20)
                {
                    result = 0.15;
                }
                else if (alquileresPasadaMes.Count >= 20 && alquileresPasadaMes.Count < 30)
                {
                    result = 0.25;
                }
                else if (alquileresPasadaMes.Count >= 30)
                {
                    result = 0.5;
                }
            }

            return result;
        }

        private double calcularPago(Producto p, Alquiler al)
        {
            if (p.GetType().Equals(typeof(Pelicula)))
            {
                return al.tiempoDeAlquiler * PELICULAS_PRECIO * (1 - al.descuento);
            }
            else
            {
                return al.tiempoDeAlquiler * VIDEOJUEGOS_PRECIO * (1 - al.descuento);
            }
        }

        public double calcularTotalIngresos(List<Alquiler> alquileres)
        {
            double result = 0.0;
            foreach (Alquiler al in alquileres)
            {
                if (al.estado != AlquilerEstado.Extraviado)
                {
                    result += al.totalPago;
                }
            }
            return result;
        }

        public List<Alquiler> mostrarAlquileresPeliculas()
        {
            List<Alquiler> result = new List<Alquiler>();

            foreach (Alquiler al in mostrarTodos())
            {
                Producto p = productoRepo.getProductoConID(al.productoCodigo);
                if (p.GetType().Equals(typeof(Pelicula)))
                {
                    result.Add(al);
                }
            }

            return result;
        }

        public List<Alquiler> mostrarAlquileresVideoJuegos()
        {
            List<Alquiler> result = new List<Alquiler>();

            foreach (Alquiler al in mostrarTodos())
            {
                Producto p = productoRepo.getProductoConID(al.productoCodigo);
                if (p.GetType().Equals(typeof(VideoJuegos)))
                {
                    result.Add(al);
                }
            }

            return result;
        }

        public Alquiler mostrarAlquilerConID(int id)
        {
            return alquilerRepo.getAlquilerConID(id);
        }

        public List<Alquiler> getAlquileresConProductoID(int id)
        {
            return alquilerRepo.getAlquileresConProductoID(id);
        }

        public List<Alquiler> getAlquileresConClienteID(int id)
        {
            return alquilerRepo.getAlquileresConClienteID(id);
        }

        public void actualizarAlquiler(Alquiler al, int id)
        {
            alquilerRepo.actualizarAlquiler(al, id);
        }

        private void informarAProductoRepo(Producto p, int id)
        {
            if (p.GetType().Equals(typeof(Pelicula)))
            {
                productoRepo.actualizarPelicula((Pelicula)p, id);
            }
            else
            {
                productoRepo.actualizarVideoJuegos((VideoJuegos)p, id);
            }
        }
    }
}
