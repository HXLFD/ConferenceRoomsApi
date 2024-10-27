using ConferenceRoomsApi.Models.Services;

namespace ConferenceRoomsApi.Models.ConferenceRoom
{
    public class Room
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int Capacity { get; set; }

        public decimal BaseRentPerHour { get; set; }

        public List<Service> Services { get; set; } = new List<Service>();
    }
}
