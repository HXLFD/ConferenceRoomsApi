using ConferenceApi.Models;
using ConferenceRoomsApi.Models.Bookings;
using ConferenceRoomsApi.Models.ConferenceRoom;

namespace ConferenceRoomsApi.Interfaces
{
    public interface IConferenceRoomCommands
    {
        public Task<Room> AddRoom(Room room);

        Task<bool> DeleteRoom(int id);

        Task<bool> UpdateRoom(int id, Room room);

        public Room GetRoomById(int id);

        public Booking BookRoom(int roomId, TimeSpan duration, DateTime startTime, List<string> selectedServices);

        Task<List<Room>> GetAvailableRooms(DateTime date, TimeSpan startTime, TimeSpan endTime, int capacity);
    }
}
