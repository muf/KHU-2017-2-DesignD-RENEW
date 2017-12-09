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

using Player = SoccerTradingSystem.Model.Player;
using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;

namespace SoccerTradingSystem.Views
{
    /// <summary>
    /// reg_player.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class reg_player : Page
    {
        private Window regWindow;
        public reg_player(Window _regWindow)
        {
            InitializeComponent();
            regWindow = _regWindow;
        }


        private void resiterBtn_Click(object sender, RoutedEventArgs e)
        {
            // NULL Validation
            if (emailBox.Text.Trim() == "" || passwordBox.Password.Trim() == "" || firstnameBox.Text.Trim() == "" || middlenameBox.Text.Trim() == "" || lastnameBox.Text.Trim() == "" || birthPicker.SelectedDate == null
                 || positionComboBox.SelectionBoxItem.ToString().Trim() == "" || heightBox.Text.Trim() == "" || weightBox.Text.Trim() == "")
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
            if (Convert.ToInt32(heightBox.Text) > 250 || Convert.ToInt32(heightBox.Text) < 0)
            {
                MessageBox.Show("키가 올바르지 않습니다. 다시 확인해주세요.");
                return;
            }
            if (Convert.ToInt32(weightBox.Text) > 250 || Convert.ToInt32(weightBox.Text) < 0)
            {
                MessageBox.Show("몸무게가 올바르지 않습니다. 다시 확인해주세요.");
                return;
            }

            // Value
            string email = emailBox.Text;
            string password = passwordBox.Password;
            string firstName = firstnameBox.Text;
            string middleName = middlenameBox.Text;
            string lastName = lastnameBox.Text;
            int birth = Convert.ToInt32(birthPicker.SelectedDate.Value.Date.ToShortDateString().Replace("-", ""));
            string position = positionComboBox.SelectionBoxItem.ToString();
            int height = Convert.ToInt32(heightBox.Text);
            int weight = Convert.ToInt32(weightBox.Text);
            Player _Player = new Player(-1, email, password, false, -1, null, -1, firstName, middleName, lastName, birth, position, -1, weight, height, "Free", null);


            SystemAccountHandler sah = new SystemAccountHandler();
            try
            {
                bool flag = sah.registerPlayerAccount(_Player);

                if (flag)
                {
                    MessageBox.Show("회원가입에 성공하였습니다. \n관리자의 승인을 기다려주세요.");
                    regWindow.Close();
                }
                else
                    MessageBox.Show("회원가입에 실패하였습니다.");
            }
            catch (Exception eee)
            {
                MessageBox.Show(eee.Message.ToString());
            }

            regWindow.Close();

        }

        private void emailBox_Leave(object sender, System.EventArgs e)
        {
            if (emailBox.Text.Trim() != "")
            {
                bool emailCheck = Regex.IsMatch(emailBox.Text, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                if (!emailCheck)
                {
                    MessageBox.Show("이메일 형식이 올바르지 않습니다. 다시 확인해주세요.");
                }
            }
        }
    }
}
