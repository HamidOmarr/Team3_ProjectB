using System;

public static class ReservationAccess
{
    public static void SaveReservation(ReservationModel reservation)
    {
        // Simulate saving to database with a confirmation message
        Console.WriteLine("\nReservation saved:");
        Console.WriteLine($"Movie: {reservation.MovieTitle}");
        Console.WriteLine($"Time: {reservation.Showtime}");
        Console.WriteLine($"Seats: {string.Join(", ", reservation.SeatIds)}");
        Console.WriteLine($"Snacks: {(reservation.Snacks.Count > 0 ? string.Join(", ", reservation.Snacks) : "None")}");
        Console.WriteLine($"Total Paid: €{reservation.TotalPrice}");
        Console.WriteLine("Reservation timestamp: " + reservation.ReservationTime);
    }
}
