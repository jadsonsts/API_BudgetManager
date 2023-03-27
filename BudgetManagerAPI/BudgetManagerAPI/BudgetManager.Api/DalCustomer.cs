using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Drawing.Drawing2D;
using MySql.Data.MySqlClient;

namespace BudgetManager.Api
{
    public class DalCustomer : IDalCustomer
    {
        private IConnection _connection;

        public DalCustomer(IConnection connection)
        {
            _connection = connection;
        }

        public bool Delete(Customer value)
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

        public bool Edit(Customer value)
        {
            using (MySqlCommand _command = _connection.CreateCommand())
            {
                //create sql instruction
                _command.CommandText = @"UPDATE customer
                                        SET name = @name
                                          , familyName = @familyName
                                          , email = @email
                                          , phone = @phone
                                          , profilePicture = @profilePicture
                                          WHERE customer_ID = @customer_ID";
               //add parameters
               _command.Parameters.Add("@name",MySqlDbType.String).Value = value.Name;                           
               _command.Parameters.Add("@familyName", MySqlDbType.String).Value = value.FamilyName;                           
               _command.Parameters.Add("@email", MySqlDbType.String).Value = value.Email;                           
               _command.Parameters.Add("@phone", MySqlDbType.String).Value = value.Phone;                           
               _command.Parameters.Add("@profilePicture", MySqlDbType.String).Value = value.ProfilePicture;
               _command.Parameters.Add("@customer_ID",MySqlDbType.Int32).Value = value.Id;
                
                
                using (var reader = _command.ExecuteReader())
                {
                    if (reader.Read())
                    value.Id = (int)reader.GetInt32(0);
                }

               return true;                           
            }
        }

        public Customer Find(object firebaseID)
        {
            Customer customer = null;
            using (MySqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "SELECT customer_ID, firebase_ID, name, familyName, phone, email, profilePicture, isActive FROM customer WHERE firebase_ID=@Id";
                _command.Parameters.Add("@Id", MySqlDbType.String).Value = firebaseID;
                using (MySqlDataReader reader = _command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();
                        customer = new Customer
                        {
                            Id = reader.GetInt32(0),
                            firebaseID = reader.GetString(1),
                            Name = reader.GetString(2),
                            FamilyName = reader.GetString(3),
                            Phone = reader.GetString(4),
                            Email = reader.GetString(5),
                            ProfilePicture = reader.IsDBNull(6) ? "" : reader.GetString(6),
                            isActive = reader.GetBoolean(7),

                        };
                    }
                }
            }
            return customer;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Customer Insert(Customer value)
        {
            using (MySqlCommand _command = _connection.CreateCommand())
            {
                // cria o comando sql
                _command.CommandText = "INSERT INTO customer (firebase_ID, name, familyName, phone, email, profilePicture, isActive) VALUES (@firebaseID, @name, @familyName, @phone, @email, @profilePicture, @isActive); SELECT LAST_INSERT_ID();";

                // Adiciona os parametros ao sql
                _command.Parameters.Add("@firebaseId", MySqlDbType.String).Value = value.firebaseID;
                _command.Parameters.Add("@name", MySqlDbType.String).Value = value.Name;
                _command.Parameters.Add("@familyName", MySqlDbType.String).Value = value.FamilyName;
                _command.Parameters.Add("@phone", MySqlDbType.String).Value = value.Phone;
                _command.Parameters.Add("@email", MySqlDbType.String).Value = value.Email;
                _command.Parameters.Add("@profilePicture", MySqlDbType.String).Value = value.ProfilePicture;
                _command.Parameters.Add("@isActive",MySqlDbType.Bit).Value = value.isActive;

                // Recupera o Id do usuário cadastrado
                using (var reader = _command.ExecuteReader())
                {
                    if (reader.Read())
                        value.Id = (int)reader.GetInt32(0);
                }

                return value;
            }

            return null;
        }

        public IEnumerable<Customer> List()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> ListFiltered(params object[] filter)
        {
            throw new NotImplementedException();
        }
    }
}
