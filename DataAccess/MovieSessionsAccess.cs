using Dapper;
using Microsoft.Data.Sqlite;

public static class MovieSessionsAccess
{
    private const string ConnectionString = "Data Source=../../../DataSources/ReservationSysteem.db";
    private const string Table = "movie_session";

    public static MovieSessionModel GetSessionByMovieAndTime(string movieName, string sessionTime)
    {
        using var connection = new SqliteConnection(ConnectionString);
        connection.Open();
        string sql = $@"
            SELECT ms.id AS Id, ms.movie_id AS MovieId, ms.auditorium_id AS AuditoriumId, ms.start_time AS StartTime, ms.end_time AS EndTime
            FROM {Table} ms
            JOIN movie m ON ms.movie_id = m.id
            WHERE m.title = @MovieName AND ms.start_time = @SessionTime";
        return connection.QueryFirstOrDefault<MovieSessionModel>(sql, new { MovieName = movieName, SessionTime = sessionTime });
    }
}
