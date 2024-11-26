using ConferenceRoomsApi.Models.ConferenceRoom;

namespace ConferenceRoomsApi.Models.Services
{
    public class Service
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public List<RoomService> RoomServices { get; set; } = new List<RoomService>();
    }
}
