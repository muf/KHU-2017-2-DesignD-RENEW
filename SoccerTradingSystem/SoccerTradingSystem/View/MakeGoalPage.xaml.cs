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
using System.Data;

using MakeGamePage = SoccerTradingSystem.View.MakeGamePage;
using MakeRatingPage = SoccerTradingSystem.View.MakeRatingPage;
using MakeGoalPage = SoccerTradingSystem.View.MakeGoalPage;

namespace SoccerTradingSystem.View
{
    /// <summary>
    /// MakeGoalPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MakeGoalPage : Page
    {
        public MakeGoalPage()
        {
            InitializeComponent();
        }

        public struct goalinput
        {
            public string player { set; get; }
            public string asist { set; get; }
            public string time { set; get; }
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            string _player = playertextbox.Text;
            string _asists = asisttextbox.Text;
            string _time = goaltimetextbox.Text;
            goalDataGrid.Items.Add(new goalinput() { player = _player, asist = _asists, time = _time });
            playertextbox.Text = "";
            asisttextbox.Text = "";
            goaltimetextbox.Text = "";
        }
    }
}
