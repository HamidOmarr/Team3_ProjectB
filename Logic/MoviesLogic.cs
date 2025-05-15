namespace Team3_ProjectB
{
    public class MoviesLogic
{
    public List<MovieModel> GetAllMovies()
    {
        MoviesAccess moviesAccess = new MoviesAccess();
        return moviesAccess.GetAllMovies();
    }
}
}