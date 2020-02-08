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
    public partial class AnadirAlquilerPagina : Page
    {
        private AlquilerService alquilerService = new AlquilerService();
        private ClienteService clienteService = new ClienteService();
        private ProductoService productoService = new ProductoService();
        private int productoID;
        private int clienteID;

        public AnadirAlquilerPagina()
        {
            InitializeComponent();
        }

        public AnadirAlquilerPagina(int val) : this()
        {
            productoID = val;
            this.Loaded += new RoutedEventHandler(AnadirAlquilerPagina_Loaded);
        }

        private void AnadirAlquilerPagina_Loaded(object sender, RoutedEventArgs e)
        {
            if (productoID != 0)
            {
                productoIdOTitulo.Text = productoID.ToString();
            }
        }

        private void btnBuscarProducto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Producto producto = new Producto();

                if (Int32.TryParse(productoIdOTitulo.Text, out int n))
                {
                    producto = productoService.getProductoConID(n);
                }
                else
                {
                    producto = productoService.getProductoConTitulo(productoIdOTitulo.Text);
                }

                productoID = producto.productCodigo;
                productoIdOTitulo.Text = producto.titulo;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnBuscarCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente();

                if (Int32.TryParse(clienteIdONIF.Text, out int n))
                {
                    cliente = clienteService.getClienteConID(n);
                }
                else
                {
                    cliente = clienteService.getClienteConNIF(clienteIdONIF.Text);
                }

                clienteID = cliente.clienteCodigo;
                clienteIdONIF.Text = cliente.nombre + " " + cliente.apellidos;
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnAnadir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int tiempo = ValidationUtil.NumeroInteger(ValidationUtil.NullOrEmpty(tiempoDeAlquiler.Text));

                string tempDate = ValidationUtil.NullOrEmpty(fechaDeAlquiler.SelectedDate.ToString());
                DateTime fecha = DateTime.Parse(tempDate);

                alquilerId.Text = alquilerService.anadirAlquiler(productoID, clienteID, fecha, tiempo).ToString();
                mostrarInfo(alquilerService.mostrarAlquilerConID(Int32.Parse(alquilerId.Text)));
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void mostrarInfo(Alquiler al)
        {
            productoIdOTitulo.Text = al.productoCodigo.ToString();
            clienteIdONIF.Text = al.clienteCodigo.ToString();
            fechaDeAlquiler.SelectedDate = al.fechaDeAlquiler;
            tiempoDeAlquiler.Text = al.tiempoDeAlquiler.ToString();
            descuento.Text = al.descuento.ToString();
            pago.Text = al.totalPago.ToString();
        }
    }
}
