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
using Club = SoccerTradingSystem.Model.Club;
using Manager = SoccerTradingSystem.Model.Manager;
using User = SoccerTradingSystem.Model.User;
using RetrieveHandler = SoccerTradingSystem.Controller.RetrieveHandler;
using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;

namespace SoccerTradingSystem.Views
{
    /// <summary>
    /// user_info.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    using JSON = List<Dictionary<string, object>>;

    public partial class user_info : Page
    {
        bool flag = false;

        public user_info()
        {
            InitializeComponent();
        }

        // 페이지가 로드 되었을 때 함수 호출 최초로 플레이어 데이터 그리드를 보여줌
        private void OnPageLoad(object sender, RoutedEventArgs e)
        {
            PlayersDataGridSetting("");
            //ClubsDataGridSetting("");
            //ManagersDataGridSetting("");
            UserDataGridSetting("");
        }

        // 플레이어 그리드에서 더블 클릭시 메서드 호출 인증을 업데이트함
        private void Player_Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            SystemAccountHandler sah = new SystemAccountHandler();
            RetrieveHandler rh = new RetrieveHandler();
            DataRowView row = (DataRowView)playerDataGrid.SelectedItems[0];
            int uid = Convert.ToInt32((row[0]));

            JSON filter = new JSON();
            filter.Add(new Dictionary<string, object>());
            filter[0].Add("uid", uid);
            List<User> users = rh.retrieveUser(filter);
            rh.retrievePlayer(null);

            bool auth = users[0].authenticated;

            if (auth)
            {
                if (MessageBox.Show("현재 유저는 관리자 인증이 활성화 된 상태입니다.\n인증을 비활성화 하시겠습니까?", "question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    User _user = new User(users[0].uid, users[0].email, users[0].password, false);
                    sah.updateUser(_user);
                }
            }
            else
            {
                if (MessageBox.Show("현재 유저는 관리자 인증이 비활성화 된 상태입니다.\n인증을 활성화 하시겠습니까?", "question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    User _user = new User(users[0].uid, users[0].email, users[0].password, true);
                    sah.updateUser(_user);
                }
            }
            PlayersDataGridSetting("");
        }

        // 클럽 그리드에서 더블 클릭시 메서드 호출 인증을 업데이트함
        private void Club_Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            SystemAccountHandler sah = new SystemAccountHandler();
            DataRowView row = (DataRowView)clubDataGrid.SelectedItems[0];
            int uid = Convert.ToInt32((row[0]));

            //bool auth = sah.retrieveUserData(uid).authenticated;
            //if (auth)
            //{
            //    if (MessageBox.Show("현재 유저는 관리자 인증이 활성화 된 상태입니다.\n인증을 비활성화 하시겠습니까?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            //    {
            //        sah.updateUserAuth(uid, false);
            //    }
            //}
            //else
            //{
            //    if (MessageBox.Show("현재 유저는 관리자 인증이 비활성화 된 상태입니다.\n인증을 활성화 하시겠습니까?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            //    {
            //        sah.updateUserAuth(uid, true);
            //    }
            //}
            ClubsDataGridSetting("");
        }

        // 매니저 그리드에서 더블 클릭시 메서드 호출 인증을 업데이트함
        private void Manager_Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            SystemAccountHandler sah = new SystemAccountHandler();
            DataRowView row = (DataRowView)managerDataGrid.SelectedItems[0];
            int uid = Convert.ToInt32((row[0]));
            //bool auth = sah.retrieveUserData(uid).authenticated;
            //if (auth)
            //{
            //    if (MessageBox.Show("현재 유저는 관리자 인증이 활성화 된 상태입니다.\n인증을 비활성화 하시겠습니까?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            //    {
            //        sah.updateUserAuth(uid, false);
            //    }
            //}
            //else
            //{
            //    if (MessageBox.Show("현재 유저는 관리자 인증이 비활성화 된 상태입니다.\n인증을 활성화 하시겠습니까?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            //    {
            //        sah.updateUserAuth(uid, true);
            //    }
            //}
            ManagersDataGridSetting("");
        }

