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
    public partial class RegresarAlquilerPagina : Page
    {
        private AlquilerService alquilerService = new AlquilerService();
        private ProductoService productoService = new ProductoService();
        private ClienteService clienteService = new ClienteService();
        private int alquilerID;

        public RegresarAlquilerPagina()
        {
            InitializeComponent();
        }

        public RegresarAlquilerPagina(int val) : this()
        {
            alquilerID = val;
            this.Loaded += new RoutedEventHandler(RegresarAlquilerPagina_Loaded);

        }

        private void RegresarAlquilerPagina_Loaded(object sender, RoutedEventArgs e)
        {
            if (alquilerID != 0)
            {
                id.Text = alquilerID.ToString();
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Alquiler al = alquilerService.mostrarAlquilerConID(Int32.Parse(id.Text));
                alquilerID = al.alquilerCodigo;
                mostrarInfo(al);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                alquilerService.regresarAlquiler(alquilerID);
                mostrarInfo(alquilerService.mostrarAlquilerConID(alquilerID));
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void mostrarInfo(Alquiler al)
        {
            Cliente c = clienteService.getClienteConID(al.clienteCodigo);
            Producto p = productoService.getProductoConID(al.productoCodigo);
            
            productoId.Text = al.productoCodigo.ToString();
            titulo.Text = p.titulo;
            clienteId.Text = al.clienteCodigo.ToString();
            nombreYApellidos.Text = c.nombre + " " + c.apellidos;
            fechaDeAlquiler.Text = al.fechaDeAlquiler.ToString();
            tiempoDeAlquiler.Text = al.tiempoDeAlquiler.ToString();
            descuento.Text = al.descuento.ToString();
            pago.Text = al.totalPago.ToString();
            estado.Text = al.estado.ToString();
        }
    }
}
