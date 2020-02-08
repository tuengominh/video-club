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
    public partial class TodosAlquileresPagina : Page
    {
        private AlquilerService alquilerService = new AlquilerService();

        public TodosAlquileresPagina()
        {
            InitializeComponent();

            try
            {
                List<Alquiler> alquileres = alquilerService.mostrarTodos();
                AlquilerGrid.ItemsSource = alquileres;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            Alquiler selectedAlquiler = AlquilerGrid.SelectedItem as Alquiler;
            if (selectedAlquiler != null)
            {
                RegresarAlquilerPagina page = new RegresarAlquilerPagina(selectedAlquiler.alquilerCodigo);
                this.NavigationService.Navigate(page);
            }
            else
            {
                RegresarAlquilerPagina page = new RegresarAlquilerPagina(0);
                this.NavigationService.Navigate(page);
            }
        }
    }
}
