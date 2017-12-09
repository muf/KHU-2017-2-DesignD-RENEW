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
using System.Text.RegularExpressions;

using Manager = SoccerTradingSystem.Model.Manager;
using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;

namespace SoccerTradingSystem.Views
{
    /// <summary>
    /// reg_manager.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class reg_manager : Page
    {
        private Window regWindow;
        public reg_manager(Window _regWindow)
        {
            InitializeComponent();
            regWindow = _regWindow;
        }

        private void resiterBtn_Click(object sender, RoutedEventArgs e)
        {
            // NULL Validation
            if (emailBox.Text.Trim() == "" || passwordBox.Password.Trim() == "" || nameBox.Text.Trim() == "" || telBox.Text.Trim() == "")
            {
                MessageBox.Show("필수 사항을 모두 입력해 주십시오.");
                return;
            }

            // Validation
            bool emailCheck = Regex.IsMatch(emailBox.Text, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            if (!emailCheck)
            {
                MessageBox.Show("이메일 형식이 올바르지 않습니다. 다시 확인해주세요.");
                return;
            }
            if (IsPhoneNumber(passwordBox.Password))
            {
                MessageBox.Show("전화번호 형식이 올바르지 않습니다. 다시 확인해주세요.");
                return;
            }

            // Value
            string email = emailBox.Text;
            string password = passwordBox.Password;
            string name = nameBox.Text;
            string telNumber = telBox.Text;
            Manager _Manager = new Manager(-1, email, password, false, -1, name, telNumber);

            // 레지스트
            SystemAccountHandler sah = new SystemAccountHandler();
            // 입력 값 받아서 newPlayer에 셋팅
            bool flag = sah.registerManagerAccount(_Manager);

            if (flag)
            {
                MessageBox.Show("회원가입에 성공하였습니다. \n관리자의 승인을 기다려주세요."); return;
            }
            else
                MessageBox.Show("회원가입에 실패하였습니다.");


            // 윈도우 닫음
            regWindow.Close();
        }

        private void emailBox_Leave(object sender, System.EventArgs e)
        {
            bool emailCheck = Regex.IsMatch(emailBox.Text, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            if (!emailCheck)
            {
                MessageBox.Show("이메일 형식이 올바르지 않습니다. 다시 확인해주세요.");
                regWindow.Close();
            }
        }

        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^(\+[0-9]{9})$").Success;
        }
    }
}
