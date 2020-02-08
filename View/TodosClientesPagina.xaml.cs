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
    public partial class TodosClientesPagina : Page
    {
        private ClienteService clienteService = new ClienteService();

        public TodosClientesPagina()
        {
            InitializeComponent();
            try
            {
                List<Cliente> clientes = clienteService.mostrarTodos();
                ClientesGrid.ItemsSource = clientes;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            Cliente selectedCliente = ClientesGrid.SelectedItem as Cliente;
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
