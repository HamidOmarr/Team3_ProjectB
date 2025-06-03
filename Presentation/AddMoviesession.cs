using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3_ProjectB;


namespace Team3_ProjectB.Presentation
{
    public class AddMoviesession : ShowMovies
    {
        public static int AuditoriumId;
        public static DateTime StartTime;
        public static DateTime EndTime;

        public new static void DisplaySessions()
        {
            DisplaySessionsCore(selectedMovie =>
            {
                Console.Clear();
                Console.WriteLine($"Selected movie: {selectedMovie.Title}");

                try
                {
                    Console.Write("Enter Auditorium ID: ");
                    AuditoriumId = int.Parse(Console.ReadLine());

                    Console.Write("Enter Start Time (yyyy-MM-dd HH:mm): ");
                    StartTime = DateTime.Parse(Console.ReadLine());

                    Console.Write("Enter End Time (yyyy-MM-dd HH:mm): ");
                    EndTime = DateTime.Parse(Console.ReadLine());

                    MovieSessionsAccess access = new MovieSessionsAccess();
                    access.AddMovieSession(selectedMovie.Id, AuditoriumId, StartTime, EndTime);

                    Console.WriteLine("\n✅ Movie session added successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Failed to add movie session: {ex.Message}");
                }

                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
                NavigationService.Navigate(Menu.Start);
            });
        }
    }
}