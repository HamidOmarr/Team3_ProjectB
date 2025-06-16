namespace Team3_ProjectB.Presentation
{
    public class DeleteMovieSession
    {
        public static void Display()
        {
            Console.Clear();
            var logic = new MovieSessionsLogic();
            var sessions = logic.GetAllDetailedMovieSessions();

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
                Console.WriteLine("Use ↑ ↓ to select a session. Press Enter to delete. Press Backspace to go back.\n");

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

            Console.Clear();
            LoginStatusHelper.ShowLoginStatus();
            Console.WriteLine($"Delete session: {selectedSession.Title} ({selectedSession.StartTime:yyyy-MM-dd HH:mm} - {selectedSession.EndTime:HH:mm}, {selectedSession.AuditoriumName})");
            Console.Write("Are you sure you want to delete this session? (y/n): ");
            var confirm = Console.ReadLine();

            if (confirm?.ToLower() != "y")
            {
                Console.WriteLine("Deletion canceled.");
                Console.ReadKey();
                return;
            }

            try
            {
                logic.DeleteMovieSession(selectedSession.Id);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nSession deleted successfully!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nFailed to delete session: {ex.Message}");
            }

            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
            NavigationService.GoBack();
        }
    }
}
