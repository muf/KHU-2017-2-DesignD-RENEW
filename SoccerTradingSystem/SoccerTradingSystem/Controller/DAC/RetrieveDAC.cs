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

    class RetrieveDAC : MariaDBConnector
    {

        public JSON findUser(String email)
        {
            // email, password 기반으로 해당하는 user 정보 검색
            query = $"SELECT * from {userTable} where `email` = '{email}'";
            queryResult = execute(query);
            return queryResult;
        }
        public JSON getUserData()
        {
            query = "SELECT * from (SELECT user.*, clientId, client.type as clientType from user LEFT OUTER JOIN client ON user.uid = client.userId ) as CT  LEFT OUTER JOIN manager ON CT.uid = manager.userId";
            queryResult = execute(query);
            return queryResult;
        }
        public JSON getUserData(int uid)
        {
            query = $"SELECT * from (SELECT user.*, clientId, client.type as clientType from user LEFT OUTER JOIN client ON user.uid = client.userId ) as CT  LEFT OUTER JOIN manager ON CT.uid = manager.userId  WHERE `uid`={uid}";
            queryResult = execute(query);
            return queryResult;
        }
        public JSON getPlayersData()
        {
            query = "SELECT * from (SELECT user.*, client.clientId, client.type as client_type from user INNER JOIN client ON user.uid = client.userId) as CT INNER JOIN player ON player.clientId = CT.clientId";
            queryResult = execute(query);
            return queryResult;
        }
        public JSON getPlayersData(int uid)
        {
            query = $"SELECT * from (SELECT user.*, client.clientId, client.type as client_type from user INNER JOIN client ON user.uid = client.userId) as CT INNER JOIN player ON player.clientId = CT.clientId where uid={uid}";
            queryResult = execute(query);
            return queryResult;
        }
        public JSON getClubsData()
        {
            query = " SELECT* from(SELECT user.*, client.clientId as clientId, client.type as client_type from user INNER JOIN client ON user.uid = client.userId) as CT INNER JOIN club ON club.clientId = CT.clientId";
            queryResult = execute(query);
            return queryResult;
        }
        public JSON getClubsData(int uid)
        {
            query = $" SELECT* from(SELECT user.*, client.clientId as clientId, client.type as client_type from user INNER JOIN client ON user.uid = client.userId) as CT INNER JOIN club ON club.clientId = CT.clientId where uid={uid}";
            queryResult = execute(query);
            return queryResult;
        }
        public JSON getManagersData()
        {
            query = "SELECT user.*, manager.managerId, manager.name, manager.telNumber from user INNER JOIN manager ON user.uid = manager.userId";
            queryResult = execute(query);
            return queryResult;
        }
        public JSON getManagersData(int uid)
        {
            query = $"SELECT user.*, manager.managerId, manager.name, manager.telNumber from user INNER JOIN manager ON user.uid = manager.userId where uid={uid}";
            queryResult = execute(query);
            return queryResult;
        }
        public JSON getBankAccountData()
        {
            query = "SELECT * from (SELECT BankAccount.*, secretKey from BankAccount INNER JOIN BankAccountAuth ON BankAccount.bankAccountAuthId = BankAccountAuth.bankAccountAuthId) as CT INNER JOIN client ON client.clientId = CT.clientId ";
            queryResult = execute(query);
            return queryResult;

        }
        public JSON getBankAccountData(int uid)
        {
            query = $"SELECT * from (SELECT BankAccount.*, secretKey from BankAccount INNER JOIN BankAccountAuth ON BankAccount.bankAccountAuthId = BankAccountAuth.bankAccountAuthId) as CT INNER JOIN client ON client.clientId = CT.clientId WHERE userId={uid}";
            queryResult = execute(query);
            return queryResult;
        }
    }
}
