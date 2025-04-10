public class ShowMoviesManager
{
    public List<MovieSessionModel> MovieSessions { get; private set; }

    public ShowMoviesManager()
    {
        MoviesAcces moviesAccess = new MoviesAcces(); 
        MovieSessions = moviesAccess.GetAllMovieSessions();
    }

    public static void DisplaySessions()
    {
        ShowMoviesManager showMovies = new ShowMoviesManager();
        Console.WriteLine("Available Movie Sessions:");
        var moviesAccess = new MoviesAcces();
        var sessions = moviesAccess.GetAllDetailedMovieSessions();

        foreach (var session in sessions)
        {
            Console.WriteLine("──────────────────────────────");
            Console.WriteLine($"Title: {session.Title}");
            Console.WriteLine($"Genre: {session.Genre}");
            Console.WriteLine($"Description: {session.Description}");
            Console.WriteLine($"Time: {session.StartTime} - {session.EndTime}");
            Console.WriteLine($"Auditorium: {session.AuditoriumName}");
            Console.WriteLine($"Session ID: {session.Id}");
        }

        Console.WriteLine("Enter Session ID to select:");
        int sessionId = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine($"You have selected session {sessionId}. Seat selection will be added and demonstrated in our next meeting!");

        Console.WriteLine($"Proceeding to payment...");
        ReservationSystem.ProcessPayment();
    }
}
