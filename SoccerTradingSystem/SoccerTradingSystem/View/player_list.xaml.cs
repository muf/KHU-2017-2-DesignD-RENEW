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

using Player = SoccerTradingSystem.Model.Player;
using RetrieveHandler = SoccerTradingSystem.Controller.RetrieveHandler;
using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;

namespace SoccerTradingSystem.Views
{
    /// <summary>
    /// player_list.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    using JSON = List<Dictionary<string, object>>;
    public partial class player_list : Page
    {
        public player_list()
        {
            InitializeComponent();
        }

        private void OnPageLoad(object sender, RoutedEventArgs e)
        {
            PlayersDataGridSetting("");
        }

        // 플레이어 그리드에서 더블 클릭시 선수 정보 창을 생성
        private void Player_Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView row = (DataRowView)playerDataGrid.SelectedItems[0];
            int uid = Convert.ToInt32((row[0]));

            // 윈도우 호출
            PlayerDetailWindow _PlyaerDetailWindow = new PlayerDetailWindow(uid);
            _PlyaerDetailWindow.Show();
        }

        // 플레이어 그리드 구성
        public void PlayersDataGridSetting(string context)
        {
            SystemAccountHandler sah = new SystemAccountHandler();
            RetrieveHandler rh = new RetrieveHandler();

            JSON filter = new JSON();
            filter.Add(new Dictionary<string, object>());
            List<Player> list = rh.retrievePlayer(null);

            // DataTable 생성
            DataTable dataTable = new DataTable();

            // 컬럼 생성
            dataTable.Columns.Add("uid", typeof(string));
            dataTable.Columns.Add("pid", typeof(string));
            dataTable.Columns.Add("email", typeof(string));
            dataTable.Columns.Add("name", typeof(string));
            dataTable.Columns.Add("birth", typeof(string));
            dataTable.Columns.Add("position", typeof(string));
            dataTable.Columns.Add("weight", typeof(string));
            dataTable.Columns.Add("height", typeof(string));
            dataTable.Columns.Add("status", typeof(string));
            dataTable.Columns.Add("authenticated", typeof(string));
            dataTable.Columns.Add("player", typeof(Player));

            // 데이터 생성
            for (int i = 0; i < list.Count; i++)
            {
                string uid = Convert.ToString(list[i].uid);
                string pid = Convert.ToString(list[i].playerId);
                string email = list[i].email;
                string name = list[i].firstName + list[i].middleName + list[i].lastName;
                string birth = Convert.ToString(list[i].birth);
                string postion = Convert.ToString(list[i].position);
                string weight = Convert.ToString(list[i].weight);
                string height = Convert.ToString(list[i].height);
                string status = list[i].status;
                string authenticated = (list[i].authenticated) ? "TRUE" : "FALSE";
                dataTable.Rows.Add(new object[] { uid, pid, email, name, birth, postion, weight, height, status, authenticated, list[i] });
            }

            // DataTable의 Default View를 바인딩하기
            playerDataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void plyaerSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            PlayersDataGridSetting(playerSearchBox.Text);
        }
    }
}
