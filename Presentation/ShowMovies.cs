﻿public class ShowMovies
{
    public List<MovieModel> Movies { get; private set; }

    public ShowMovies()
    {
        MoviesLogic moviesLogic = new MoviesLogic();
        Movies = moviesLogic.GetAllMovies();
    }

    public static void DisplaySessions()
    {
        ShowMovies showMovies = new ShowMovies();
        var sessions = showMovies.Movies;

        if (sessions.Count == 0)
        {
            Console.WriteLine("No movies available.");
            return;
        }

        int selectedIndex = 0;
        ConsoleKey key;

        do
        {
            Console.Clear();
            Console.WriteLine("Use ↑ ↓ to choose a movie, then press Enter:\n");

            for (int i = 0; i < sessions.Count; i++)
            {
                var s = sessions[i];
                bool isSelected = i == selectedIndex;

                if (isSelected)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine($"[>] {s.Title}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"[ ] {s.Title}");
                }
            }

            var selected = sessions[selectedIndex];
            Console.WriteLine("\n──────────────────────────────");
            Console.WriteLine($"Title: {selected.Title}");
            Console.WriteLine($"Description: {selected.Description}");
            Console.WriteLine($"Genre: {selected.Genre}");
            Console.WriteLine($"Duration: {selected.DurationMinutes}");
            Console.WriteLine($"Rating: {selected.Rating}");
            Console.WriteLine($"Language: {selected.Languague}");
            Console.WriteLine($"Subtitle Language: {selected.SubtitleLanguage}");
            Console.WriteLine($"ReleaseDate: {selected.ReleaseDate}");

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                selectedIndex--;
            else if (key == ConsoleKey.DownArrow && selectedIndex < sessions.Count - 1)
                selectedIndex++;

        } while (key != ConsoleKey.Enter);

        var selectedMovie = sessions[selectedIndex];
        Console.Clear();

        ShowMoviesManager.DisplaySessions(selectedMovie.Id);
    }
}
