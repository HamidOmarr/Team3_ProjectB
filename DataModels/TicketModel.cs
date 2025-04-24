public class TicketModel
{
    public long Id { get; set; }
    public long ReservationId { get; set; }
    public long MovieSessionId { get; set; }
    public long SeatId { get; set; }
    public decimal ActualPrice { get; set; }

    public TicketModel() { }

    public TicketModel(long id, long reservationId, long movieSessionId, long seatId, decimal actualPrice)
    {
        Id = id;
        ReservationId = reservationId;
        MovieSessionId = movieSessionId;
        SeatId = seatId;
        ActualPrice = actualPrice;
    }
}
