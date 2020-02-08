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
using VideoClub.Service;

namespace VideoClub
{
    public partial class MainWindow : Window
    {
        private DBHelper dbHelper = new DBHelper(SchemaUtil.DB_NAME);
        private ClienteService clienteService = new ClienteService();
        private AlquilerService alquilerService = new AlquilerService();

        public MainWindow()
        {
            dbHelper.crearSampleDatabase();
            clienteService.checkVIP();
            alquilerService.checkRegresoTarde();
            InitializeComponent();
        }
    }
}
