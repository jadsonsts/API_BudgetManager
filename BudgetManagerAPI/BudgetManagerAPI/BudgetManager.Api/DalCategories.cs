using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Drawing.Drawing2D;
using MySql.Data.MySqlClient;

namespace BudgetManager.Api 
{
    public class DalCategories : IDalCategories
    {
        private IConnection _connection;
        public DalCategories(IConnection connection)
        {
            _connection = connection;
        }

        public bool Delete(Categories value)
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

        public bool Edit(Categories value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Consulta uma categoria pelo seu Id
        /// </summary>
        /// <param name="categoryID">Id da Catetgoria</param>
        /// <returns></returns>
        public Categories Find(object categoryID)
        {
            Categories categories = null;
            using (MySqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "SELECT category_ID, name, iconName, color FROM categories where isActive = 1  and category_ID = @id";
                _command.Parameters.Add(new MySqlParameter("@id", (int)categoryID));

                using MySqlDataReader reader = _command.ExecuteReader();
                if (reader.Read())
                {
                    categories = new Categories
                    {
                        CategoryID = reader.GetInt32(0),
                        categoryName = reader.GetString(1),
                        iconName = reader.GetString(2),
                        color = reader.GetString(3),
                    };
                }
            }
            return categories;
        }

        public Categories Insert(Categories value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lista todas as categorias ativas
        /// </summary>
        /// <returns>Lista de categorias ativas</returns>
        public IEnumerable<Categories> List()
        {
            var categories = new List<Categories>();
            using (MySqlCommand _command = _connection.CreateCommand())
            {
                _command.CommandText = "SELECT category_ID, name, iconName, color FROM categories where isActive = 1";
                using MySqlDataReader reader = _command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        categories.Add(new Categories
                        {
                            CategoryID = reader.GetInt32(0),
                            categoryName = reader.GetString(1),
                            iconName = reader.GetString(2),
                            color = reader.GetString(3),
                        });
                    }
                }
            }
            return categories;
        }

        public IEnumerable<Categories> ListFiltered(params object[] filter)
        {
            throw new NotImplementedException();
        }
    }
}