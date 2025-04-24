public class ReservationModel
{
    public Int64 Id { get; set; }
    public long UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; }

    public ReservationModel() { }

    public ReservationModel(Int64 id, long userId, decimal totalPrice, string status)
    {
        Id = id;
        UserId = userId;
        TotalPrice = totalPrice;
        Status = status;
    }
}
