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

using Game = SoccerTradingSystem.Model.Game;
using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;

namespace SoccerTradingSystem.Views
{
    /// <summary>
    /// game_list.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class game_list : Page
    {
        public game_list()
        {
            InitializeComponent();
        }

        // 페이지가 로드 되었을 때 리뉴
        private void OnPageLoad(object sender, RoutedEventArgs e)
        {
            //ClubsDataGridSetting("");
        }
    }
}
