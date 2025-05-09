public class SeatsLogic
{
    public List<SeatsModel> GetSeatsByAuditorium(int auditoriumId)
    {
        SeatAccess seatAccess = new SeatAccess();
        return seatAccess.GetSeatsByAuditorium(auditoriumId);
    }

    public SeatsModel GetSeatByRowAndNumber(string row, int seatNumber)
    {
        return SeatAccess.GetSeatByRowAndNumber(row, seatNumber); // Call statically
    }
}

