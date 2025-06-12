using System;
using System.Collections.Generic;
using Team3_ProjectB;

namespace Team3_ProjectB.Presentation
{
    public class EditMovie
    {
        public static void Display()
        {
            Console.Clear();
            LoginStatusHelper.ShowLoginStatus();

            var logic = new MoviesLogic();
            var movies = logic.GetAllMovies();

            if (movies.Count == 0)
            {
                Console.WriteLine("❌ No movies found.");
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
                Console.WriteLine("Use ↑ ↓ to select a movie. Press Enter to edit. Press Backspace to go back.\n");

                for (int i = 0; i < movies.Count; i++)
                {
                    var movie = movies[i];
                    bool isSelected = (i == selectedIndex);

                    if (isSelected)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine($"[{(isSelected ? ">" : " ")}] {movie.Title} ({movie.ReleaseDate:yyyy-MM-dd}) | {movie.Genre}");

                    if (isSelected)
                        Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                    selectedIndex--;
                else if (key == ConsoleKey.DownArrow && selectedIndex < movies.Count - 1)
                    selectedIndex++;
                else if (key == ConsoleKey.Backspace)
                {
                    NavigationService.GoBack();
                    return;
                }

            } while (key != ConsoleKey.Enter);

            var selectedMovie = movies[selectedIndex];

            Console.Clear();
            LoginStatusHelper.ShowLoginStatus();
            Console.WriteLine($"Editing movie: {selectedMovie.Title} ({selectedMovie.ReleaseDate:yyyy-MM-dd}, {selectedMovie.Genre})\n");

            Console.Write($"Title ({selectedMovie.Title}): ");
            var title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title)) title = selectedMovie.Title;

            Console.Write($"Description ({selectedMovie.Description}): ");
            var description = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(description)) description = selectedMovie.Description;

            Console.Write($"Duration in minutes ({selectedMovie.DurationMinutes}): ");
            var durationInput = Console.ReadLine();
            int duration;
            if (string.IsNullOrWhiteSpace(durationInput) || !int.TryParse(durationInput, out duration)) duration = selectedMovie.DurationMinutes;

            Console.Write($"Release date ({selectedMovie.ReleaseDate:yyyy-MM-dd}): ");
            var releaseDateInput = Console.ReadLine();
            DateOnly releaseDate;
            if (string.IsNullOrWhiteSpace(releaseDateInput) || !DateOnly.TryParse(releaseDateInput, out releaseDate))
                releaseDate = selectedMovie.ReleaseDate;



            Console.Write($"Rating ({selectedMovie.Rating}): ");
            var rating = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(rating)) rating = selectedMovie.Rating;

            Console.Write($"Genre ({selectedMovie.Genre}): ");
            var genre = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(genre)) genre = selectedMovie.Genre;

            Console.Write($"Language ({selectedMovie.Language}): ");
            var language = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(language)) language = selectedMovie.Language;

            Console.Write($"Subtitle language ({selectedMovie.SubtitleLanguage}): ");
            var subtitleLanguage = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(subtitleLanguage)) subtitleLanguage = selectedMovie.SubtitleLanguage;

            try
            {
                logic.UpdateMovie(selectedMovie.Id, title, description, duration, releaseDate, rating, genre, language, subtitleLanguage);



                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nMovie updated successfully!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nFailed to update movie: {ex.Message}");
            }

            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
            NavigationService.GoBack();
        }
    }
}
