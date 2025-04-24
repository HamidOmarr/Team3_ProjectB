using Dapper;
using Microsoft.Data.Sqlite;

public static class TicketsAccess
{
    private const string ConnectionString = "Data Source=../../../DataSources/ReservationSysteem.db";
    private const string Table = "ticket";

    public static void Create(TicketModel ticket)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        string sql = $"INSERT INTO {Table} (reservation_id, movie_session_id, seat_id, actual_price) VALUES (@ReservationId, @MovieSessionId, @SeatId, @ActualPrice); SELECT last_insert_rowid();";
        ticket.Id = connection.ExecuteScalar<Int64>(sql, ticket);
    }

    public static TicketModel GetById(Int64 id)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        string sql = $"SELECT id AS Id, reservation_id AS ReservationId, movie_session_id AS MovieSessionId, seat_id AS SeatId, actual_price AS ActualPrice FROM {Table} WHERE id = @Id";
        return connection.QueryFirstOrDefault<TicketModel>(sql, new { Id = id });
    }

    public static void Update(TicketModel ticket)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        string sql = $"UPDATE {Table} SET reservation_id = @ReservationId, movie_session_id = @MovieSessionId, seat_id = @SeatId, actual_price = @ActualPrice WHERE id = @Id";
        connection.Execute(sql, ticket);
    }

    public static void Delete(Int64 id)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        connection.Execute(sql, new { Id = id });
    }
}
