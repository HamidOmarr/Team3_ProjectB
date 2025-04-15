public class SeatsModel
{
    public int Id { get; set; }
    public string RowNumber { get; set; }
    public int SeatNumber { get; set; }
    public int SeatTypeId { get; set; }
    public double Price { get; set; }

    public SeatsModel(int id, string rowNumber, int seatNumber, int seatTypeId, double price)
    {
        Id = id;
        RowNumber = rowNumber;
        SeatNumber = seatNumber;
        SeatTypeId = seatTypeId;
        Price = price;

    }
    public SeatsModel() { }
}