public class SeatsLogic
{
    public List<SeatsModel> GetSeatsByAuditorium(int auditoriumId)
    {
        SeatAccess seatAccess = new SeatAccess();
        return seatAccess.GetSeatsByAuditorium(auditoriumId);
    }
}
