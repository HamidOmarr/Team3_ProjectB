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
                    Console.Write("Enter Auditorium number: ");
                    AuditoriumId = int.Parse(Console.ReadLine());

                    Console.Write("Enter Start Time (yyyy-MM-dd HH:mm): ");
                    StartTime = DateTime.Parse(Console.ReadLine());

                    Console.Write("Enter Duration in minutes: ");
                    int durationMinutes = int.Parse(Console.ReadLine());

                    if (StartTime < DateTime.Now)
                    {
                        Console.WriteLine("\nCannot create a session in the past.");
                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                        NavigationService.Navigate(Menu.Start);
                        return;
                    }

                    EndTime = StartTime.AddMinutes(durationMinutes);

                    MovieSessionsLogic logic = new MovieSessionsLogic();
                    var allSessions = logic.GetAllMovieSessions();

                    var sessionsInAuditorium = allSessions.Where(s => s.AuditoriumId == AuditoriumId);

                    bool overlap = sessionsInAuditorium.Any(s =>
                    {
                        DateTime existingStart = s.StartTime;
                        DateTime existingEnd = s.EndTime.AddMinutes(30);

                        return StartTime < existingEnd && EndTime > existingStart;
                    });

                    if (overlap)
                    {
                        Console.WriteLine("\nCannot create session: There is already a session (or cleaning time) in that auditorium at the selected time.");
                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                        NavigationService.Navigate(Menu.Start);
                        return;
                    }

                    logic.AddMovieSession(selectedMovie.Id, AuditoriumId, StartTime, EndTime);

                    Console.WriteLine("\nMovie session added successfully!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nFailed to add movie session: {ex.Message}");
                }

                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
                NavigationService.Navigate(Menu.Start);
            });
        }


    }
}