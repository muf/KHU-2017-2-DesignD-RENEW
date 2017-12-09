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

using MakeGameWindow = SoccerTradingSystem.View.MakeGameWindow;
using MakeGamePage = SoccerTradingSystem.View.MakeGamePage;
using MakeRatingPage = SoccerTradingSystem.View.MakeRatingPage;
using MakeGoalPage = SoccerTradingSystem.View.MakeGoalPage;

using Club = SoccerTradingSystem.Model.Club;
using RetrieveHandler = SoccerTradingSystem.Controller.RetrieveHandler;

namespace SoccerTradingSystem.View
{
    /// <summary>
    /// MageGamePage.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    using JSON = List<Dictionary<string, object>>;
    public partial class MakeGamePage : Page
    {
        public MakeGamePage()
        {
            InitializeComponent();
            getComboListData();
        }

        private void getComboListData()
        {
            RetrieveHandler rh = new RetrieveHandler();
            JSON filter = new JSON();
            List<Club> clubs = rh.retrieveClub(null);
            for(int i = 0; i< clubs.Count; i++)
            {
                homeTeamcomboBox.Items.Add(clubs[i].name);
                awayTeamcomboBox.Items.Add(clubs[i].name);
            }
        }
    }
}
