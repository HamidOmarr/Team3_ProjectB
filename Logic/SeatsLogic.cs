public class SeatsLogic
{
    public List<SeatsModel> GetSeatsByAuditorium(int auditoriumId)
    {
        SeatAccess seatAccess = new SeatAccess();
        return seatAccess.GetSeatsByAuditorium(auditoriumId);
    }

    public SeatsModel GetSeatByRowAndNumber(string row, int seatNumber, int auditoriumId)
    {
        return SeatAccess.GetSeatByRowAndNumber(row, seatNumber, auditoriumId); // Call statically
    }


    public static HashSet<int> GetReservedSeatIds(int movieSessionId)
    {
        var access = new SeatAccess();
        return new HashSet<int>(access.GetReservedSeatIds(movieSessionId));
    }



}

