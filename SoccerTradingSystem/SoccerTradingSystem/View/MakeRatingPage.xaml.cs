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
using RetrieveHandler = SoccerTradingSystem.Controller.RetrieveHandler;
using Club = SoccerTradingSystem.Model.Club;

namespace SoccerTradingSystem.View
{
    /// <summary>
    /// MageRatingPage.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    using JSON = List<Dictionary<string, object>>;
    public partial class MakeRatingPage : Page
    {
        int homeUid;
        int awayUid;
        public MakeRatingPage(int _homeUid, int _awayUid)
        {
            InitializeComponent();

            homeUid = _homeUid;
            awayUid = _awayUid;
            getcomboboxdata();
        }

        public struct ratinginput
        {
            public string player { set; get; }
            public string rating { set; get; }
        }

        public void getcomboboxdata()
        {
            RetrieveHandler rh = new RetrieveHandler();

            JSON home_filter = new JSON();
            home_filter.Add(new Dictionary<string, object>());
            home_filter[0].Add("uid", homeUid);
            List<Club> homeClubs = rh.retrieveClub(home_filter);

            JSON away_filter = new JSON();
            away_filter.Add(new Dictionary<string, object>());
            away_filter[0].Add("uid", awayUid);
            List<Club> awayClubs = rh.retrieveClub(away_filter);


            for (int i = 0; i < homeClubs[0].players.Count(); i++)
            {
                playercombobox.Items.Add(homeClubs[0].players[i].lastName + " " + homeClubs[0].players[i].middleName + homeClubs[0].players[i].firstName);
            }
            for (int i = 0; i < awayClubs[0].players.Count(); i++)
            {
                playercombobox.Items.Add(awayClubs[0].players[i].lastName + " " + awayClubs[0].players[i].middleName + awayClubs[0].players[i].firstName);
            }

        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ratingtextbox.Text == "" || playercombobox.SelectedItem.ToString() == "")
            {
                MessageBox.Show("입력 값 에러");
                return;
            }
            
            string _player = playercombobox.SelectedItem.ToString();
            string _rating = ratingtextbox.Text;
            goalDataGrid.Items.Add(new ratinginput() { player = _player, rating = _rating});
            ratingtextbox.Text = "";
        }
    }
}
