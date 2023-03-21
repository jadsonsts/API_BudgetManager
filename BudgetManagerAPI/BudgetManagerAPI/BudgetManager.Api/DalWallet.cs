using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Drawing.Drawing2D;
using MySql.Data.MySqlClient;

namespace BudgetManager.Api 
{
    public class DalWallet: IDalWallet
    {
        private IConnection _connection;
        public DalWallet(IConnection connection)
        {
            _connection = connection;
        }

        public bool Delete(Wallet value)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public bool Edit(Wallet value)
        {
            throw new NotImplementedException();
        }

        public Wallet Find(object Customer_ID)
        {
            Wallet wallet = null;
            using (MySqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "SELECT wallet_ID, name, amount, customer_ID FROM wallet where customer_ID = @Id";
                _command.Parameters.Add("@Id", MySqlDbType.Int32).Value = Customer_ID; 
                using (MySqlDataReader reader = _command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        wallet = new Wallet
                        {
                            WalletID = reader.GetInt32(0),
                            WalletName = reader.GetString(1),
                            Amount = reader.GetDecimal(2),
                            Customer_ID = reader.GetInt32(3),
                        };
                    }
                }
            }
            return wallet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>

        public Wallet Insert(Wallet value)
        {
            using (MySqlCommand _command = _connection.CreateCommand())
            {
                // cria o comando sql
                _command.CommandText = "INSERT INTO wallet (name, amount, customer_ID) VALUES (@name, @amount, @Customer_ID) ;"; //SELECT LAST_INSERT_ID()

                // Adiciona os parametros ao sql
                _command.Parameters.Add("@name", MySqlDbType.String).Value = value.WalletName;
                _command.Parameters.Add("@amount", MySqlDbType.Decimal).Value = value.Amount;
                _command.Parameters.Add("@Customer_ID", MySqlDbType.Int32).Value = value.Customer_ID;

                //value.WalletID = (int)_command.ExecuteScalar();

                return value;
            }

            return null;
        }

        public IEnumerable<Wallet> List()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Wallet> ListFiltered(params object[] filter)
        {
            throw new NotImplementedException();
        }
    }
}