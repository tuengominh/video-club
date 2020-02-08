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
    public partial class AnadirClientePagina : Page
    {
        private ClienteService clienteService = new ClienteService();

        public AnadirClientePagina()
        {
            InitializeComponent();
        }

        private void btnAnadir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente();

                cliente.nombre = ValidationUtil.NullOrEmpty(nombre.Text);
                cliente.apellidos = ValidationUtil.NullOrEmpty(apellidos.Text);
                cliente.direccion = ValidationUtil.NullOrEmpty(direccion.Text);
                cliente.nif = ValidationUtil.NIF(nif.Text);
                cliente.telefono = ValidationUtil.Telefono(telefono.Text);
                cliente.email = ValidationUtil.Email(email.Text);

                string tempDate = ValidationUtil.NullOrEmpty(fechaDeAlta.SelectedDate.ToString());
                cliente.fechaDeAlta = DateTime.Parse(tempDate);

                cliente.estado = ClienteEstado.Activo;
                cliente.VIP = false;

                id.Text = clienteService.anadirCliente(cliente).ToString();
                mostrarInfo(clienteService.getClienteConID(Int32.Parse(id.Text)));
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
            fechaDeAlta.SelectedDate = c.fechaDeAlta;
        }
    }
}
