public class ReservationConsumablesLogic
{
    private readonly ReservationConsumablesAccess _reservationConsumablesAccess;

    public ReservationConsumablesLogic()
    {
        _reservationConsumablesAccess = new ReservationConsumablesAccess();
    }

    public void SaveReservationConsumable(ReservationConsumableModel reservationConsumable)
    {
        _reservationConsumablesAccess.SaveReservationConsumable(reservationConsumable);
    }

    public List<(string Name, int Quantity, decimal ActualPrice)> GetConsumablesForCheckout(long reservationId)
    {
        return _reservationConsumablesAccess.GetConsumablesForCheckout(reservationId);
    }
}
