using System;
using System.Collections.Generic;

public class Checkout
{
    public void StartCheckout(int accountId, int sessionId, List<int> seatIds, List<string> snacks, string movieTitle, DateTime showtime)
    {
        var reservation = new ReservationModel
        {
            AccountId = accountId,
            SessionId = sessionId,
            SeatIds = seatIds,
            Snacks = snacks,
            MovieTitle = movieTitle,
            Showtime = showtime,
            //TotalPrice = ReservationSystem.CalculateTotalPrice(sessionId, seatIds, snacks),
            ReservationTime = DateTime.Now
        };

        Console.WriteLine("===== Checkout Summary =====");
        Console.WriteLine($"Movie: {movieTitle}");
        Console.WriteLine($"Showtime: {showtime}");
        Console.WriteLine("Seats: " + string.Join(", ", seatIds));
        Console.WriteLine("Snacks: " + (snacks.Count > 0 ? string.Join(", ", snacks) : "None"));
        Console.WriteLine($"Total: €{reservation.TotalPrice}");
        Console.WriteLine("============================");

        ReservationSystem.ProcessPayment();
        ReservationAccess.SaveReservation(reservation);
    }
}
