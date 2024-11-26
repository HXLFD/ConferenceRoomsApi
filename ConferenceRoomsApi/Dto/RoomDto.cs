namespace ConferenceRoomsApi.Dto
{
    public class RoomDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public decimal BaseRentPerHour { get; set; }

        public List<ServiceDto> Services { get; set; }

    }
}
