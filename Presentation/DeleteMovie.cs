using System;
using System.Collections.Generic;
using Team3_ProjectB;

namespace Team3_ProjectB.Presentation
{
    public class DeleteMovie
    {
        public static void Display()
        {
            Console.Clear();
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
                Console.WriteLine("Use ↑ ↓ to select a movie. Press Enter to delete. Press Backspace to go back.\n");

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
            Console.WriteLine($"Delete movie: {selectedMovie.Title} ({selectedMovie.ReleaseDate:yyyy-MM-dd}, {selectedMovie.Genre})");
            Console.Write("Are you sure you want to delete this movie? (y/n): ");
            var confirm = Console.ReadLine();

            if (confirm?.ToLower() != "y")
            {
                Console.WriteLine("Deletion canceled.");
                Console.ReadKey();
                return;
            }

            try
            {
                logic.DeleteMovie(selectedMovie.Id);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nMovie deleted successfully!");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("FOREIGN KEY constraint failed"))
                {
                    Console.WriteLine("\n❌ This movie cannot be deleted because there are sessions or other references.");
                }
                else
                {
                    Console.WriteLine($"\nFailed to delete movie: {ex.Message}");
                }
            }

            Console.WriteLine("Press any key to return...");
            Console.ReadKey();
            NavigationService.GoBack();
        }
    }
}
