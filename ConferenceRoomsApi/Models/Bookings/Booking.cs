using ConferenceRoomsApi.Models.Services;

namespace ConferenceRoomsApi.Models.Bookings
{
    public class Booking
    {
        public int Id { get; set; }

        public int RoomId { get; set; } // ID забронированного зала

        public TimeSpan Duration { get; set; } // Продолжительность аренды

        public DateTime StartTime { get; set; } // Время начала бронирования

        public List<string> SelectedServices { get; set; } = new List<string>(); // Имена выбранных услуг

        public decimal TotalCost { get; set; } // Общая стоимость бронирования

    }
}