        // 유저 그리드에서 더블 클릭시 메서드 호출 인증을 업데이트함
        private void User_Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            SystemAccountHandler sah = new SystemAccountHandler();
            DataRowView row = (DataRowView)userDataGrid.SelectedItems[0];
            int uid = Convert.ToInt32((row[0]));
            //bool auth = sah.retrieveUserData(uid).authenticated;
            //if (auth)
            //{
            //    if (MessageBox.Show("현재 유저는 관리자 인증이 활성화 된 상태입니다.\n인증을 비활성화 하시겠습니까?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            //    {
            //        sah.updateUserAuth(uid, false);
            //    }
            //}
            //else
            //{
            //    if (MessageBox.Show("현재 유저는 관리자 인증이 비활성화 된 상태입니다.\n인증을 활성화 하시겠습니까?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            //    {
            //        sah.updateUserAuth(uid, true);
            //    }
            //}
            UserDataGridSetting("");
        }

        // 탭을 선택할 시 그에 맞는 그리드를 업데이트함
        public void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (flag)
            {
                if (PlayersTab.IsSelected)
                {
                    PlayersDataGridSetting("");
                    flag = true;
                }

                if (ClubsTab.IsSelected)
                {
                    ClubsDataGridSetting("");
                }

                if (ManagersTab.IsSelected)
                {
                    ManagersDataGridSetting("");
                }

                if (UsersTab.IsSelected)
                {
                    UserDataGridSetting("");
                }
            }
        }

        // 플레이어 그리드 구성
        public void PlayersDataGridSetting(string context)
        {
            SystemAccountHandler sah = new SystemAccountHandler();
            RetrieveHandler rh = new RetrieveHandler();

            JSON filter = new JSON();
            filter.Add(new Dictionary<string, object>());
            List<User> users = rh.retrieveUser(filter);
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
                dataTable.Rows.Add(new string[] { uid, pid, email, name, birth, postion, weight, height, status, authenticated });
            }

            // DataTable의 Default View를 바인딩하기
            playerDataGrid.ItemsSource = dataTable.DefaultView;
        }

        // 클럽 그리드 구성
        private void ClubsDataGridSetting(string context)
        {
            SystemAccountHandler sah = new SystemAccountHandler();
            RetrieveHandler rh = new RetrieveHandler();

            JSON filter = new JSON();
            filter.Add(new Dictionary<string, object>());
            List<User> users = rh.retrieveUser(filter);
            List<Club> cist = rh.retrieveClub(null);

            // DataTable 생성
            DataTable dataTable = new DataTable();

            // 컬럼 생성
            dataTable.Columns.Add("uid", typeof(string));
            dataTable.Columns.Add("cid", typeof(string));
            dataTable.Columns.Add("email", typeof(string));
            dataTable.Columns.Add("name", typeof(string));
            dataTable.Columns.Add("birth", typeof(string));
            dataTable.Columns.Add("contactnumber", typeof(string));
            dataTable.Columns.Add("authenticated", typeof(string));

            // 데이터 생성
            for (int i = 0; i < cist.Count; i++)
            {
                string uid = Convert.ToString(cist[i].uid);
                string pid = Convert.ToString(cist[i].clubId);
                string email = cist[i].email;
                string name = cist[i].name;
                string birth = Convert.ToString(cist[i].birth);
                string contactnumber = cist[i].contactNumber;
                string authenticated = (cist[i].authenticated) ? "TRUE" : "FALSE";
                dataTable.Rows.Add(new string[] { uid, pid, email, name, birth, contactnumber, authenticated });
            }

            // DataTable의 Default View를 바인딩하기
            clubDataGrid.ItemsSource = dataTable.DefaultView;
        }

        // 매니저 그리드 구성
        private void ManagersDataGridSetting(string context)
        {
            SystemAccountHandler sah = new SystemAccountHandler();
            RetrieveHandler rh = new RetrieveHandler();

            JSON filter = new JSON();
            filter.Add(new Dictionary<string, object>());
            List<User> users = rh.retrieveUser(filter);
            List<Manager> mlist = rh.retrieveManager(null);

            // DataTable 생성
            DataTable dataTable = new DataTable();

            // 컬럼 생성
            dataTable.Columns.Add("uid", typeof(string));
            dataTable.Columns.Add("mid", typeof(string));
            dataTable.Columns.Add("email", typeof(string));
            dataTable.Columns.Add("name", typeof(string));
            dataTable.Columns.Add("telnumber", typeof(string));
            dataTable.Columns.Add("authenticated", typeof(string));

            // 데이터 생성
            for (int i = 0; i < mlist.Count; i++)
            {
                string uid = Convert.ToString(mlist[i].uid);
                string pid = Convert.ToString(mlist[i].managerId);
                string email = mlist[i].email;
                string name = mlist[i].name;
                string telNumber = mlist[i].telNumber;
                string authenticated = (mlist[i].authenticated) ? "TRUE" : "FALSE";
                dataTable.Rows.Add(new string[] { uid, pid, email, name, telNumber, authenticated });
            }

            // DataTable의 Default View를 바인딩하기
            managerDataGrid.ItemsSource = dataTable.DefaultView;

        }

        private void UserDataGridSetting(string context)
        {
            try
            {
                SystemAccountHandler sah = new SystemAccountHandler();
                RetrieveHandler rh = new RetrieveHandler();

                JSON filter = new JSON();
                filter.Add(new Dictionary<string, object>());
                List<User> users = rh.retrieveUser(filter);
                List<User> ulist = rh.retrieveUser(null);

                // DataTable 생성
                DataTable dataTable = new DataTable();

                // 컬럼 생성
                dataTable.Columns.Add("uid", typeof(string));
                dataTable.Columns.Add("email", typeof(string));
                dataTable.Columns.Add("password", typeof(string));
                dataTable.Columns.Add("authenticated", typeof(string));

                // 데이터 생성
                for (int i = 0; i < ulist.Count; i++)
                {
                    string uid = Convert.ToString(ulist[i].uid);
                    string email = Convert.ToString(ulist[i].email);
                    string password = Convert.ToString(ulist[i].password);
                    string authenticated = (ulist[i].authenticated) ? "TRUE" : "FALSE";
                    dataTable.Rows.Add(new string[] { uid, email, password, authenticated });
                }

                // DataTable의 Default View를 바인딩하기
                userDataGrid.ItemsSource = dataTable.DefaultView;
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message.ToString());
            }

        }

        private void plyaerSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            PlayersDataGridSetting(playerSearchBox.Text);
        }

        private void clubSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            ClubsDataGridSetting(clubSearchBox.Text);
        }

        private void managerSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            ManagersDataGridSetting(managerSearchBox.Text);
        }

        private void userSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            UserDataGridSetting(userSearchBox.Text);
        }
    }
}