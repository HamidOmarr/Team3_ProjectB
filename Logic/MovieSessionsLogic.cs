public class MovieSessionsLogic
{
    private readonly MovieSessionsAccess _movieSessionsAccess;

    public MovieSessionsLogic()
    {
        _movieSessionsAccess = new MovieSessionsAccess();
    }

    public List<MovieSessionModel> GetAllMovieSessions()
    {
        return _movieSessionsAccess.GetAllMovieSessions();
    }

    public List<FullMovieSessionModel> GetDetailedMovieSessionsFromId(long movieId)
    {
        return _movieSessionsAccess.GetDetailedMovieSessionsFromId(movieId);
    }

    public MovieSessionModel GetSessionByMovieAndTime(string movieName, string sessionTime)
    {
        return _movieSessionsAccess.GetSessionByMovieAndTime(movieName, sessionTime);
    }
}
