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

        foreach (var (row, seatNum) in selectedSeats)
        {
            Console.WriteLine($"|  - Row {row}, Seat {seatNum.ToString().PadRight(14)}|");
        }

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



        var reservation = new ReservationModel
        {
            UserId = user.Id,
            TotalPrice = CalculateTotalPrice(selectedSeats),
            Status = "pending"
        };
        ReservationsAccess.Create(reservation);

        foreach (var (row, seatNum) in selectedSeats)
        {
            var seat = SeatAccess.GetSeatByRowAndNumber(row, seatNum);
            var ticket = new TicketModel
            {
                ReservationId = reservation.Id, // No change needed
                MovieSessionId = GetMovieSessionId(movieName, sessionTime), // No change needed
                SeatId = seat.Id, // No cast needed since SeatId is long
                ActualPrice = (decimal)seat.Price // Cast Price to decimal
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
        foreach (var (row, seatNum) in selectedSeats)
        {
            var seat = SeatAccess.GetSeatByRowAndNumber(row, seatNum);
            totalPrice += (decimal)seat.Price; // Cast Price to decimal
        }
        return totalPrice;
    }


    private static long GetMovieSessionId(string movieName, string sessionTime)
    {
        var session = MovieSessionsAccess.GetSessionByMovieAndTime(movieName, sessionTime);
        return session.Id;
    }
}
