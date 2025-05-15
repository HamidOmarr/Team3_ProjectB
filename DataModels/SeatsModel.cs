namespace Team3_ProjectB
{
    public class SeatsModel
    {
        public long Id { get; set; }
        public string RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public int SeatTypeId { get; set; }
        public decimal Price { get; set; }

        public SeatsModel(long id, string rowNumber, int seatNumber, int seatTypeId, decimal price)
        {
            Id = id;
            RowNumber = rowNumber;
            SeatNumber = seatNumber;
            SeatTypeId = seatTypeId;
            Price = price;

        }
        public SeatsModel() { }
    }
}