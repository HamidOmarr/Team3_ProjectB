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
                Console.WriteLine($"Movie duration: {selectedMovie.DurationMinutes} minutes (+30 min cleanup)\n");

                try
                {
                    MovieSessionsLogic logic = new MovieSessionsLogic();
                    var allSessions = logic.GetAllMovieSessions()
                        .OrderByDescending(s => s.StartTime)
                        .ToList();

                    if (allSessions.Count > 0)
                    {
                        Console.WriteLine("Latest sessions in all auditoriums:");
                        foreach (var session in allSessions.Take(5))
                        {
                            Console.WriteLine(
                                $"- {session.StartTime:yyyy-MM-dd HH:mm} to {session.EndTime.AddMinutes(30):HH:mm} | Auditorium: {session.AuditoriumId}");
                        }
                        Console.WriteLine();
                    }

                    Console.Write("Enter Auditorium number: ");
                    AuditoriumId = int.Parse(Console.ReadLine());

                    var sessionsInAuditorium = allSessions
                        .Where(s => s.AuditoriumId == AuditoriumId)
                        .OrderByDescending(s => s.StartTime)
                        .ToList();

                    if (sessionsInAuditorium.Count > 0)
                    {
                        Console.WriteLine($"\nLatest sessions in Auditorium {AuditoriumId}:");
                        foreach (var session in sessionsInAuditorium.Take(3))
                        {
                            Console.WriteLine(
                                $"- {session.StartTime:yyyy-MM-dd HH:mm} to {session.EndTime.AddMinutes(30):HH:mm}");
                        }
                        Console.WriteLine();
                    }

                    Console.Write("Enter Start Time (yyyy-MM-dd HH:mm): ");
                    StartTime = DateTime.Parse(Console.ReadLine());

                    if (StartTime < DateTime.Now)
                    {
                        Console.WriteLine("\nCannot create a session in the past.");
                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                        NavigationService.GoBack();
                        return;
                    }

                    int durationMinutes = selectedMovie.DurationMinutes;
                    EndTime = StartTime.AddMinutes(durationMinutes + 30);

                    Console.WriteLine($"Calculated End Time (including cleanup): {EndTime:yyyy-MM-dd HH:mm}");

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
                        NavigationService.GoBack();
                        return;
                    }

                    logic.AddMovieSession(selectedMovie.Id, AuditoriumId, StartTime, EndTime);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nMovie session added successfully!");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nFailed to add movie session: {ex.Message}");
                }

                Console.WriteLine("Press any key to return...");
                Console.ReadKey();
                NavigationService.GoBack();
            });
        }
    }
}
