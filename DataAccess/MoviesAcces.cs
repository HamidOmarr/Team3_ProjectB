using Microsoft.Data.Sqlite;

using Dapper;

public class MoviesAcces
{

    private static SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/Reservationsysteem.db");

    private static string Table = "movie_session";

    public MoviesAcces()
	{
        _connection.Open();
    }
    public List<FullMovieSessionModel> GetAllDetailedMovieSessions()
    {
        string sql = @"
    SELECT 
        ms.id AS Id,
        m.title AS Title,
        m.genre AS Genre,
        m.description AS Description,
        ms.start_time AS StartTime,
        ms.end_time AS EndTime,
        a.name AS AuditoriumName
    FROM movie_session ms
    JOIN movie m ON ms.movie_id = m.id
    JOIN auditorium a ON ms.auditorium_id = a.id
";

        var sessions = _connection.Query<FullMovieSessionModel>(sql).ToList();
        return sessions;
    }

    public List<MovieSessionModel> GetAllMovieSessions()
    {
        string sql = @"
    SELECT 
        ms.id AS Id,
        ms.movie_id AS MovieId,
        ms.auditorium_id AS AuditoriumId,
        ms.start_time AS StartTime,
        ms.end_time AS EndTime,
        m.title AS Title,
        m.genre AS Genre,
        m.description AS Description,
        a.name AS AuditoriumName
    FROM movie_session ms
    JOIN movie m ON ms.movie_id = m.id
    JOIN auditorium a ON ms.auditorium_id = a.id
";
        var movieSessions = _connection.Query<MovieSessionModel>(sql).ToList();
        return movieSessions;
    }
}
