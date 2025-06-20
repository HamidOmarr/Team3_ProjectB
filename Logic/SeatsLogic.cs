using System.Linq;

namespace Team3_ProjectB
{
    public class SeatsLogic
{
    public List<SeatsModel> GetSeatsByAuditorium(int auditoriumId)
    {
        SeatAccess seatAccess = new SeatAccess();
        return seatAccess.GetSeatsByAuditorium(auditoriumId);
    }

    public SeatsModel GetSeatByRowAndNumber(string row, int seatNumber, int auditoriumId)
    {
        return SeatAccess.GetSeatByRowAndNumber(row, seatNumber, auditoriumId);
    }


    public static HashSet<int> GetReservedSeatIds(int movieSessionId)
    {
        var access = new SeatAccess();
        return new HashSet<int>(access.GetReservedSeatIds(movieSessionId));
    }

    public List<SeatsModel> GetAvailableSeats(int auditoriumId, int movieSessionId)
    {
        var allSeats = GetSeatsByAuditorium(auditoriumId);
        var reservedSeatIds = GetReservedSeatIds(movieSessionId);
        return allSeats.Where(seat => !reservedSeatIds.Contains((int)seat.Id)).ToList();
    }


    }
}
