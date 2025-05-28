using Microsoft.Data.Sqlite;

using Dapper;
namespace Team3_ProjectB
{
    public class MoviesAccess
    {

        private static SqliteConnection _connection = new SqliteConnection("Data Source=../../../DataSources/ReservationSysteem.db");

        private static string Table = "movie";

        public MoviesAccess()
        {
            _connection.Open();
        }


        public void AddMovie(string title, string description, int duration_minutes, DateOnly release_date, string rating, string genre, string language, string subtitle_language)
        {
            string sql = $"INSERT INTO {Table} (title, description, duration_minutes, release_date, rating, genre, languague, subtitle_language) VALUES (@title, @description, @duration_minutes, @release_date, @rating, @genre, @language, @subtitle_language);";
            _connection.Execute(sql, new
            {
                title,
                description,
                duration_minutes,
                release_date = release_date.ToString("yyyy-MM-dd"), // Store as string for SQLite
                rating,
                genre,
                language,
                subtitle_language
            });
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
}