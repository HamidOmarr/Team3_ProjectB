public class Checkout
{
    public static void StartCheckout(string movieName, string sessionTime, List<(string row, int seat)> selectedSeats)
    {
        Console.Clear();

        Console.WriteLine("+----------------------------+");
        Console.WriteLine("|         RECEIPT            |");
        Console.WriteLine("+----------------------------+");
        Console.WriteLine($"| Movie: {movieName.PadRight(20)}|");
        Console.WriteLine($"| Time:  {sessionTime.PadRight(20)}|");
        Console.WriteLine("+----------------------------+");
        Console.WriteLine("| Seats:                     |");

        decimal totalPrice = 0;
        SeatsLogic seatsLogic = new SeatsLogic();
        PricesLogic pricesLogic = new PricesLogic();

        foreach (var (row, seatNum) in selectedSeats)
        {
            var seat = seatsLogic.GetSeatByRowAndNumber(row, seatNum);
            decimal actualPrice = pricesLogic.GetPrice(seat.SeatTypeId, null); // Retrieve the price

            // Print row and seat information
            Console.WriteLine($"|  - Row {row}, Seat {seatNum.ToString().PadRight(14)}|");
            totalPrice += actualPrice;
        }

        Console.WriteLine("+----------------------------+");
        Console.WriteLine($"| Total Price: {totalPrice:F2} EUR".PadRight(28) + "|");
        Console.WriteLine("+----------------------------+");

        AccountModel user = AccountsLogic.CurrentAccount;
        if (user == null)
        {
            Console.WriteLine("\nYou are not logged in. Would you like to log in? (y/n)");
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
                Console.WriteLine("Please provide your name:");
                string name = Console.ReadLine();
                Console.WriteLine("Please provide your email:");
                string email = Console.ReadLine();

                user = new AccountModel(0, name, email, "wow", "guest");
                user.Id = AccountsAccess.Write(user); // Assign the generated Id
                Console.WriteLine("Guest user details saved successfully.");
            }
        }

        ReservationsLogic reservationsLogic = new ReservationsLogic();
        var reservation = new ReservationModel
        {
            UserId = user.Id,
            TotalPrice = CalculateTotalPrice(selectedSeats),
            Status = "pending"
        };
        reservation.Id = reservationsLogic.CreateReservation(reservation);

        MovieSessionsLogic movieSessionsLogic = new MovieSessionsLogic();
        foreach (var (row, seatNum) in selectedSeats)
        {
            var seat = seatsLogic.GetSeatByRowAndNumber(row, seatNum);

            // Retrieve the price based on seat type and promotion type
            decimal actualPrice = pricesLogic.GetPrice(seat.SeatTypeId, null); // Pass null for promotion_type_id if no promotion is applied

            var ticket = new TicketModel
            {
                ReservationId = reservation.Id,
                MovieSessionId = movieSessionsLogic.GetSessionByMovieAndTime(movieName, sessionTime).Id,
                SeatId = seat.Id,
                ActualPrice = actualPrice // Set the actual price
            };
            TicketsAccess.Create(ticket);
        }

        Console.WriteLine("\nThank you for your reservation!");
        Console.WriteLine("Press any key to return to the main menu...");
        Console.ReadKey();
        Menu.Start();
    }

    private static decimal CalculateTotalPrice(List<(string row, int seat)> selectedSeats)
    {
        decimal totalPrice = 0;
        SeatsLogic seatsLogic = new SeatsLogic();
        foreach (var (row, seatNum) in selectedSeats)
        {
            var seat = seatsLogic.GetSeatByRowAndNumber(row, seatNum);
            totalPrice += (decimal)seat.Price; // Cast Price to decimal
        }
        return totalPrice;
    }
}
