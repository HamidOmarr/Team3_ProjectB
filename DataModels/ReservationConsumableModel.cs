namespace Team3_ProjectB
{
    public class ReservationConsumableModel
    {
        public long Id { get; set; }
        public long ReservationId { get; set; }
        public long ConsumableId { get; set; }
        public int Quantity { get; set; }
        public decimal ActualPrice { get; set; }
    }
}