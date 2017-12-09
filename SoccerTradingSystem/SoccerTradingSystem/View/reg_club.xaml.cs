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

using Club = SoccerTradingSystem.Model.Club;
using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;

namespace SoccerTradingSystem.Views
{
    /// <summary>
    /// reg_club.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class reg_club : Page
    {
        private Window regWindow;
        public reg_club(Window _regWindow)
        {
            InitializeComponent();
            regWindow = _regWindow;
        }

        private void resiterBtn_Click(object sender, RoutedEventArgs e)
        {
            // NULL Validation
            if (emailBox.Text.Trim() == "" || passwordBox.Password.Trim() == "" || nameBox.Text.Trim() == "" || birthPicker.SelectedDate == null
                 || contactBox.Text == "")
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

            string email = emailBox.Text;
            string password = passwordBox.Password;
            string name = nameBox.Text;
            int birth = Convert.ToInt32(birthPicker.SelectedDate.Value.Date.ToShortDateString().Replace("-", ""));
            string contactNumber = contactBox.Text;
            Club _Club = new Club(-1, email, password, false, -1,null,-1, name, contactNumber, birth, null, null);

            SystemAccountHandler sah = new SystemAccountHandler();
            bool flag = sah.registerClubAccount(_Club);

            if (flag)
            {
                MessageBox.Show("회원가입에 성공하였습니다. \n관리자의 승인을 기다려주세요.");
                regWindow.Close();
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
            }
        }

        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^(\+[0-9]{9})$").Success;
        }
    }
}
