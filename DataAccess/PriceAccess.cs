using Dapper;
using Microsoft.Data.Sqlite;

public static class PriceAccess
{
    private const string ConnectionString = "Data Source=../../../DataSources/ReservationSysteem.db";
    private const string Table = "price";

    public static decimal GetPrice(int seatTypeId, int? promotionTypeId)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        string sql = $@"
            SELECT amount 
            FROM {Table} 
            WHERE seat_type_id = @SeatTypeId 
            AND (promotion_type_id = @PromotionTypeId OR @PromotionTypeId IS NULL)";

        return connection.QueryFirstOrDefault<decimal>(sql, new { SeatTypeId = seatTypeId, PromotionTypeId = promotionTypeId });
    }
}
