using ConferenceRoomsApi.Models.ConferenceRoom;
using ConferenceRoomsApi.Models.Services;

namespace ConferenceRoomsApi.Models
{
    public class RoomService
    {
        public int RoomId { get; set; }
        public Room Room { get; set; }

        // Внешний ключ на Service
        public int ServiceId { get; set; }
        public Service Service { get; set; }
    }
}
