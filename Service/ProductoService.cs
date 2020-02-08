using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Model;
using VideoClub.Repository;

namespace VideoClub.Service
{
    public class ProductoService
    {
        private ProductoRepo productoRepo;

        public ProductoService()
        {
            this.productoRepo = new ProductoRepo();
        }

        public List<Producto> mostrarTodos()
        {
            return productoRepo.getTodos();
        }

        public List<Producto> mostrarPeliculas()
        {
            return productoRepo.getPeliculas();
        }

        public List<Producto> mostrarVideoJuegos()
        {
            return productoRepo.getVideoJuegos();
        }

        public Producto getProductoConID(int id)
        {
            return productoRepo.getProductoConID(id);
        }

        public Producto getProductoConTitulo(string titulo)
        {
            return productoRepo.getProductoConTitulo(titulo);
        }

        public int anadirPelicula(Pelicula p)
        {
            return productoRepo.anadirPelicula(p);
        }

        public int anadirVideoJuegos(VideoJuegos p)
        {
            return productoRepo.anadirVideoJuegos(p);
        }

        public void actualizarPelicula(Pelicula p, int id)
        {
            productoRepo.actualizarPelicula(p, id);
        }

        public void actualizarVideoJuegos(VideoJuegos p, int id)
        {
            productoRepo.actualizarVideoJuegos(p, id);
        }
    }
}
