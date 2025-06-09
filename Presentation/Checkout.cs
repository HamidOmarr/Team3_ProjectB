namespace Team3_ProjectB
{
    public class Checkout
    {
        public static void StartCheckout(string movieName, string sessionTime, List<(string row, int seat)> selectedSeats, int auditoriumId)
        {
            Console.Clear();
            LoginStatusHelper.ShowLoginStatus(); // Toon login status bovenaan

            AccountsLogic accountsLogic = new AccountsLogic();
            AccountModel? user = AccountsLogic.CurrentAccount;

            if (user == null)
            {
                string[] loginOptions = { "Yes, log in", "No, continue as guest" };
                int selectedIndex = 0;
                ConsoleKey key;

                do
                {
                    Console.Clear();
                    LoginStatusHelper.ShowLoginStatus();

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

                if (selectedIndex == 0) // User wil inloggen
                {
                    Console.Clear();
                    LoginStatusHelper.ShowLoginStatus();

                    user = UserLogin.Start();
                    if (user == null) return; // Terug bij backspace in login
                }
                else
                {
                    Console.Clear();
                    LoginStatusHelper.ShowLoginStatus();

                    Console.WriteLine("Continuing with guest account...");

                    string? name = AccountsLogic.CustomInput("Please provide your name (Backspace to go back): ");
                    if (name == null)
                    {
                        NavigationService.GoBack();
                        return;
                    }

                    string? email;
                    do
                    {
                        email = AccountsLogic.CustomInput("Please provide your email address (Backspace to go back): ");
                        if (email == null)
                        {
                            NavigationService.GoBack();
                            return;
                        }

                        if (!accountsLogic.IsValidEmail(email))
                        {
                            Console.WriteLine("Invalid email: it must contain '@' and \".\" and valid characters. Please try again.");
                            Thread.Sleep(2500);
                            Console.Clear();
                            LoginStatusHelper.ShowLoginStatus();
                        }

                    } while (!accountsLogic.IsValidEmail(email));

                    user = accountsLogic.GetOrCreateGuest(name, email);
                    Console.WriteLine("Guest user details loaded successfully.");
                }
            }

            // Maak een reservering aan
            ReservationsLogic reservationsLogic = new ReservationsLogic();
            var reservation = new ReservationModel
            {
                UserId = user.Id,
                TotalPrice = 0,
                Status = "pending"
            };
            reservation.Id = reservationsLogic.CreateReservation(reservation);

            // Start foodmenu
            Foodmenu.StartFoodMenu(reservation.Id);

            // Toon overzicht tickets + food + totaalprijs
            decimal totalPrice = 0;
            SeatsLogic seatsLogic = new SeatsLogic();
            PricesLogic pricesLogic = new PricesLogic();

            const int receiptWidth = 30;

            string BorderLine() => "+" + new string('-', receiptWidth - 2) + "+";
            string FormatLine(string content) => $"| {content.PadRight(receiptWidth - 3)}|";

            Console.Clear();
            LoginStatusHelper.ShowLoginStatus();

            Console.WriteLine(BorderLine());
            Console.WriteLine(FormatLine("RECEIPT"));
            Console.WriteLine(BorderLine());
            Console.WriteLine(FormatLine($"Name:  {user.Name}"));
            Console.WriteLine(FormatLine($"Email: {user.Email}"));
            Console.WriteLine(BorderLine());
            Console.WriteLine(FormatLine($"Movie: {movieName}"));
            Console.WriteLine(FormatLine($"Time:  {sessionTime}"));
            Console.WriteLine(BorderLine());

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
    }
}
