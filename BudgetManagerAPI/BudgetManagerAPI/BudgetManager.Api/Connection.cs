using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;

namespace BudgetManager.Api
{
    public class Connection : IConnection
    {
        public MySqlConnection Connect { get; private set; }

        public Connection(MySqlConnection sqlConnection)
        {
            Connect = sqlConnection;
        }

        public Connection(string connectionString)
        {
            Connect = new MySqlConnection(connectionString);
        }

        public MySqlCommand CreateCommand()
        {
            return Open()
                .CreateCommand();
        }

        public MySqlConnection Open()
        {
            if (Connect == null)
                throw new Exception("No instance class ...");
            if (Connect.State != System.Data.ConnectionState.Open)
            {
                Connect.Open();
            }
            return Connect;
        }

        public void Close()
        {
            if (Connect != null && Connect.State == System.Data.ConnectionState.Open)
            {
                Connect.Close();
                Connect.Dispose();
            }
        }

        public void Dispose()
        {
            Close();
        }
    }
}
