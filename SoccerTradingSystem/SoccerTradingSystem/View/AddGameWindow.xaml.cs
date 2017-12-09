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
using System.Windows.Shapes;

using Game = SoccerTradingSystem.Model.Game;
using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;

namespace SoccerTradingSystem.Views
{
    /// <summary>
    /// AddGameWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AddGameWindow : Window
    {
        public AddGameWindow()
        {
            InitializeComponent();
        }
    }
}
