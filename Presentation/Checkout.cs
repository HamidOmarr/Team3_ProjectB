
namespace Team3_ProjectB
{
    public class Checkout
    {
        public static void StartCheckout(string movieName, string sessionTime, List<(string row, int seat)> selectedSeats, int auditoriumId)
        {
            Console.Clear();

            AccountsLogic accountsLogic = new AccountsLogic();
            AccountModel user = AccountsLogic.CurrentAccount;

            // Check if the user is logged in or create a guest user
            if (user == null)
            {
                string[] loginOptions = { "Yes, log in", "No, continue as guest" };
                int selectedIndex = 0;
                ConsoleKey key;

                do
                {
                    Console.Clear();
                    Console.WriteLine("You are not logged in.\nWould you like to log in? Press Backspace to go back.\n");
                    for (int i = 0; i < loginOptions.Length; i++)
                    {
                        if (i == selectedIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkCyan;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine($"[>] {loginOptions[i]}");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine($"[ ] {loginOptions[i]}");
                        }
                    }

                    key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.UpArrow && selectedIndex > 0)
                        selectedIndex--;
                    else if (key == ConsoleKey.DownArrow && selectedIndex < loginOptions.Length - 1)
                        selectedIndex++;
                    else if (key == ConsoleKey.Backspace)
                    {
                        NavigationService.GoBack();
                        return;
                    }

                } while (key != ConsoleKey.Enter);

                if (selectedIndex == 0)
                {
                    Console.Clear();

                    user = UserLogin.Start();
                    if (user == null)
                    {
                        // If user pressed Backspace in login, go back
                        return;
                    }
                }
                else
                {
                    Console.Clear();

                    Console.WriteLine("Continuing with guest account...");
                    var existingGuestUser = accountsLogic.GetAccountByEmail("guest@example.com");
                    if (existingGuestUser != null)
                    {
                        user = existingGuestUser;
                    }
                    else
                    {
                        var uniqueGuestEmail = $"guest_{Guid.NewGuid()}@example.com";
                        string? name = CustomInput("Please provide your name (Backspace to go back): ");
                        if (name == null)
                        {
                            NavigationService.GoBack();
                            return;
                        }
                        user = new AccountModel(0, name, uniqueGuestEmail, "guest_password", "guest");
                        user.Id = accountsLogic.WriteAccount(user);
                        Console.WriteLine("Guest user details saved successfully.");
                    }
                }
            }

            // Create a reservation
            ReservationsLogic reservationsLogic = new ReservationsLogic();
            var reservation = new ReservationModel
            {
                UserId = user.Id,
                TotalPrice = 0,
                Status = "pending"
            };
            reservation.Id = reservationsLogic.CreateReservation(reservation);

            // Food menu selection
            Foodmenu.StartFoodMenu(reservation.Id);

            // Seat and food summary
            decimal totalPrice = 0;
            SeatsLogic seatsLogic = new SeatsLogic();
            PricesLogic pricesLogic = new PricesLogic();

            const int receiptWidth = 30;

            string BorderLine() => "+" + new string('-', receiptWidth - 2) + "+";
            string FormatLine(string content) => $"| {content.PadRight(receiptWidth - 3)}|";

            Console.Clear();
            Console.WriteLine(BorderLine());
            Console.WriteLine(FormatLine("RECEIPT"));
            Console.WriteLine(BorderLine());
            Console.WriteLine(FormatLine($"Movie: {movieName}"));
            Console.WriteLine(FormatLine($"Time:  {sessionTime}"));
            Console.WriteLine(BorderLine());
            Console.WriteLine(FormatLine("Seats:"));

            foreach (var (row, seatNum) in selectedSeats)
            {
                var seat = seatsLogic.GetSeatByRowAndNumber(row, seatNum, auditoriumId);
                decimal actualPrice = pricesLogic.GetPrice(seat.SeatTypeId, null);

                Console.WriteLine(FormatLine($"- Row {row}, Seat {seatNum} - {actualPrice} EUR"));
                totalPrice += actualPrice;

                var ticket = new TicketModel
                {
                    ReservationId = reservation.Id,
                    MovieSessionId = new MovieSessionsLogic().GetSessionByMovieAndTime(movieName, sessionTime).Id,
                    SeatId = seat.Id,
                    ActualPrice = actualPrice
                };
                new TicketsLogic().CreateTicket(ticket);
            }

            Console.WriteLine(FormatLine("Food & Drinks:"));
            var foodItems = new ReservationConsumablesLogic().GetConsumablesForCheckout(reservation.Id);

            decimal foodTotalPrice = 0;
            foreach (var (name, quantity, actualPrice) in foodItems)
            {
                string line = $"- {name} x{quantity} - {actualPrice} EUR";
                if (line.Length > receiptWidth - 3)
                    line = line.Substring(0, receiptWidth - 6) + "...";

                Console.WriteLine(FormatLine(line));
                foodTotalPrice += actualPrice;
            }

            Console.WriteLine(BorderLine());
            totalPrice += foodTotalPrice;
            Console.WriteLine(FormatLine($"Total Price: {totalPrice:F2} EUR"));
            Console.WriteLine(BorderLine());

            Console.WriteLine("\nThank you for your reservation!");
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();

            NavigationService.Navigate(Menu.Start);
        }

        // Helper for custom input with Backspace support
        private static string? CustomInput(string prompt)
        {
            Console.WriteLine(prompt);
            string input = "";
            ConsoleKeyInfo keyInfo;
            while (true)
            {
                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (!string.IsNullOrEmpty(input))
                    {
                        Console.WriteLine();
                        return input;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        input = input.Substring(0, input.Length - 1);
                        Console.Write("\b \b");
                    }
                    else
                    {
                        Console.WriteLine();
                        return null; // Signal to go back
                    }
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    input += keyInfo.KeyChar;
                    Console.Write(keyInfo.KeyChar);
                }
            }
        }
    }
}