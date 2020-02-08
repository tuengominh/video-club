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
    public partial class InactivosClientesPagina : Page
    {
        private ClienteService clienteService = new ClienteService();

        public InactivosClientesPagina()
        {
            InitializeComponent();
            List<Cliente> clientes = clienteService.mostrarInactivos();
            InactivosGrid.ItemsSource = clientes;
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            Cliente selectedCliente = InactivosGrid.SelectedItem as Cliente;
            
            if (selectedCliente != null)
            {
                ActualizarClientePagina page = new ActualizarClientePagina(selectedCliente.clienteCodigo);
                this.NavigationService.Navigate(page);
            }
            else
            {
                ActualizarClientePagina page = new ActualizarClientePagina(0);
                this.NavigationService.Navigate(page);
            }
        }
    }
}
