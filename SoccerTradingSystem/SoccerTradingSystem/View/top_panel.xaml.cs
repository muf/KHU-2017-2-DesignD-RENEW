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

namespace SoccerTradingSystem.Views
{
    /// <summary>
    /// top_panel.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class top_panel : Page
    {
        private main main;
        public top_panel(main _main)
        {
            InitializeComponent();
            main = _main;

            // 로그아웃 된 상태
            if(App.cookie == null)
            {
                loginBtn.Visibility = System.Windows.Visibility.Visible;
                logoutBtn.Visibility = System.Windows.Visibility.Collapsed;
                user_registBtn.Visibility = System.Windows.Visibility.Visible;
                myInfoBtn.Visibility = System.Windows.Visibility.Collapsed;
                topLoginedEmail.Text = "게스트 모드";
            }
            else
            {
                loginBtn.Visibility = System.Windows.Visibility.Collapsed;
                logoutBtn.Visibility = System.Windows.Visibility.Visible;
                user_registBtn.Visibility = System.Windows.Visibility.Collapsed;
                myInfoBtn.Visibility = System.Windows.Visibility.Visible;
                topLoginedEmail.Text = "환영합니다.  " + App.cookie.user.email + "[ " + App.cookie.type + " ]";
            }
        }

        public void logout_Click(object sender, RoutedEventArgs e)
        {
            App.cookie = null;

            //MessageBox.Show("logout");
            loginBtn.Visibility = System.Windows.Visibility.Visible;
            logoutBtn.Visibility = System.Windows.Visibility.Collapsed;

            main.NavigationService.Refresh();
            main.returnToHome();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow(this);
            loginWindow.Show();
        }

        private void user_registBtn_Click(object sender, RoutedEventArgs e)
        {
            // regist
            PlayerRegistWindow userRegWindow = new PlayerRegistWindow();
            userRegWindow.Show();
        }

        public void myInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            main.Navi_My_info();
        }

        public void logined_success(string email)
        {
            main.NavigationService.Refresh(); // refresh
        }
    }
}