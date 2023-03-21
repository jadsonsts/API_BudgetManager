using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Drawing.Drawing2D;
using MySql.Data.MySqlClient;

namespace BudgetManager.Api
{
    public class DalTransaction : IDalTransaction
    {
        private IConnection _connection;
        public DalTransaction(IConnection connection)
        {
            _connection = connection;
        }

        public bool Delete(Transaction value)
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

        public bool Edit(Transaction value)
        {
            using (MySqlCommand _command = _connection.CreateCommand())
            {
                // cria o comando sql
                _command.CommandText = @"UPDATE transation
                                        SET amount = @amount
                                          , category_ID = @categoryID
                                          , reference = @reference
                                          , date = @date
                                          , comment = @comment
                                          , transactionType = @transactionType
                                          , wallet_ID = @walletID
                                        WHERE transaction_ID = @transactionId";

                // Adiciona os parametros ao sql
                _command.Parameters.Add("@amount", MySqlDbType.Double).Value = value.Amount;
                _command.Parameters.Add("@categoryID", MySqlDbType.Int32).Value = value.CategoryID;
                _command.Parameters.Add("@reference", MySqlDbType.String).Value = value.Reference;
                _command.Parameters.Add("@date", MySqlDbType.DateTime).Value = value.Date;
                _command.Parameters.Add("@comment", MySqlDbType.String).Value = value.Comment;
                _command.Parameters.Add("@transactionType", MySqlDbType.String).Value = value.TransactionType;
                _command.Parameters.Add("@walletID", MySqlDbType.String).Value = value.WalletID;
                _command.Parameters.Add("@transactionId", MySqlDbType.String).Value = value.ID;

                // Recupera o Id do usu�rio cadastrado
                value.ID = (int)_command.ExecuteScalar();

                return true;
            }
        }

        public Transaction Find(object id)
        {
            using (MySqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "SELECT * FROM transaction where wallet_ID = @Id";
                _command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

                using MySqlDataReader reader = _command.ExecuteReader();
                if (reader.Read())
                    return new Transaction
                    {
                        ID = reader.GetInt32(0),
                        Reference = reader.GetString(1),
                        Amount = reader.GetDecimal(2),
                        Date = reader.GetDateTime(3),
                        Comment = reader.GetString(4),
                        TransactionType = reader.GetString(5),
                        WalletID = reader.GetInt32(6),
                        CategoryID = reader.GetInt32(7),
                    };
            }
            return null;
        }

        public Transaction Insert(Transaction value)
        {
            using (MySqlCommand _command = _connection.CreateCommand())
            {
                // cria o comando sql
                _command.CommandText = "INSERT INTO transation (amount, category_ID, reference, date, comment, transactionType, wallet_ID) VALUES (@amount, @categoryID, @reference, @date, @comment, @transactionType, @walletID) SELECT LAST_INSERT_ID();";

                // Adiciona os parametros ao sql
                _command.Parameters.Add("@amount", MySqlDbType.Double).Value = value.Amount;
                _command.Parameters.Add("@categoryID", MySqlDbType.Int32).Value = value.CategoryID;
                _command.Parameters.Add("@reference", MySqlDbType.String).Value = value.Reference;
                _command.Parameters.Add("@date", MySqlDbType.DateTime).Value = value.Date;
                _command.Parameters.Add("@comment", MySqlDbType.String).Value = value.Comment;
                _command.Parameters.Add("@transactionType", MySqlDbType.String).Value = value.TransactionType;
                _command.Parameters.Add("@walletID", MySqlDbType.String).Value = value.WalletID;

                // Recupera o Id do usuario cadastrado
                value.ID = (int)_command.ExecuteScalar();

                return value;
            }

            return null;
        }

        public IEnumerable<Transaction> List()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> ListFiltered(params object[] filter)
        {
            var transactions = new List<Transaction>();
            using (MySqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "SELECT * FROM transaction WHERE transactionType = @type AND wallet_ID = @id";
                _command.Parameters.Add("@type", MySqlDbType.Int32).Value = filter[0];
                _command.Parameters.Add("@id", MySqlDbType.Int32).Value = filter[1];

                using MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    transactions.Add(new Transaction
                    {
                        ID = reader.GetInt32(0),
                        Reference = reader.GetString(1),
                        Amount = reader.GetDecimal(2),
                        Date = reader.GetDateTime(3),
                        Comment = reader.GetString(4),
                        TransactionType = reader.GetString(5),
                        WalletID = reader.GetInt32(6),
                        CategoryID = reader.GetInt32(7),
                    });
                }
                return transactions;
            }
        }
    }
}