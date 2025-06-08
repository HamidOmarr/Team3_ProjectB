namespace Team3_ProjectB
{
    public class MoviesLogic
    {
        public List<MovieModel> GetAllMovies()
        {
            MoviesAccess moviesAccess = new MoviesAccess();
            return moviesAccess.GetAllMovies();
        }

        public static void AddMovie(string title, string description, int duration_minutes, DateOnly release_date, string rating, string genre, string language, string subtitle_language)
        {
            MoviesAccess moviesAccess = new MoviesAccess();
            moviesAccess.AddMovie(
                title,
                description,
                duration_minutes,
                        release_date,
                        rating,
                        genre,
                        language,
                        subtitle_language
                    );
        }

        public void UpdateMovie(long id, string title, string description, int duration_minutes, DateOnly release_date, string rating, string genre, string language, string subtitle_language)
        {
            MoviesAccess moviesAccess = new MoviesAccess();
            moviesAccess.UpdateMovie(id, title, description, duration_minutes, release_date, rating, genre, language, subtitle_language);
        }

        public void DeleteMovie(long id)
        {
            MoviesAccess moviesAccess = new MoviesAccess();
            moviesAccess.DeleteMovie(id);
        }
    }
}
