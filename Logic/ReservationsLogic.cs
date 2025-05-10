public class ReservationsLogic
{
    public long CreateReservation(ReservationModel reservation)
    {
        return ReservationsAccess.Create(reservation);
    }
}
