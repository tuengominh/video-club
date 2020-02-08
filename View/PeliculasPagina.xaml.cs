using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VideoClub.Model;
using VideoClub.Service;

namespace VideoClub.View
{
    public partial class PeliculasPagina : Page
    {
        private ProductoService productoService = new ProductoService();
        public PeliculasPagina()
        {
            InitializeComponent();
            try
            {
                List<Producto> productos = productoService.mostrarPeliculas();
                List<Pelicula> peliculas = new List<Pelicula>();

                foreach (Producto p in productos)
                {
                    peliculas.Add((Pelicula)p);
                }

                PeliculasGrid.ItemsSource = peliculas;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            Pelicula selectedPelicula = PeliculasGrid.SelectedItem as Pelicula;
            if(selectedPelicula != null)
            {
                ActualizarProductoPagina page = new ActualizarProductoPagina(selectedPelicula.productCodigo);
                this.NavigationService.Navigate(page);
            } 
            else
            {
                ActualizarProductoPagina page = new ActualizarProductoPagina(0);
                this.NavigationService.Navigate(page);
            }
        }

        private void btnAlquiler_Click(object sender, RoutedEventArgs e)
        {
            Pelicula selectedPelicula = PeliculasGrid.SelectedItem as Pelicula;
            if (selectedPelicula != null)
            {
                AnadirAlquilerPagina page = new AnadirAlquilerPagina(selectedPelicula.productCodigo);
                this.NavigationService.Navigate(page);
            }
            else 
            {
                AnadirAlquilerPagina page = new AnadirAlquilerPagina(0);
                this.NavigationService.Navigate(page);
            }
        }
    }
}
