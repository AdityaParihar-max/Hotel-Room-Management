using System;
using System.ComponentModel.DataAnnotations;

namespace HotelDatabase.Models
{
    public class UserRequest
    {
        [Required]
        public string? RoomType { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? DepartureDate { get; set; }
    }
}
