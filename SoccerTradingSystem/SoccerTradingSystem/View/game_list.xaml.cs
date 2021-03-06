﻿using System;
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

using Game = SoccerTradingSystem.Model.Game;
using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;
using RetrieveHandler = SoccerTradingSystem.Controller.RetrieveHandler;
using GameDetailWindow = SoccerTradingSystem.View.GameDetailWindow;

namespace SoccerTradingSystem.Views
{
    /// <summary>
    /// game_list.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    using JSON = List<Dictionary<string, object>>;
    public partial class game_list : Page
    {
        public game_list()
        {
            InitializeComponent();
        }

        private void Game_Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView row = (DataRowView)gameDataGrid.SelectedItems[0];
            int gid = Convert.ToInt32((row[0]));

            // 윈도우 호출
            GameDetailWindow _GameDetailWindow = new GameDetailWindow(gid);
            _GameDetailWindow.Show();
        }

        // 페이지가 로드 되었을 때 리뉴
        private void OnPageLoad(object sender, RoutedEventArgs e)
        {
            GameDataGridSetting("");
        }

        public void GameDataGridSetting(string context)
        {
            RetrieveHandler rh = new RetrieveHandler();
            JSON filter = new JSON();
            filter.Add(new Dictionary<string, object>());
            List<Game> list = rh.retrieveGame(null);
            //List<Game> list = null;

            if (list == null)
                return;

            // DataTable 생성
            DataTable dataTable = new DataTable();

            // 컬럼 생성
            dataTable.Columns.Add("gid", typeof(string));
            dataTable.Columns.Add("homeId", typeof(string));
            dataTable.Columns.Add("awayId", typeof(string));
            dataTable.Columns.Add("homeScore", typeof(string));
            dataTable.Columns.Add("awayScore", typeof(string));
            dataTable.Columns.Add("winner", typeof(string));
            dataTable.Columns.Add("gameDate", typeof(string));
            dataTable.Columns.Add("gameTime", typeof(string));

            // 데이터 생성


            for (int i = 0; i < list.Count; i++)
            {
                string gid = Convert.ToString(list[i].gameId);
                string homeId = Convert.ToString(list[i].homeTeam.name);
                string awayId = Convert.ToString(list[i].awayTeam.name);
                string homeScore = Convert.ToString(list[i].homeScore);
                string awayScore = Convert.ToString(list[i].awayScore);
                string winner;
                if (list[i].homeScore > list[i].awayScore)
                {
                    winner = Convert.ToString(list[i].homeTeam.name);
                } else if (list[i].homeScore < list[i].awayScore)
                {
                    winner = Convert.ToString(list[i].awayTeam.name);
                } else
                {
                    winner = "무승부";
                }
                string _gameDate = list[i].date[0] + list[i].date[1] + list[i].date[2] + list[i].date[3] + "-" + list[i].date[4] + list[i].date[5] + "-" + list[i].date[6] + list[i].date[7] + " " + list[i].startTime[0] + list[i].startTime[1] + ":" + list[i].startTime[2] + list[i].startTime[3];
                string gameDate = _gameDate;
                string gameTime = Convert.ToString(list[i].playTime) + "분";
                dataTable.Rows.Add(new object[] { gid, homeId, awayId, homeScore, awayScore, winner, gameDate, gameTime });
            }

            gameDataGrid.ItemsSource = dataTable.DefaultView;
        }
    }
}
