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
using VideoClub.Repository;
using VideoClub.Service;

namespace VideoClub.View
{
    public partial class AnadirProductoPagina : Page
    {
        private ProductoService productoService = new ProductoService();
        public AnadirProductoPagina()
        {
            InitializeComponent();
            checkTipo();
        }

        private void btnAnadir_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnPelicula.IsSelected == true)
                {
                    Pelicula pe = new Pelicula();
                    pe.tipo = Tipo.Pelicula;

                    pe.titulo = ValidationUtil.NullOrEmpty(titulo.Text);
                    pe.noDeCopias = ValidationUtil.NumeroInteger(ValidationUtil.NullOrEmpty(noDeCopias.Text));

                    pe.duracion = ValidationUtil.NumeroInteger(ValidationUtil.NullOrEmpty(duracion.Text));
                    pe.anoProduccion = ValidationUtil.Ano(ValidationUtil.NumeroInteger(ValidationUtil.NullOrEmpty(anoProduccion.Text)));
                    pe.anoCompra = ValidationUtil.Ano(ValidationUtil.NumeroInteger(ValidationUtil.NullOrEmpty(anoCompra.Text)));

                    id.Text = productoService.anadirPelicula(pe).ToString();
                }
                else if (btnVideoJuegos.IsSelected == true)
                {
                    VideoJuegos v = new VideoJuegos();
                    v.tipo = Tipo.VideoJuegos;

                    v.titulo = ValidationUtil.NullOrEmpty(titulo.Text);
                    v.noDeCopias = ValidationUtil.NumeroInteger(ValidationUtil.NullOrEmpty(noDeCopias.Text));

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

                    id.Text = productoService.anadirVideoJuegos(v).ToString();
                }
                else
                {
                    throw new Exception("Eligir un tipo de el producto.");
                }
                mostrarInfo(productoService.getProductoConID(Int32.Parse(id.Text)));
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
                btnVideoJuegos.IsSelected = true;
                checkTipo();
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
                btnPelicula.IsSelected = true;
                checkTipo();
                duracion.Text = pe.duracion.ToString();
                anoProduccion.Text = pe.anoProduccion.ToString();
                anoCompra.Text = pe.anoCompra.ToString();
            }
        }

        private void checkTipo()
        {
            try
            {
                if (btnPelicula.IsSelected == true)
                {
                    btnPC.IsEnabled = false;
                    btnXbox.IsEnabled = false;
                    btnPS2.IsEnabled = false;
                    btnPS3.IsEnabled = false;
                    btnWii.IsEnabled = false;
                    duracion.IsEnabled = true;
                    anoProduccion.IsEnabled = true;
                    anoCompra.IsEnabled = true;
                }
                else if (btnVideoJuegos.IsSelected == true)
                {
                    duracion.IsEnabled = false;
                    anoProduccion.IsEnabled = false;
                    anoCompra.IsEnabled = false;
                    btnPC.IsEnabled = true;
                    btnXbox.IsEnabled = true;
                    btnPS2.IsEnabled = true;
                    btnPS3.IsEnabled = true;
                    btnWii.IsEnabled = true;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void dropdown_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (btnPelicula.IsSelected == true)
            {
                btnVideoJuegos.IsSelected = false;
            }
            else if (btnVideoJuegos.IsSelected == true)
            {
                btnPelicula.IsSelected = false;
            }
            checkTipo();
        }
    }
}
