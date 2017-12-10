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
    /// ViewClubPlayer.xaml에 대한 상호 작용 논리
    /// </summary>
    using JSON = List<Dictionary<string, object>>;
    public partial class ViewClubPlayer : Window
    {
        int uid;
        public ViewClubPlayer(int _uid)
        {
            InitializeComponent();
            uid = _uid;
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);

            RetrieveHandler rh = new RetrieveHandler();
            JSON filter = new JSON();
            filter.Add(new Dictionary<string, object>());
            filter[0].Add("uid", uid);
            List<Player> players = rh.retrievePlayer(filter);

            //Goal DataTable 생성
            DataTable dataTable = new DataTable();
            // 컬럼 생성
            dataTable.Columns.Add("uid", typeof(string));
            dataTable.Columns.Add("pid", typeof(string));
            dataTable.Columns.Add("name", typeof(string));
            dataTable.Columns.Add("email", typeof(string));
            dataTable.Columns.Add("birth", typeof(string));
            dataTable.Columns.Add("position", typeof(string));
            dataTable.Columns.Add("weight", typeof(string));
            dataTable.Columns.Add("height", typeof(string));
            dataTable.Columns.Add("status", typeof(string));

            for (int i = 0; i < players.Count(); i++)
            {
                string uid = Convert.ToString(players[i].uid);
                string pid = Convert.ToString(players[i].playerId);
                string email = players[i].email;
                string name = players[i].firstName + players[i].middleName + players[i].lastName;
                string birth = Convert.ToString(players[i].birth);
                string postion = Convert.ToString(players[i].position);
                string weight = Convert.ToString(players[i].weight);
                string height = Convert.ToString(players[i].height);
                string status = players[i].status;
                dataTable.Rows.Add(new object[] { uid, pid, email, name, birth, postion, weight, height, status });
            }
            playerDataGrid.ItemsSource = dataTable.DefaultView;


        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
