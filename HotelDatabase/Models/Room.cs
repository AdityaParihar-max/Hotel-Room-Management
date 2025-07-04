using System.ComponentModel.DataAnnotations;

namespace HotelDatabase.Models
{
    public class Room
    {
        public int Id { get; set; }

        public string? RoomNumber { get; set; }
        public string? Type { get; set; }
        public decimal Price { get; set; }
        public string? Features { get; set; }
        public bool IsAvailable { get; set; }
    }
}
