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

using Types = SoccerTradingSystem.Model.Types;
using Player = SoccerTradingSystem.Model.Player;
using RetrieveHandler = SoccerTradingSystem.Controller.RetrieveHandler;
using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;

namespace SoccerTradingSystem.Views
{
    /// <summary>
    /// PlayerDetailWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    using JSON = List<Dictionary<string, object>>;
    public partial class PlayerDetailWindow : Window
    {
        private int curPlayerUid;

        public PlayerDetailWindow(int uid)
        {
            InitializeComponent();
            curPlayerUid = uid;

            if (App.cookie != null)
            {
                if (App.cookie.type == Types.UserType.Club)
                {
                    PlayerOfferBtn.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    PlayerOfferBtn.Visibility = System.Windows.Visibility.Hidden;
                }
            }else
            {
                PlayerOfferBtn.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            SystemAccountHandler sah = new SystemAccountHandler();
            RetrieveHandler rh = new RetrieveHandler();

            JSON filter = new JSON();
            filter.Add(new Dictionary<string, object>());
            filter[0].Add("uid", curPlayerUid);
            List<Player> players = rh.retrievePlayer(filter);
            rh.retrievePlayer(null);

            Player curPlayer = players[0];

            string pName = curPlayer.firstName + curPlayer.middleName + " " + curPlayer.lastName;
            string pBirth = curPlayer.birth.ToString();
            string pPosition = curPlayer.position;
            string pWeight = curPlayer.weight.ToString();
            string pHeight = curPlayer.height.ToString();

            nameBlock.Text = pName;
            birthBlock.Text = pBirth;
            positionBlock.Text = pPosition;
            weightBlock.Text = pWeight;
            heightBlock.Text = pHeight;
        }

        private bool playerCheck()
        {
            RetrieveHandler rh = new RetrieveHandler();

            JSON filter = new JSON();
            filter.Add(new Dictionary<string, object>());
            filter[0].Add("uid", curPlayerUid);
            List<Player> players = rh.retrievePlayer(filter);
            rh.retrievePlayer(null);

            Player curPlayer = players[0];
            return true;
        }

        private void PlayerOfferBtn_Click(object sender, RoutedEventArgs e)
        {
            playerCheck();
            MakeContractWindow _MakeContractWindow = new MakeContractWindow(curPlayerUid);
            _MakeContractWindow.Show();
        }
    }
}
