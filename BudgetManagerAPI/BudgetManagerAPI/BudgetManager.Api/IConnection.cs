using MySql.Data.MySqlClient;

namespace BudgetManager.Api
{
    public interface IConnection : IDisposable
    {
        MySqlConnection Connect { get; }
        MySqlConnection Open();
        void Close();
        MySqlCommand CreateCommand();
    }
}
