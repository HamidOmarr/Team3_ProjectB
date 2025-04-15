using Microsoft.Data.Sqlite;

using Dapper;

public class MoviesAcces
{

    private static SqliteConnection _connection = new SqliteConnection("Data Source=../../../DataSources/ReservationSysteem.db");

    private static string Table = "movie_session";

    public MoviesAcces()
	{
        _connection.Open();
    }

    public List<MovieModel> GetAllMovies()
    {
        string sql = @"
    SELECT 
        id AS Id,
        title AS Title,
        description AS Description,
        duration_minutes AS DurationMinutes,
        release_date AS ReleaseDate,
        rating AS Rating,
        genre AS Genre,
        languague AS Languague,
        subtitle_language AS SubtitleLanguage
    FROM movie
";

        var Movies = _connection.Query<MovieModel>(sql).ToList();
        return Movies;
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
        a.name AS AuditoriumName
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
