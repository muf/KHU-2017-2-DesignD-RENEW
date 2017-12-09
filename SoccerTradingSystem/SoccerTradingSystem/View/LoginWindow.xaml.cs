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

using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;

namespace SoccerTradingSystem.Views
{
    /// <summary>
    /// LoginWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginWindow : Window
    {
        private top_panel TP;
        public LoginWindow(top_panel top_panel)
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.TP = top_panel;
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            // get email & password
            string email_string = emailBox.Text;
            string password_string = passwordBox.Password;

            // Validation Check (null)
            if(email_string.Trim() == "" || password_string.Trim() == "")
            {
                MessageBox.Show("입력되지 않은 값이 있습니다.");
                return;
            }

            // user login
            SystemAccountHandler sah = new SystemAccountHandler();
            String email = email_string;
            String password = password_string;
            App.cookie = sah.login(email, password);

            // Dismatch Error
            if (App.cookie == null)
            {
                MessageBox.Show("로그인 실패");
                return;
            }

            // No Auth Error
            if (!App.cookie.user.authenticated)
            {
                MessageBox.Show("해당 아이디는 관리자의 승인을 허가 받지 못한 아이디입니다.");
                return;
            }

            // Top Panel & Main logined form setting
            TP.logined_success(email);
            this.Close();
        }

        private void PW_Keydown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                loginBtn_Click(sender, e);
            }
        }

        // Eecape
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }
    }
}
