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

namespace SoccerTradingSystem
{
    using MariaDBConnector = SoccerTradingSystem.Controller.DAC.MariaDBConnector;
    using JSON = List<Dictionary<string, object>>;

    public partial class MainWindow : Window
    {

        private MariaDBConnector con = new MariaDBConnector();
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
