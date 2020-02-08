using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VideoClub.Model;
using VideoClub.Service;

namespace VideoClub.Views
{
    public partial class StatsPagina : Page
    {
        private AlquilerService alService = new AlquilerService();
        private List<Alquiler> alquileres = new List<Alquiler>();
        private List<Alquiler> peliculas = new List<Alquiler>();
        private List<Alquiler> juegos = new List<Alquiler>();

        public StatsPagina()
        {
            try
            {
                alquileres = alService.mostrarTodos();
                peliculas = alService.mostrarAlquileresPeliculas();
                juegos = alService.mostrarAlquileresVideoJuegos();

                InitializeComponent();
                LoadPieChartData();

                totalAlquileres.Text = alquileres.Count.ToString();
                totalAlquileresIngresos.Text = alService.calcularTotalIngresos(alquileres).ToString() + " EUR";
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void LoadPieChartData()
        {
            try
            {
                ((PieSeries)alChart.Series[0]).ItemsSource = new KeyValuePair<string, int>[]
           {
                new KeyValuePair<string, int>("Peliculas: " + peliculas.Count.ToString(), peliculas.Count),
                new KeyValuePair<string, int>("Video Juegos: " + juegos.Count.ToString(), juegos.Count)
           };

                double pe = alService.calcularTotalIngresos(peliculas);
                double v = alService.calcularTotalIngresos(juegos);
                ((PieSeries)ingreChart.Series[0]).ItemsSource = new KeyValuePair<string, double>[]
                {
                new KeyValuePair<string, double>("Peliculas: " + pe.ToString() + " EUR", pe),
                new KeyValuePair<string, double>("Video Juegos: " + v.ToString() + " EUR", v)
                };
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}
