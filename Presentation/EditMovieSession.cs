namespace Team3_ProjectB.Presentation
{
    public class EditMovieSession
    {
        public static void Display()
        {
            Console.Clear();
            LoginStatusHelper.ShowLoginStatus();

            var logic = new MovieSessionsLogic();
            var moviesLogic = new MoviesLogic();
            var sessions = logic.GetAllDetailedMovieSessions()
                                .OrderByDescending(s => s.StartTime)
                                .ToList();

            if (sessions.Count == 0)
            {
                Console.WriteLine("❌ No movie sessions found.");
                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
                NavigationService.GoBack();
                return;
            }

            int selectedIndex = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                LoginStatusHelper.ShowLoginStatus();
                Console.WriteLine("Use ↑ ↓ to select a session. Press Enter to edit. Press Backspace to go back.\n");

                for (int i = 0; i < sessions.Count; i++)
                {
                    var session = sessions[i];
                    bool isSelected = (i == selectedIndex);

                    if (isSelected)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine($"[{(isSelected ? ">" : " ")}] {session.StartTime:yyyy-MM-dd HH:mm} - {session.EndTime:HH:mm} | {session.Title} - {session.AuditoriumName}");

                    if (isSelected)
                        Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                    selectedIndex--;
                else if (key == ConsoleKey.DownArrow && selectedIndex < sessions.Count - 1)
                    selectedIndex++;
                else if (key == ConsoleKey.Backspace)
                {
                    NavigationService.GoBack();
                    return;
                }

            } while (key != ConsoleKey.Enter);

            var selectedSession = sessions[selectedIndex];
            var movie = moviesLogic.GetAllMovies().FirstOrDefault(m => m.Title == selectedSession.Title);

            if (movie == null)
            {
                Console.WriteLine("Movie not found for this session.");
                Console.ReadKey();
                NavigationService.GoBack();
                return;
            }

            int movieDuration = movie.DurationMinutes;
            Console.Clear();
            LoginStatusHelper.ShowLoginStatus();
            Console.WriteLine($"Editing session: {selectedSession.Title} ({selectedSession.StartTime:yyyy-MM-dd HH:mm} - {selectedSession.EndTime:HH:mm}, {selectedSession.AuditoriumName})");
            Console.WriteLine($"Movie duration: {movieDuration} minutes (+30 min cleanup)\n");

            Console.Write("Enter new Start Time (yyyy-MM-dd HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime newStart))
            {
                Console.WriteLine("Invalid date format.");
                Console.ReadKey();
                return;
            }

            DateTime newEnd = newStart.AddMinutes(movieDuration + 30);
            Console.WriteLine($"Calculated End Time: {newEnd:yyyy-MM-dd HH:mm}");

            try
            {
                logic.UpdateMovieSession(selectedSession.Id, newStart, newEnd);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nSession updated successfully!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nFailed to update session: {ex.Message}");
            }

            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
            NavigationService.GoBack();
        }
    }
}
