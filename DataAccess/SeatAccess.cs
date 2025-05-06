using Microsoft.Data.Sqlite;

using Dapper;

public class SeatAccess
{
    private const string ConnectionString = "Data Source=../../../DataSources/ReservationSysteem.db";
    private const string Table = "seat";

    public static SeatsModel GetSeatByRowAndNumber(string row, int seatNumber)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        string sql = $"SELECT id AS Id, auditorium_id AS AuditoriumId, row_number AS RowNumber, seat_number AS SeatNumber, seat_type_id AS SeatTypeId FROM {Table} WHERE row_number = @Row AND seat_number = @SeatNumber";
        return connection.QueryFirstOrDefault<SeatsModel>(sql, new { Row = row, SeatNumber = seatNumber });
    }

    public List<SeatsModel> GetSeatsByAuditorium(int auditoriumId)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();

        string query = @"
        SELECT 
            s.id,
            s.row_number AS RowNumber,
            s.seat_number AS SeatNumber,
            s.seat_type_id AS SeatTypeId,
            p.amount AS Price
        FROM seat s
        INNER JOIN price p ON s.seat_type_id = p.seat_type_id
        WHERE s.auditorium_id = @AuditoriumId
        ORDER BY s.row_number, s.seat_number;
    ";

        return connection.Query<SeatsModel>(query, new { AuditoriumId = auditoriumId }).ToList();
    }

}
