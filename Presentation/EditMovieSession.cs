using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3_ProjectB;

namespace Team3_ProjectB.Presentation
{
    public class EditMovieSession
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

            Console.WriteLine("=== All Movie Sessions ===\n");
            foreach (var session in sessions)
            {
                Console.WriteLine($"ID: {session.Id} | Movie: {session.Title} | Start: {session.StartTime} | End: {session.EndTime} | Auditorium: {session.AuditoriumName}");
            }

            Console.Write("\nEnter Session ID to edit: ");
            if (!long.TryParse(Console.ReadLine(), out long sessionId))
            {
                Console.WriteLine("Invalid session ID.");
                Console.ReadKey();
                return;
            }

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
                logic.UpdateMovieSession(sessionId, newStart, newEnd);
                Console.WriteLine("\n✅ Session updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Failed to update session: {ex.Message}");
            }

            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
            NavigationService.GoBack();
        }
    }
}