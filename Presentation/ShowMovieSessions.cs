public class ShowMoviesManager
{
    public List<MovieSessionModel> MovieSessions { get; private set; }

    public ShowMoviesManager()
    {
        MovieSessionsLogic movieSessionsLogic = new MovieSessionsLogic();
        MovieSessions = movieSessionsLogic.GetAllMovieSessions();
    }

    public static int? DisplaySessions(int movieId)
    {
        MovieSessionsLogic movieSessionsLogic = new MovieSessionsLogic();
        var sessions = movieSessionsLogic.GetDetailedMovieSessionsFromId(movieId);

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

        var selectedSession = sessions[selectedIndex];
        SeatSelection.AmountSeatsInput(
            selectedSession.AuditoriumId,
            selectedSession.Title,
            selectedSession.StartTime
        );
        return sessions[selectedIndex].Id;
    }
}
