using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Controller.DAC
{
    using User = SoccerTradingSystem.Model.User;
    using Client = SoccerTradingSystem.Model.Client;
    using Player = SoccerTradingSystem.Model.Player;
    using Club = SoccerTradingSystem.Model.Club;
    using Manager = SoccerTradingSystem.Model.Manager;
    using JSON = List<Dictionary<string, object>>;

    class SystemAccountDAC : MariaDBConnector
    {
        public SystemAccountDAC() : base()
        {
        }
        public JSON authenticate(String email, String password)
        {
            // email, password 기반으로 해당하는 user 정보 검색
            query = $"SELECT * from {userTable} where `email` = '{email}' AND `password` = '{password}'";
            queryResult = execute(query);
            return queryResult;
        }
        public void addPlayerData(Player player)
        {
            query = $"INSERT INTO {userTable} (`email`, `password`, `type`) VALUES ('{player.email}', '{player.password}','Client');";
            query += $"INSERT INTO {clientTable} (`userId`, `type`) VALUES ( "
                + $" (SELECT `uid` FROM {userTable} WHERE `email` = '{player.email}') , 'Player');  ";
            query += $"INSERT INTO {playerTable} (clientId, firstName, middleName, lastName, birth, position, weight, height, status)"
                        + $" VALUES ( ( SELECT `clientId` FROM {clientTable} WHERE `userId` =  (SELECT `uid` FROM {userTable} WHERE `email` = '{player.email}')) "
                        + $" ,'{player.firstName}','{player.middleName}','{player.lastName}','{player.birth}','{player.position}','{player.weight}','{player.height}','{player.status}') ";
            queryResult = execute(query);
        }
        public void addClubData(Club club)
        {
            //query = "begin; ";
            query = $"INSERT INTO {userTable} (`email`, `password`, `type`) VALUES ('{club.email}', '{club.password}','Client');  ";
            query += $"INSERT INTO {clientTable} (`userId`, `type`) VALUES ( "
                + $" (SELECT `uid` FROM {userTable} WHERE `email` = '{club.email}') , 'Club');  ";
            query += $"INSERT INTO {clubTable} (clientId, birth, name, contactNumber)"
                        + $" VALUES ( ( SELECT `clientId` FROM {clientTable} WHERE `userId` =  (SELECT `uid` FROM {userTable} WHERE `email` = '{club.email}')) "
                        + $" ,'{club.birth}','{club.name}','{club.contactNumber}') ";
            queryResult = execute(query);
        }
        public void addManagerData(Manager manager)
        {
            query = $"INSERT INTO {userTable} (`email`, `password`, `type`) VALUES ('{manager.email}', '{manager.password}','Manager');  ";
            query += $"INSERT INTO {ManagerTable} (`userId`, `name`, `telNumber`) VALUES ( "
                + $" (SELECT `uid` FROM {userTable} WHERE `email` = '{manager.email}') , '{manager.name}', '{manager.telNumber}');  ";
            queryResult = execute(query);
        }


        public bool updateUserData(User user)
        {
            query = $"UPDATE {userTable} SET `password`  = {user.password}, `authenticated` = {user.authenticated}'  WHERE uid = {user.uid}; "; 
            queryResult = execute(query);
            return true;
        }
    }
}
