using System;
using System.Collections.Generic;

namespace Team3_ProjectB
{
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
                Console.Clear();
                LoginStatusHelper.ShowLoginStatus();

                Console.WriteLine("No sessions found.");
                Console.WriteLine("Press any key to go back...");
                Console.ReadKey();
                NavigationService.GoBack();
                return null;
            }

            int selectedIndex = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                LoginStatusHelper.ShowLoginStatus();

                Console.WriteLine("Use ↑ ↓ to select a session. Press Enter to choose. Press Backspace to go back.");

                for (int i = 0; i < sessions.Count; i++)
                {
                    var session = sessions[i];
                    bool isSelected = i == selectedIndex;
                    if (isSelected)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine($"[{(isSelected ? ">" : " ")}] {session.StartTime:yyyy-MM-dd HH:mm} - {session.EndTime:yyyy-MM-dd HH:mm} | {session.MovieModel.Title} ({session.MovieModel.Genre}) - {session.Auditorium.Name}");

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
                    return null;
                }

            } while (key != ConsoleKey.Enter);

            var selectedSession = sessions[selectedIndex];

            Console.Clear();
            LoginStatusHelper.ShowLoginStatus();

            NavigationService.Navigate(() =>
                SeatSelection.AmountSeatsInput(
                    selectedSession.AuditoriumId,
                    selectedSession.MovieModel.Title,
                    selectedSession.StartTime.ToString("yyyy-MM-dd HH:mm"),
                    0,
                    selectedSession.Id
                )
            );

            return sessions[selectedIndex].Id;
        }
    }
}
