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

using Club = SoccerTradingSystem.Model.Club;
using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;
using Types = SoccerTradingSystem.Model.Types;
using Player = SoccerTradingSystem.Model.Player;
using RetrieveHandler = SoccerTradingSystem.Controller.RetrieveHandler;
using ViewClubPlayer = SoccerTradingSystem.View.ViewClubPlayer;

namespace SoccerTradingSystem.Views
{
    /// <summary>
    /// ClubDetailWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    using JSON = List<Dictionary<string, object>>;
    public partial class ClubDetailWindow : Window
    {
        private int curClubUid;

        public ClubDetailWindow(int uid)
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            curClubUid = uid;

            if (App.cookie != null)
            {
                //if (App.cookie.type == "Player")
                //{
                //    ClubOfferBtn.Visibility = System.Windows.Visibility.Visible;
                //}
                //else
                //{
                //    ClubOfferBtn.Visibility = System.Windows.Visibility.Hidden;
                //}
            }
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            RetrieveHandler rh = new RetrieveHandler();

            JSON filter = new JSON();
            filter.Add(new Dictionary<string, object>());
            filter[0].Add("uid", curClubUid);
            List<Club> clubs = rh.retrieveClub(filter);

            string cName = clubs[0].name;
            string cBirth = clubs[0].birth.ToString();
            string cContactNumber = clubs[0].contactNumber;

            nameBlock.Text = cName;
            birthBlock.Text = cBirth;
            contactNumberBlock.Text = cContactNumber;
        }

        private void ClubOfferBtn_Click(object sender, RoutedEventArgs e)
        {
            MakeContractWindow _MakeContractWindow = new MakeContractWindow(curClubUid);
            _MakeContractWindow.Show();
        }

        private void ViewPlayerBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewClubPlayer _ViewClubPlayer = new ViewClubPlayer(curClubUid);
            _ViewClubPlayer.Show();
        }
    }
}
