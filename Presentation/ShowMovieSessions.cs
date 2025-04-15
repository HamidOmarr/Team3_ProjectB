public class ShowMoviesManager
{
    public List<MovieSessionModel> MovieSessions { get; private set; }

    public ShowMoviesManager()
    {
        MoviesAcces moviesAccess = new MoviesAcces(); 
        MovieSessions = moviesAccess.GetAllMovieSessions();
    }

    public static int? DisplaySessions(int movieId)
    {
        var moviesAccess = new MoviesAcces();
        var sessions = moviesAccess.GetDetailedMovieSessionsFromId(movieId);

        if (sessions.Count == 0)
        {
            Console.WriteLine("No sessions found.");
            return null;
        }

        int selectedIndex = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine("Use ↑ ↓ to select a session. Press Enter to choose.\n");

            for (int i = 0; i < sessions.Count; i++)
            {
                var session = sessions[i];
                bool isSelected = i == selectedIndex;

                if (isSelected)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine($"[{(isSelected ? ">" : " ")}] {session.StartTime:HH:mm} - {session.EndTime:HH:mm} | {session.Title} ({session.Genre}) - {session.AuditoriumName}");

                if (isSelected)
                    Console.ResetColor();
            }

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                selectedIndex--;
            else if (key == ConsoleKey.DownArrow && selectedIndex < sessions.Count - 1)
                selectedIndex++;

        } while (key != ConsoleKey.Enter);
        SeatSelection.AmountSeatsInput();
        return sessions[selectedIndex].Id;
    }

}
