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
}
