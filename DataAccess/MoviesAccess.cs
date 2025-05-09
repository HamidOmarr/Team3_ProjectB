using Microsoft.Data.Sqlite;

using Dapper;

public class MoviesAccess
{

    private static SqliteConnection _connection = new SqliteConnection("Data Source=../../../DataSources/ReservationSysteem.db");

    private static string Table = "movie";

    public MoviesAccess()
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


}