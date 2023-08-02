namespace TMS.Models.Dto
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string EventId { get; set; }
        public int NumberOfTickets { get; set; }
        public string? TicketCategory { get; set; }
        public double TotalPrice { get; set; }
    }
}