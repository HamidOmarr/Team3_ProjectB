using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team3_ProjectB;

namespace Team3_ProjectB.Presentation
{
    public class AddMovie
    {



        public static void AddMovieMethod()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Fill in the movie information to add a movie (press Backspace before typing to go back).\n");

                string Title = ReadInputWithBackNavigation("Enter the movie title:");

                string Description = ReadInputWithBackNavigation("Enter the movie description:");

                string DurationMinutes;
                while (true)
                {
                    DurationMinutes = ReadInputWithBackNavigation("Enter the movie duration in minutes:");
                    if (int.TryParse(DurationMinutes, out int duration) && duration > 0)
                        break;
                    Console.WriteLine("Invalid duration. Please enter a positive integer.");
                }

                string ReleaseDate;
                while (true)
                {
                    ReleaseDate = ReadInputWithBackNavigation("Enter the movie release date (YYYY-MM-DD):");
                    if (DateOnly.TryParse(ReleaseDate, out _))
                        break;
                    Console.WriteLine("Invalid date format. Please enter the date as YYYY-MM-DD.");
                }

                string Rating = ReadInputWithBackNavigation("Enter the movie rating:");
                string Genre = ReadInputWithBackNavigation("Enter the movie genre:");
                string Language = ReadInputWithBackNavigation("Enter the movie language:");
                string SubtitleLanguage = ReadInputWithBackNavigation("Enter the movie subtitle language:");

                // Now parse and add the movie
                MoviesAccess moviesAccess = new MoviesAccess();
                moviesAccess.AddMovie(
                    Title,
                    Description,
                    int.Parse(DurationMinutes),
                    DateOnly.Parse(ReleaseDate),
                    Rating,
                    Genre,
                    Language,
                    SubtitleLanguage
                );

                Console.WriteLine("Movie added successfully! Press any key to return.");
                Console.ReadKey();
                NavigationService.GoBack();
            }
            catch (OperationCanceledException)
            {
                // User hit backspace on empty input to go back
            }
        }

        private static string ReadInputWithBackNavigation(string prompt)
        {
            Console.WriteLine(prompt + " (press Backspace to go back before typing)");
            var input = "";
            ConsoleKeyInfo key;
            while (true)
            {
                key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return input;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (input.Length == 0)
                    {
                        NavigationService.GoBack();
                        throw new OperationCanceledException();
                    }
                    else
                    {
                        input = input.Substring(0, input.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    input += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
            }
        }



    }
}
