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
using System.Data;

using MakeGamePage = SoccerTradingSystem.View.MakeGamePage;
using MakeRatingPage = SoccerTradingSystem.View.MakeRatingPage;
using MakeGoalPage = SoccerTradingSystem.View.MakeGoalPage;

using RetrieveHandler = SoccerTradingSystem.Controller.RetrieveHandler;
using GameHandler = SoccerTradingSystem.Controller.GameHandler;
using Player = SoccerTradingSystem.Model.Player;
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
        public MakeRatingPage _MakeRatingPage = null;
        public MakeGoalPage _MakeGoalPage = null;
        int curPage = 0;
        int curHome = 0;
        int curAway = 0;

        public MakeGameWindow()
        {
            InitializeComponent();
        }

        public struct goalinput
        {
            public string player { set; get; }
            public string asist { set; get; }
            public string time { set; get; }
        }

        private void OnWindowLoad(object sender, RoutedEventArgs e)
        {
            createGameInfoBtn_Click();
            NextBtn.Visibility = System.Windows.Visibility.Visible;
            PrevBtn.Visibility = System.Windows.Visibility.Collapsed;
            RegBtn.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void createGameInfoBtn_Click()
        {
            content_frame.Navigate(_MakeGamePage);
        }

        private void createRatingBtn_Click()
        {
            content_frame.Navigate(_MakeRatingPage);
        }

        private void createGoalBtn_Click()
        {
            content_frame.Navigate(_MakeGoalPage);
        }

        private void NextBtn_Click(object sender, RoutedEventArgs e)
        {
            if(curPage == 0)
            {
                // Validation Game Page
                if (_MakeGoalPage == null)
                {
                    RetrieveHandler rh = new RetrieveHandler();

                    JSON home_filter = new JSON();
                    home_filter.Add(new Dictionary<string, object>());
                    home_filter[0].Add("name", _MakeGamePage.homeTeamcomboBox.SelectedItem.ToString());

                    JSON away_filter = new JSON();
                    away_filter.Add(new Dictionary<string, object>());
                    away_filter[0].Add("name", _MakeGamePage.awayTeamcomboBox.SelectedItem.ToString());

                    List<Club> home_clubs = rh.retrieveClub(home_filter);
                    List<Club> away_clubs = rh.retrieveClub(away_filter);
                    //_MakeGoalPage = new MakeGoalPage();
                    curHome = home_clubs[0].uid;
                    curAway = away_clubs[0].uid;
                    _MakeGoalPage = new MakeGoalPage(home_clubs[0].uid, away_clubs[0].uid);
                }

                createGoalBtn_Click();
                PrevBtn.Visibility = System.Windows.Visibility.Visible;
            }
            if(curPage == 1)
            {
                // Validation Goal Page

                if(_MakeRatingPage == null)
                {
                    _MakeRatingPage = new MakeRatingPage(curHome, curAway);
                }

                createRatingBtn_Click();
                NextBtn.Visibility = System.Windows.Visibility.Collapsed;
                RegBtn.Visibility = System.Windows.Visibility.Visible;
            }
            curPage++;
        }

        private void PrevBtn_Click(object sender, RoutedEventArgs e)
        {
            if (curPage == 1)
            {
                // Validation Goal

                createGameInfoBtn_Click();
                PrevBtn.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (curPage == 2)
            {
                // Validation Rating

                createGoalBtn_Click();
                NextBtn.Visibility = System.Windows.Visibility.Visible;
                RegBtn.Visibility = System.Windows.Visibility.Collapsed;
            }
            curPage--;
        }

        private void registerGame(object sender, RoutedEventArgs e)
        {
            // Validation Rating

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
            String date = _MakeGamePage.datePicker.SelectedDate.Value.Date.ToShortDateString().Replace("-", "");
            String startTime = _MakeGamePage.startTimeBox.Text;
            String playTime = _MakeGamePage.timeBox.Text;
            int homeScore  = Convert.ToInt32(_MakeGamePage.homeTeamScoreBox.Text);
            int awayScore = Convert.ToInt32(_MakeGamePage.awayTeamScoreBox.Text);
            Club homeTeam = homeclubs[0];
            Club awayTeam = awayclubs[0];

            List<Goal> goals = new List<Goal> { };
            for(int i = 0; i< _MakeGoalPage.goalDataGrid.Items.Count; i++)
            {
                TextBlock _player = _MakeGoalPage.goalDataGrid.Columns[0].GetCellContent(_MakeGoalPage.goalDataGrid.Items[i]) as TextBlock;
                TextBlock _goal = _MakeGoalPage.goalDataGrid.Columns[1].GetCellContent(_MakeGoalPage.goalDataGrid.Items[i]) as TextBlock;
                TextBlock _time = _MakeGoalPage.goalDataGrid.Columns[2].GetCellContent(_MakeGoalPage.goalDataGrid.Items[i]) as TextBlock;
                String player = Convert.ToString(_player.Text);
                String goal = Convert.ToString(_goal.Text);
                String time = Convert.ToString(_time.Text);
                JSON playerfilter = new JSON();
                playerfilter.Add(new Dictionary<string, object>());
                playerfilter[0].Add("name", player);
                JSON asistfilter = new JSON();
                asistfilter.Add(new Dictionary<string, object>());
                asistfilter[0].Add("name", player);
                List<Player> _AssistPlayer = new List<Player> { };
                _AssistPlayer.Insert(0, rh.retrievePlayer(asistfilter)[0]);
                Goal _Goal = new Goal(-1, -1, rh.retrievePlayer(playerfilter)[0], _AssistPlayer, time);
                goals.Insert(i, _Goal);
            }

            List<Rating> ratings = new List<Rating> { };
            for (int i = 0; i < _MakeRatingPage.goalDataGrid.Items.Count; i++)
            {
                TextBlock _player = _MakeRatingPage.goalDataGrid.Columns[0].GetCellContent(_MakeRatingPage.goalDataGrid.Items[i]) as TextBlock;
                TextBlock _rating = _MakeRatingPage.goalDataGrid.Columns[1].GetCellContent(_MakeRatingPage.goalDataGrid.Items[i]) as TextBlock;
                String player = Convert.ToString(_player.Text);
                String rating = Convert.ToString(_rating.Text);
                JSON playerfilter = new JSON();
                playerfilter.Add(new Dictionary<string, object>());
                playerfilter[0].Add("name", player);

                Rating _Rating = new Rating(-1, -1, rh.retrievePlayer(playerfilter)[0], Convert.ToInt32(rating));
                ratings.Insert(i, _Rating);
            }

            Game _Game = new Game(gameId, date, startTime, playTime, homeScore, awayScore, homeTeam, awayTeam, goals, ratings);
            if (gh.registerGame(_Game))
            {
                MessageBox.Show("경기 정보를 성공적으로 추가 했습니다.");
                this.Close();
            }else
            {
                MessageBox.Show("경기 정보를 추가하는데 실패 했습니다.");
                this.Close();
            }
        }
    }
}
