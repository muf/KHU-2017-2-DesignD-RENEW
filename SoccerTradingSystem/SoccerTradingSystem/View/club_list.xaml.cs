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

using Club = SoccerTradingSystem.Model.Club;
using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;

namespace SoccerTradingSystem.Views
{
    /// <summary>
    /// club_list.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class club_list : Page
    {
        public club_list()
        {
            InitializeComponent();
        }

        // 페이지가 로드 되었을 때 함수 호출 최초로 플레이어 데이터 그리드를 보여줌
        private void OnPageLoad(object sender, RoutedEventArgs e)
        {
            ClubsDataGridSetting("");
        }

        // 클럽 그리드에서 더블 클릭시 메서드 호출 인증을 업데이트함
        private void Club_Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView row = (DataRowView)clubDataGrid.SelectedItems[0];
            int uid = Convert.ToInt32((row[0]));

            ClubDetailWindow _ClubDetailWindow = new ClubDetailWindow(uid);
            _ClubDetailWindow.Show();
        }


        // 클럽 그리드 구성
        private void ClubsDataGridSetting(string context)
        {
            //SystemAccountHandler sah = new SystemAccountHandler();
            //List<Club> cist = sah.retrieveClubData(context);

            //// DataTable 생성
            //DataTable dataTable = new DataTable();

            //// 컬럼 생성
            //dataTable.Columns.Add("uid", typeof(string));
            //dataTable.Columns.Add("cid", typeof(string));
            //dataTable.Columns.Add("email", typeof(string));
            //dataTable.Columns.Add("name", typeof(string));
            //dataTable.Columns.Add("birth", typeof(string));
            //dataTable.Columns.Add("contactnumber", typeof(string));
            //dataTable.Columns.Add("authenticated", typeof(string));

            //// 데이터 생성
            //for (int i = 0; i < cist.Count; i++)
            //{
            //    string uid = Convert.ToString(cist[i].uid);
            //    string pid = Convert.ToString(cist[i].clubId);
            //    string email = cist[i].email;
            //    string name = cist[i].name;
            //    string birth = Convert.ToString(cist[i].birth);
            //    string contactnumber = cist[i].contactNumber;
            //    string authenticated = (cist[i].authenticated) ? "TRUE" : "FALSE";
            //    dataTable.Rows.Add(new string[] { uid, pid, email, name, birth, contactnumber, authenticated });
            //}

            //// DataTable의 Default View를 바인딩하기
            //clubDataGrid.ItemsSource = dataTable.DefaultView;
        }


        private void clubSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            ClubsDataGridSetting(clubSearchBox.Text);
        }
    }
}
