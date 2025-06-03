using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3_ProjectB;

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

            Console.WriteLine("=== All Movie Sessions ===\n");
            foreach (var session in sessions)
            {
                Console.WriteLine($"ID: {session.Id} | Movie: {session.Title} | Start: {session.StartTime} | End: {session.EndTime} | Auditorium: {session.AuditoriumName}");
            }

            Console.Write("\nEnter Session ID to delete: ");
            if (!long.TryParse(Console.ReadLine(), out long sessionId))
            {
                Console.WriteLine("Invalid session ID.");
                Console.ReadKey();
                return;
            }

            Console.Write("Are you sure you want to delete this session? (y/n): ");
            var confirm = Console.ReadLine();
            if (confirm?.ToLower() != "y")
            {
                Console.WriteLine("❌ Deletion canceled.");
                Console.ReadKey();
                return;
            }

            try
            {
                logic.DeleteMovieSession(sessionId);
                Console.WriteLine("\n✅ Session deleted successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Failed to delete session: {ex.Message}");
            }

            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
            NavigationService.GoBack();
        }
    }
}
