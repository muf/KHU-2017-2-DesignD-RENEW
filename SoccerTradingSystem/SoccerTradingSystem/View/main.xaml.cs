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

using Types = SoccerTradingSystem.Model.Types;

namespace SoccerTradingSystem.Views
{
    using LocalData = SoccerTradingSystem.Model.LocalData;
    /// <summary>
    /// main.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class main : Page
    {
        public main()
        {
            InitializeComponent();
            top_panel TP = new top_panel(this);
            top_frame.Navigate(TP);
            
            if (App.cookie != null)
            {
                if (App.cookie.userType == Types.UserType.Manager)
                {
                    managerMenu.Visibility = System.Windows.Visibility.Visible;
                    userInfoBtn.Visibility = System.Windows.Visibility.Visible;
                    gameInfoBtn.Visibility = System.Windows.Visibility.Visible;
                    ContractionBtn.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    managerMenu.Visibility = System.Windows.Visibility.Collapsed;
                    userInfoBtn.Visibility = System.Windows.Visibility.Collapsed;
                    gameInfoBtn.Visibility = System.Windows.Visibility.Collapsed;
                }
                if (App.cookie.userType == Types.UserType.Club || App.cookie.userType == Types.UserType.Player)
                {
                    ContractionBtn.Visibility = System.Windows.Visibility.Visible;
                }
            }
            else
            {
                managerMenu.Visibility = System.Windows.Visibility.Collapsed;
                userInfoBtn.Visibility = System.Windows.Visibility.Collapsed;
                gameInfoBtn.Visibility = System.Windows.Visibility.Collapsed;
                ContractionBtn.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        public void returnToHome()
        {
            content_frame.Navigate(new home());
        }

        private void gameInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            //game_list _game_list = new game_list();
            //content_frame.Navigate(_game_list);
        }

        private void userInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            user_info _user_info = new user_info();
            content_frame.Navigate(_user_info);
        }

        private void PlayerBtn_Click(object sender, RoutedEventArgs e)
        {
            player_list _plyaer_list = new player_list();
            content_frame.Navigate(_plyaer_list);
        }

        private void ClubBtn_Click(object sender, RoutedEventArgs e)
        {
            club_list _club_list = new club_list();
            content_frame.Navigate(_club_list);
        }

        private void ContractionBtn_Click(object sender, RoutedEventArgs e)
        {
            //contraction_list _contraction_list = new contraction_list();
            //content_frame.Navigate(_contraction_list);
        }
    }
}
