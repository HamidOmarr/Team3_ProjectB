using Dapper;
using Microsoft.Data.Sqlite;

public class MovieSessionsAccess
{
    private readonly SqliteConnection _connection;

    public MovieSessionsAccess()
    {
        _connection = new SqliteConnection("Data Source=../../../DataSources/ReservationSysteem.db");
        _connection.Open();
    }

    private const string Table = "movie_session";

    public MovieSessionModel GetSessionByMovieAndTime(string movieName, string sessionTime)
    {
        string sql = $@"
            SELECT ms.id AS Id, ms.movie_id AS MovieId, ms.auditorium_id AS AuditoriumId, ms.start_time AS StartTime, ms.end_time AS EndTime
            FROM {Table} ms
            JOIN movie m ON ms.movie_id = m.id
            WHERE m.title = @MovieName AND ms.start_time = @SessionTime";
        return _connection.QueryFirstOrDefault<MovieSessionModel>(sql, new { MovieName = movieName, SessionTime = sessionTime });
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

    public List<FullMovieSessionModel> GetDetailedMovieSessionsFromId(long movieId)
    {
        string sql = @"
    SELECT 
        ms.id AS Id,
        m.title AS Title,
        m.genre AS Genre,
        m.description AS Description,
        ms.start_time AS StartTime,
        ms.end_time AS EndTime,
        a.name AS AuditoriumName,
        ms.auditorium_id AS AuditoriumId
    FROM movie_session ms
    JOIN movie m ON ms.movie_id = m.id
    JOIN auditorium a ON ms.auditorium_id = a.id
    WHERE ms.movie_id = @movieId
    ";

        var sessions = _connection.Query<FullMovieSessionModel>(sql, new { movieId }).ToList();
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
