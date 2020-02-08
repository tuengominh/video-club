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
    public partial class ActualizarProductoPagina : Page
    {
        private ProductoService productoService = new ProductoService();
        private int productoID = 0;
        private Producto producto;
        private int cantidadAnadir = 0;

        public ActualizarProductoPagina()
        {
            InitializeComponent();
        }

        public ActualizarProductoPagina(int val) : this()
        {
            productoID = val;
            this.Loaded += new RoutedEventHandler(ActualizarProductoPagina_Loaded);

        }

        private void ActualizarProductoPagina_Loaded(object sender, RoutedEventArgs e)
        {
            if (productoID != 0)
            {
                id.Text = productoID.ToString();
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                producto = productoService.getProductoConID(Int32.Parse(id.Text));
                productoID = producto.productCodigo;
                mostrarInfo(producto);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnAnadirCopia_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cantidadAnadir = ValidationUtil.NumeroInteger(cantidad.Text);
                int newCantidad = Int32.Parse(noDeCopias.Text) + cantidadAnadir;
                noDeCopias.Text = newCantidad.ToString();
                cantidad.Text = "";
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
                producto = productoService.getProductoConID(productoID);

                if (producto.GetType().Equals(typeof(VideoJuegos)))
                {
                    VideoJuegos v = (VideoJuegos)producto;

                    v.titulo = ValidationUtil.NullOrEmpty(titulo.Text);
                    v.anadirCopias(cantidadAnadir);

                    if (btnPC.IsChecked == true)
                    {
                        v.plataforma = Plataforma.PC;
                    }
                    else if (btnXbox.IsChecked == true)
                    {
                        v.plataforma = Plataforma.Xbox;
                    }
                    else if (btnPS2.IsChecked == true)
                    {
                        v.plataforma = Plataforma.PS2;
                    }
                    else if (btnPS3.IsChecked == true)
                    {
                        v.plataforma = Plataforma.PS3;
                    }
                    else if (btnWii.IsChecked == true)
                    {
                        v.plataforma = Plataforma.Wii;
                    }
                    else
                    {
                        throw new Exception("Plataforma debe ser PC, Xbox, PS2, PS3 or Wii.");
                    }

                    productoService.actualizarVideoJuegos(v, v.productCodigo);
                }
                else
                {
                    Pelicula pe = (Pelicula)producto;

                    pe.titulo = ValidationUtil.NullOrEmpty(titulo.Text);
                    pe.anadirCopias(cantidadAnadir);

                    pe.duracion = ValidationUtil.NumeroInteger(ValidationUtil.NullOrEmpty(duracion.Text));
                    pe.anoProduccion = ValidationUtil.Ano(ValidationUtil.NumeroInteger(ValidationUtil.NullOrEmpty(anoProduccion.Text)));
                    pe.anoCompra = ValidationUtil.Ano(ValidationUtil.NumeroInteger(ValidationUtil.NullOrEmpty(anoCompra.Text)));

                    productoService.actualizarPelicula(pe, pe.productCodigo);
                }
                mostrarInfo(productoService.getProductoConID(productoID));
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void mostrarInfo(Producto p)
        {
            titulo.Text = p.titulo;
            noDeCopias.Text = p.noDeCopias.ToString();

            if (p.GetType().Equals(typeof(VideoJuegos)))
            {
                VideoJuegos v = (VideoJuegos)p;
                tipo.Text = v.tipo.ToString();
                duracion.IsEnabled = false;
                anoProduccion.IsEnabled = false;
                anoCompra.IsEnabled = false;
                btnPC.IsEnabled = true;
                btnXbox.IsEnabled = true;
                btnPS2.IsEnabled = true;
                btnPS3.IsEnabled = true;
                btnWii.IsEnabled = true;
                if (v.plataforma == Plataforma.PC)
                {
                    btnPC.IsChecked = true;
                } 
                else if (v.plataforma == Plataforma.Xbox)
                {
                    btnXbox.IsChecked = true;
                }
                else if (v.plataforma == Plataforma.PS2)
                {
                    btnPS2.IsChecked = true;
                }
                else if (v.plataforma == Plataforma.PS3)
                {
                    btnPS3.IsChecked = true;
                }
                else if (v.plataforma == Plataforma.Wii)
                {
                    btnWii.IsChecked = true;
                }
            }
            else
            {
                Pelicula pe = (Pelicula)p;
                tipo.Text = pe.tipo.ToString();
                btnPC.IsEnabled = false;
                btnXbox.IsEnabled = false;
                btnPS2.IsEnabled = false;
                btnPS3.IsEnabled = false;
                btnWii.IsEnabled = false;
                duracion.IsEnabled = true;
                anoProduccion.IsEnabled = true;
                anoCompra.IsEnabled = true;
                duracion.Text = pe.duracion.ToString();
                anoProduccion.Text = pe.anoProduccion.ToString();
                anoCompra.Text = pe.anoCompra.ToString();
            }
        }
    }
}
