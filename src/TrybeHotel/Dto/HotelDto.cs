namespace TrybeHotel.Dto
{
    public class HotelDto
    {
        public int HotelId { get; set; }

        public List<string>? Image { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public int CityId { get; set; }
        public string? CityName { get; set; } // tabela Cities
        public string? State { get; set; } // tabela Cities
    }
}