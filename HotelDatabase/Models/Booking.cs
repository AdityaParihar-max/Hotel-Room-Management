using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelDatabase.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [ForeignKey("Room")]
        public int RoomId { get; set; }
        public Room? Room { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public string? GuestName { get; set; }
        public string? GuestEmail { get; set; }
    }
}
