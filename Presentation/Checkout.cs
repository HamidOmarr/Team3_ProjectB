public class Checkout
{
    public static void StartCheckout(string movieName, string sessionTime, List<(string row, int seat)> selectedSeats)
    {
        Console.Clear();

        AccountsLogic accountsLogic = new AccountsLogic();
        AccountModel user = AccountsLogic.CurrentAccount;

        // Check if the user is logged in or create a guest user
        if (user == null)
        {
            Console.WriteLine("You are not logged in. Would you like to log in? (y/n)");
            string choice = Console.ReadLine()?.ToLower();

            if (choice == "y")
            {
                user = UserLogin.Start();
                if (user == null)
                {
                    Console.WriteLine("Login failed. Please try again.");
                    return; // Exit the checkout process if login fails
                }
            }
            else
            {
                // Check if a guest user already exists
                var existingGuestUser = accountsLogic.GetAccountByEmail("guest@example.com");
                if (existingGuestUser != null)
                {
                    user = existingGuestUser; // Use the existing guest user
                }
                else
                {
                    // Create a new guest user with a unique email
                    var uniqueGuestEmail = $"guest_{Guid.NewGuid()}@example.com";
                    Console.WriteLine("Please provide your name:");
                    string name = Console.ReadLine();
                    user = new AccountModel(0, name, uniqueGuestEmail, "guest_password", "guest");
                    user.Id = accountsLogic.WriteAccount(user); // Save the guest user
                    Console.WriteLine("Guest user details saved successfully.");
                }
            }
        }

        // Create a reservation
        ReservationsLogic reservationsLogic = new ReservationsLogic();
        var reservation = new ReservationModel
        {
            UserId = user.Id,
            TotalPrice = 0, // Initial total price, will be updated later
            Status = "pending"
        };
        reservation.Id = reservationsLogic.CreateReservation(reservation);

        // Calculate total price and prepare receipt
        decimal totalPrice = 0;
        SeatsLogic seatsLogic = new SeatsLogic();
        PricesLogic pricesLogic = new PricesLogic();

        Console.Clear();
        Console.WriteLine("+----------------------------+");
        Console.WriteLine("|         RECEIPT            |");
        Console.WriteLine("+----------------------------+");
        Console.WriteLine($"| Movie: {movieName.PadRight(20)}|");
        Console.WriteLine($"| Time:  {sessionTime.PadRight(20)}|");
        Console.WriteLine("+----------------------------+");
        Console.WriteLine("| Seats:                     |");

        foreach (var (row, seatNum) in selectedSeats)
        {
            var seat = seatsLogic.GetSeatByRowAndNumber(row, seatNum);
            decimal actualPrice = pricesLogic.GetPrice(seat.SeatTypeId, null); // Retrieve the price

            // Print row and seat information
            Console.WriteLine($"|  - Row {row}, Seat {seatNum.ToString().PadRight(14)}|");
            totalPrice += actualPrice;

            // Save ticket information
            MovieSessionsLogic movieSessionsLogic = new MovieSessionsLogic();
            TicketsLogic ticketsLogic = new TicketsLogic();
            var ticket = new TicketModel
            {
                ReservationId = reservation.Id,
                MovieSessionId = movieSessionsLogic.GetSessionByMovieAndTime(movieName, sessionTime).Id,
                SeatId = seat.Id,
                ActualPrice = actualPrice
            };
            ticketsLogic.CreateTicket(ticket);
        }

        // Retrieve and display food items
        Console.WriteLine("| Food & Drinks:             |");
        ReservationConsumablesLogic reservationConsumablesLogic = new ReservationConsumablesLogic();
        var foodItems = reservationConsumablesLogic.GetConsumablesForCheckout(reservation.Id);

        decimal foodTotalPrice = 0;
        foreach (var (name, quantity, actualPrice) in foodItems)
        {
            Console.WriteLine($"|  - {name} x{quantity} - {actualPrice:C}");
            foodTotalPrice += actualPrice;
        }

        Console.WriteLine("+----------------------------+");

        // Calculate the final total price
        totalPrice += foodTotalPrice;
        Console.WriteLine($"| Total Price: {totalPrice:F2} EUR".PadRight(28) + "|");
        Console.WriteLine("+----------------------------+");

        Console.WriteLine("\nThank you for your reservation!");
        Console.WriteLine("Press any key to return to the main menu...");
        Console.ReadKey();
        Menu.Start();
    }
}

