public class MovieSessionsLogic
{
    public List<MovieSessionModel> GetAllMovieSessions()
    {
        return MovieSessionsAccess.GetAllMovieSessions();
    }

    public List<FullMovieSessionModel> GetDetailedMovieSessionsFromId(long movieId)
    {
        return MovieSessionsAccess.GetDetailedMovieSessionsFromId(movieId);
    }
}
