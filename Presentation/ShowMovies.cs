﻿using Team3_ProjectB;

public class ShowMovies
{
    public List<MovieModel> Movies { get; private set; }

    public ShowMovies()
    {
        MoviesLogic moviesLogic = new MoviesLogic();
        Movies = moviesLogic.GetAllMovies();
    }

    protected static void DisplaySessionsCore(Action<MovieModel> onMovieSelected)
    {
        ShowMovies showMovies = new ShowMovies();
        var sessions = showMovies.Movies;

        if (sessions.Count == 0)
        {
            Console.WriteLine("No movies available.");
            Console.WriteLine("Press any key to go back...");
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

            Console.WriteLine("Use ↑ and ↓ to select a movie. Press Enter to confirm or Backspace to go back.\n");

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
            Console.WriteLine($"Language: {selected.Language}");
            Console.WriteLine($"Subtitle Language: {selected.SubtitleLanguage}");
            Console.WriteLine($"ReleaseDate: {selected.ReleaseDate}");

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

        var selectedMovie = sessions[selectedIndex];
        Console.Clear();

        onMovieSelected(selectedMovie);
    }

    public static void DisplaySessions()
    {
        DisplaySessionsCore(selectedMovie =>
        {
            NavigationService.Navigate(() => ShowMoviesManager.DisplaySessions(selectedMovie.Id));
        });
    }
}