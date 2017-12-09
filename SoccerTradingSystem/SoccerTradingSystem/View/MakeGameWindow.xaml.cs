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

using MakeGamePage = SoccerTradingSystem.View.MakeGamePage;
using MakeRatingPage = SoccerTradingSystem.View.MakeRatingPage;
using MakeGoalPage = SoccerTradingSystem.View.MakeGoalPage;

using RetrieveHandler = SoccerTradingSystem.Controller.RetrieveHandler;
using GameHandler = SoccerTradingSystem.Controller.GameHandler;
using Game = SoccerTradingSystem.Model.Game;
using Club = SoccerTradingSystem.Model.Club;
using Goal = SoccerTradingSystem.Model.Goal;
using Rating = SoccerTradingSystem.Model.Rating;

namespace SoccerTradingSystem.View
{
    /// <summary>
    /// MakeGameWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    using JSON = List<Dictionary<string, object>>;
    public partial class MakeGameWindow : Window
    {
        public MakeGamePage _MakeGamePage = new MakeGamePage();
        public MakeRatingPage _MakeRatingPage = new MakeRatingPage();
        public MakeGoalPage _MakeGoalPage = new MakeGoalPage();
        int curPage = 0;

        public MakeGameWindow()
        {
            InitializeComponent();
        }

        private void OnWindowLoad(object sender, RoutedEventArgs e)
        {
            content_frame.Navigate(_MakeGamePage);
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            // View Goal Page
            if(curPage == 0)
            {
                content_frame.Navigate(_MakeGoalPage);
                PrevBtn.Visibility = System.Windows.Visibility.Visible;
            }
            if(curPage == 1)
            {
                content_frame.Navigate(_MakeRatingPage);
                NextBtn.Visibility = System.Windows.Visibility.Hidden;
                RegBtn.Visibility = System.Windows.Visibility.Visible;
            }
            curPage++;
        }

        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            // View Goal Page
            if (curPage == 1)
            {
                content_frame.Navigate(_MakeGamePage);
                PrevBtn.Visibility = System.Windows.Visibility.Hidden;
            }
            if (curPage == 2)
            {
                content_frame.Navigate(_MakeGoalPage);
                NextBtn.Visibility = System.Windows.Visibility.Visible;
                RegBtn.Visibility = System.Windows.Visibility.Hidden;
            }
            curPage--;
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {


            // 등록하기
            GameHandler gh = new GameHandler();
            RetrieveHandler rh = new RetrieveHandler();

            JSON home_filter = new JSON();
            home_filter.Add(new Dictionary<string, object>());
            home_filter[0].Add("name", _MakeGamePage.homeTeamcomboBox.SelectedValue);
            List<Club> homeclubs = rh.retrieveClub(home_filter);

            JSON away_filter = new JSON();
            away_filter.Add(new Dictionary<string, object>());
            away_filter[0].Add("name", _MakeGamePage.awayTeamcomboBox.SelectedValue);
            List<Club> awayclubs = rh.retrieveClub(away_filter);

            int gameId = -1;
            String date = _MakeGamePage.dateBox.Text;
            String startTime = _MakeGamePage.startTimeBox.Text;
            String playTime = _MakeGamePage.timeBox.Text;
            int homeScore  = Convert.ToInt32(_MakeGamePage.homeTeamScoreBox.Text);
            int awayScore = Convert.ToInt32(_MakeGamePage.awayTeamScoreBox.Text);
            Club homeTeam = homeclubs[0];
            Club awayTeam = awayclubs[0];
            List<Goal> goals = new List<Goal> { };
            List<Rating> ratings = new List<Rating> { };

            Game _Game = new Game(gameId, date, startTime, playTime, homeScore, awayScore, homeTeam, awayTeam, goals, ratings);
            gh.registerGame(_Game);
        }
    }
}
