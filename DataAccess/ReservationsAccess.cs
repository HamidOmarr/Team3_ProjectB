using Dapper;
using Microsoft.Data.Sqlite;

public static class ReservationsAccess
{
    private const string ConnectionString = "Data Source=../../../DataSources/ReservationSysteem.db";
    private const string Table = "reservation";

    public static void Create(ReservationModel reservation)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        string sql = $"INSERT INTO {Table} (user_id, total_price, status) VALUES (@UserId, @TotalPrice, @Status); SELECT last_insert_rowid();";
        reservation.Id = connection.ExecuteScalar<Int64>(sql, reservation);
    }

    public static ReservationModel GetById(Int64 id)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        string sql = $"SELECT id AS Id, user_id AS UserId, total_price AS TotalPrice, status AS Status FROM {Table} WHERE id = @Id";
        return connection.QueryFirstOrDefault<ReservationModel>(sql, new { Id = id });
    }

    public static void Update(ReservationModel reservation)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        string sql = $"UPDATE {Table} SET user_id = @UserId, total_price = @TotalPrice, status = @Status WHERE id = @Id";
        connection.Execute(sql, reservation);
    }

    public static void Delete(Int64 id)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        string sql = $"DELETE FROM {Table} WHERE id = @Id";
        connection.Execute(sql, new { Id = id });
    }
}
