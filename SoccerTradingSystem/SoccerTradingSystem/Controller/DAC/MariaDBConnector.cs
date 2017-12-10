using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.Common;
using MySql.Data.MySqlClient;

namespace SoccerTradingSystem.Controller.DAC
{
    using JSON = List<Dictionary<string, object>>;
    class MariaDBConnector
    {
        private static String serverIp = "127.0.0.1";
        private static String id = "root";
        private static String password = "admin";
        private static String databaseName = "pms_real";

        protected MySqlConnection conn;
        protected String query;
        protected JSON queryResult;

        // @@ attributes 추가
        protected String userTable = "User";
        protected String clientTable = "Client";
        protected String playerTable = "Player";
        protected String clubTable = "Club";
        protected String ManagerTable = "Manager";
        protected String contractTable = "Contract";
        protected String BankAccountTable = "BankAccount";
        protected String BankAccountAuthTable = "BankAccountAuth";
        protected String GameTable = "Game";
        protected String AssistTable = "Assist";
        protected String RatingTable = "Rating";
        protected String GoalTable = "Goal";
        protected String PaymentTable = "payment";
        protected String DailyPaymentTable = "dailyPayment";
        protected String WeeklypaymentTable = "weeklypayment";
        protected String MonthlypaymentTable = "monthlypayment";

        public MariaDBConnector()
        {
        }
        public bool connectOpen()
        {
            String connectString =
                "Server=" + MariaDBConnector.serverIp
                + ";Database = " + MariaDBConnector.databaseName
                + ";Uid=" + MariaDBConnector.id
                + ";Pwd=" + MariaDBConnector.password + ";";

            conn = new MySqlConnection(connectString);
            conn.Open();

            return true;
        }
        public bool connectClose()
        {
            conn.Close();
            return true;
        }
        // 이름 변경 @@ query -> execute
        public JSON execute(String query)
        {
            // 이부분 동기화 필요
            try
            {
                connectOpen();
                MySqlCommand cmd = new MySqlCommand(query, this.conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                var results = new List<Dictionary<string, object>>();
                var schema = new List<string>();
                var fieldCount = rdr.FieldCount;
                for (var i = 0; i < fieldCount; i++)
                {
                    schema.Add(rdr.GetName(i));
                }
                while (rdr.Read())
                {
                    var dic = new Dictionary<string, object>();
                    for (var i = 0; i < fieldCount; i++)
                    {
                        dic[schema[i]] = rdr[schema[i]];
                    }
                    results.Add(dic);
                }
                connectClose();
                queryResult = results;
                return results;
            }
            catch (Exception e)
            {
                throw (e);
            }

        }
    }
}
