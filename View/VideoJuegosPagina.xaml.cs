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
    public partial class VideoJuegosPagina : Page
    {
        private ProductoService productoService = new ProductoService();

        public VideoJuegosPagina()
        {
            InitializeComponent();

            try
            {
                List<Producto> productos = productoService.mostrarVideoJuegos();
                List<VideoJuegos> videoJuegos = new List<VideoJuegos>();

                foreach (Producto p in productos)
                {
                    videoJuegos.Add((VideoJuegos)p);
                }

                VideoJuegosGrid.ItemsSource = videoJuegos;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            VideoJuegos selectedVideoJuegos = VideoJuegosGrid.SelectedItem as VideoJuegos;

            if (selectedVideoJuegos != null)
            {
                ActualizarProductoPagina page = new ActualizarProductoPagina(selectedVideoJuegos.productCodigo);
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
            VideoJuegos selectedVideoJuegos = VideoJuegosGrid.SelectedItem as VideoJuegos;
            if (selectedVideoJuegos != null)
            {
                AnadirAlquilerPagina page = new AnadirAlquilerPagina(selectedVideoJuegos.productCodigo);
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
