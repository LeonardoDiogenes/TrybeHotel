namespace TrybeHotel.Dto
{
    public class BookingDtoInsert
    {
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
        public int GuestQuant { get; set; }
        public int RoomId { get; set; }
    }

    public class BookingResponse
    {
        public string? GuestName { get; set; }
        public int BookingId { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
        public int GuestQuant { get; set; }
        public RoomDto? Room { get; set; }
    }
}
