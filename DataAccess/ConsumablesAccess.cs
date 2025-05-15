using Dapper;
using Microsoft.Data.Sqlite;
namespace Team3_ProjectB
{
    public class ConsumablesAccess
    {
        private const string ConnectionString = "Data Source=../../../DataSources/ReservationSysteem.db";
        private const string Table = "consumable";

        public List<ConsumableModel> GetAllConsumables()
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            string sql = $"SELECT id AS Id, name AS Name, price AS Price, category AS Category FROM {Table}";
            return connection.Query<ConsumableModel>(sql).ToList();
        }
    }
}