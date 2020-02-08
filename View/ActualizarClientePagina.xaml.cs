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
using VideoClub.Helper;
using VideoClub.Model;
using VideoClub.Service;

namespace VideoClub.View
{
    public partial class ActualizarClientePagina : Page
    {
        private ClienteService clienteService = new ClienteService();
        private int clienteID;
        private Cliente cliente;

        public ActualizarClientePagina()
        {
            InitializeComponent();
        }

        public ActualizarClientePagina(int val) : this()
        {
            clienteID = val;
            this.Loaded += new RoutedEventHandler(ActualizarClientePagina_Loaded);

        }

        private void ActualizarClientePagina_Loaded(object sender, RoutedEventArgs e)
        {
            if (clienteID != 0)
            {
                id.Text = clienteID.ToString();
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cliente = clienteService.getClienteConID(Int32.Parse(id.Text));
                clienteID = cliente.clienteCodigo;
                mostrarInfo(cliente);
            } 
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cliente = clienteService.getClienteConID(clienteID);

                cliente.nombre = ValidationUtil.NullOrEmpty(nombre.Text);
                cliente.apellidos = ValidationUtil.NullOrEmpty(apellidos.Text);
                cliente.direccion = ValidationUtil.NullOrEmpty(direccion.Text);
                cliente.nif = ValidationUtil.NIF(nif.Text);
                cliente.telefono = ValidationUtil.Telefono(telefono.Text);
                cliente.email = ValidationUtil.Email(email.Text);

                clienteService.actualizarCliente(cliente, clienteID);
                mostrarInfo(clienteService.getClienteConID(clienteID));
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void mostrarInfo(Cliente c)
        {
            nombre.Text = c.nombre;
            apellidos.Text = c.apellidos;
            direccion.Text = c.direccion;
            nif.Text = c.nif;
            telefono.Text = c.telefono;
            email.Text = c.email;
            fechaDeAlta.Text = c.fechaDeAlta.ToString();
            estado.Text = c.estado.ToString();

            if (c.VIP == true)
            {
                vip.Text = "Yes";
            }
            else
            {
                vip.Text = "No";
            }
        }
    }
}
