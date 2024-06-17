namespace TrybeHotel.Dto
{
    public class BookingDtoInsert
    {
        public string? GuestEmail { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
        public int GuestQuant { get; set; }
        public int RoomId { get; set; }
    }

    public class BookingResponse
    {
        public int BookingId { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
        public int GuestQuant { get; set; }
        public RoomDto? Room { get; set; }
    }
}
