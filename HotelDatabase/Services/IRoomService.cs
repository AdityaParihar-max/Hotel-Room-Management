using HotelDatabase.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelDatabase.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAvailableRoomsAsync();
        Task<Room?> GetRoomByIdAsync(int id);
        Task<Room> CreateRoomAsync(Room room);
        Task<bool> UpdateRoomAsync(int id, Room updatedRoom);
        Task<bool> DeleteRoomAsync(int id);
        Task<IEnumerable<Room>> CheckRoomAvailabilityAsync(string roomType);

        Task<IEnumerable<object>> GetRoomGroupByTypeAsync();
    }
}
