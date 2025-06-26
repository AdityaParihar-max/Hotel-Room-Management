using HotelDatabase.Data;
using HotelDatabase.Interfaces;
using HotelDatabase.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelDatabase.Services
{
    public class RoomService : IRoomService
    {
        private readonly AppDbContext _context;

        public RoomService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync()
        {
            return await _context.Rooms
                .Where(r => r.IsAvailable)
                .ToListAsync();
        }

        public async Task<Room?> GetRoomByIdAsync(int id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public async Task<Room> CreateRoomAsync(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<bool> UpdateRoomAsync(int id, Room updatedRoom)
        {
            if (id != updatedRoom.Id)
                return false;

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return false;

            room.RoomNumber = updatedRoom.RoomNumber;
            room.Type = updatedRoom.Type;
            room.Price = updatedRoom.Price;
            room.Features = updatedRoom.Features;
            room.IsAvailable = updatedRoom.IsAvailable;

            _context.Entry(room).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
                return false;

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<Room>> CheckRoomAvailabilityAsync(string roomType)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(roomType))
                    return Enumerable.Empty<Room>();
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var query = _context.Rooms
                    .Where(r => r.IsAvailable && r.Type.ToLower() == roomType.ToLower());
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw new Exception("Error checking room availability", ex);
            }
        }
        public async Task<IEnumerable<object>> GetRoomGroupByTypeAsync()
        {
            var groupedRooms = await _context.Rooms
                .GroupBy(r => r.Type)
                .Select(g => new
                {
                    RoomType = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();
            return groupedRooms;
        }
    }
}