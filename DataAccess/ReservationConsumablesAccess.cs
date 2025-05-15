using Dapper;
using Microsoft.Data.Sqlite;
namespace Team3_ProjectB
{
    public class ReservationConsumablesAccess
    {
        private const string ConnectionString = "Data Source=../../../DataSources/ReservationSysteem.db";

        public void SaveReservationConsumable(ReservationConsumableModel reservationConsumable)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            string sql = @"
            INSERT INTO reservation_consumable (reservation_id, consumable_id, quantity, actual_price)
            VALUES (@ReservationId, @ConsumableId, @Quantity, @ActualPrice)";
            connection.Execute(sql, reservationConsumable);
        }

        public List<(string Name, int Quantity, decimal ActualPrice)> GetConsumablesForCheckout(long reservationId)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            string sql = @"
            SELECT 
                c.name AS Name,
                rc.quantity AS Quantity,
                rc.actual_price AS ActualPrice
            FROM reservation_consumable rc
            JOIN consumable c ON rc.consumable_id = c.id
            WHERE rc.reservation_id = @ReservationId";
            return connection.Query<(string Name, int Quantity, decimal ActualPrice)>(sql, new { ReservationId = reservationId }).ToList();
        }
    }
}