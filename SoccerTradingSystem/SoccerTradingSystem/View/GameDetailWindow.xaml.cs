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

using Types = SoccerTradingSystem.Model.Types;
using Game = SoccerTradingSystem.Model.Game;
using Goal = SoccerTradingSystem.Model.Goal;
using Rating = SoccerTradingSystem.Model.Rating;
using Player = SoccerTradingSystem.Model.Player;
using RetrieveHandler = SoccerTradingSystem.Controller.RetrieveHandler;
using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;

namespace SoccerTradingSystem.View
{
    /// <summary>
    /// GameDetailWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    using JSON = List<Dictionary<string, object>>;
    public partial class GameDetailWindow : Window
    {
        int gid;
        public GameDetailWindow(int _gid)
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            gid = _gid;

            RetrieveHandler rh = new RetrieveHandler();
            JSON filter = new JSON();
            filter.Add(new Dictionary<string, object>());
            filter[0].Add("gameId", gid);
            List<Game> games = rh.retrieveGame(filter);

            //Goal DataTable 생성
            DataTable goal_dataTable = new DataTable();
            // 컬럼 생성
            goal_dataTable.Columns.Add("player", typeof(string));
            goal_dataTable.Columns.Add("asist", typeof(string));
            goal_dataTable.Columns.Add("time", typeof(string));

            for (int i = 0; i < games[0].goals.Count(); i++)
            {
                string player = games[0].goals[i].player.lastName + " " + games[0].goals[i].player.middleName + games[0].goals[i].player.firstName;
                string asist = games[0].goals[i].assistPlayers[0].lastName + " " + games[0].goals[i].assistPlayers[0].middleName + games[0].goals[i].assistPlayers[0].firstName;
                string time = games[0].goals[i].time;
                goal_dataTable.Rows.Add(new object[] { player, asist, time });
            }
            goalDataGrid.ItemsSource = goal_dataTable.DefaultView;

            //Rating DataTable 생성
            DataTable rating_dataTable = new DataTable();
            // 컬럼 생성
            rating_dataTable.Columns.Add("player", typeof(string));
            rating_dataTable.Columns.Add("rating", typeof(string));
            for (int i = 0; i < games[0].ratings.Count(); i++)
            {
                string player = games[0].ratings[i].player.lastName + " " + games[0].ratings[i].player.middleName + games[0].ratings[i].player.firstName;
                string rating = games[0].ratings[i].ratingGrade.ToString();
                rating_dataTable.Rows.Add(new object[] { player, rating });
            }
            ratingDataGrid.ItemsSource = rating_dataTable.DefaultView;
        }
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
