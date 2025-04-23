public class ReservationModel
{
    public int ReservationId { get; set; }
    public int AccountId { get; set; }
    public int SessionId { get; set; }
    public List<int> SeatIds { get; set; }
    public List<string> Snacks { get; set; }
    public string MovieTitle { get; set; }
    public DateTime Showtime { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime ReservationTime { get; set; }
    public string PaymentMethod { get; set; }
    public string CardNumber { get; set; }
}
