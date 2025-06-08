using System;
using System.Collections.Generic;
using Team3_ProjectB;

namespace Team3_ProjectB.Presentation
{
    public class EditMovieSession
    {
        public static void Display()
        {
            Console.Clear();
            LoginStatusHelper.ShowLoginStatus();

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

                    // Customize the session display line
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

            // After selection, proceed with editing as before
            var selectedSession = sessions[selectedIndex];

            Console.Clear();
            LoginStatusHelper.ShowLoginStatus();
            Console.WriteLine($"Editing session: {selectedSession.Title} ({selectedSession.StartTime:yyyy-MM-dd HH:mm} - {selectedSession.EndTime:HH:mm}, {selectedSession.AuditoriumName})");

            Console.Write("Enter new Start Time (yyyy-MM-dd HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime newStart))
            {
                Console.WriteLine("Invalid date format.");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter new End Time (yyyy-MM-dd HH:mm): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime newEnd))
            {
                Console.WriteLine("Invalid date format.");
                Console.ReadKey();
                return;
            }

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
