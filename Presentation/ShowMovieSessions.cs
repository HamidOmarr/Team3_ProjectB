using System;
using System.Collections.Generic;

namespace Team3_ProjectB
{
    public class ShowMoviesManager
    {
        public List<MovieSessionModel> MovieSessions { get; private set; }

        public ShowMoviesManager()
        {
            MovieSessionsLogic movieSessionsLogic = new MovieSessionsLogic();
            MovieSessions = movieSessionsLogic.GetAllMovieSessions();
        }

        public static int? DisplaySessions(int movieId)
        {
            MovieSessionsLogic movieSessionsLogic = new MovieSessionsLogic();
            var sessions = movieSessionsLogic.GetDetailedMovieSessionsFromId(movieId);

            if (sessions.Count == 0)
            {
                Console.WriteLine("No sessions found.");
                Console.WriteLine("Press any key to go back...");
                Console.ReadKey();
                NavigationService.GoBack();
                return null;
            }

            int selectedIndex = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                Console.WriteLine("Use ↑ ↓ to select a session. Press Enter to choose.");
                Console.WriteLine("Press Backspace to go back.\n");

                for (int i = 0; i < sessions.Count; i++)
                {
                    var session = sessions[i];
                    bool isSelected = i == selectedIndex;
                    if (isSelected)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine($"[{(isSelected ? ">" : " ")}] {session.StartTime:HH:mm} - {session.EndTime:HH:mm} | {session.Title} ({session.Genre}) - {session.AuditoriumName}");

                    if (isSelected)
                        Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                    selectedIndex--;
                else if (key == ConsoleKey.DownArrow && selectedIndex < sessions.Count - 1)
                    selectedIndex++;
                else if (key == ConsoleKey.Backspace)
                {
                    NavigationService.GoBack();
                    return null;
                }

            } while (key != ConsoleKey.Enter);

            var selectedSession = sessions[selectedIndex];

            // Ensure the user is logged in or create a guest user
            long userId = AccountsLogic.CurrentAccount?.Id ?? 0;
            if (userId == 0)
            {
                AccountsLogic accountsLogic = new AccountsLogic();
                var uniqueGuestEmail = $"guest_{Guid.NewGuid()}@example.com"; // Generate a unique email
                var guestUser = new AccountModel(0, "Guest", uniqueGuestEmail, "guest_password", "guest");
                userId = accountsLogic.WriteAccount(guestUser);
            }

            // Create a reservation and get the reservationId
            ReservationsLogic reservationsLogic = new ReservationsLogic();
            var reservation = new ReservationModel
            {
                UserId = userId, // Use the valid userId
                TotalPrice = 0, // Initial total price, will be updated later
                Status = "pending"
            };
            long reservationId = reservationsLogic.CreateReservation(reservation);

            // Pass the reservationId to SeatSelection
            NavigationService.Navigate(() =>
                SeatSelection.AmountSeatsInput(
                    selectedSession.AuditoriumId,
                    selectedSession.Title,
                    selectedSession.StartTime,
                    reservationId,
                    selectedSession.Id
                )
            );

            return sessions[selectedIndex].Id;
        }
    }
}
