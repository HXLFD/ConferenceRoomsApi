namespace ConferenceApi.Models
{
    public class AddRoomRequest
    {
        public string Name { get; set; } // Название зала

        public int Capacity { get; set; } // Вместимость

        public decimal BasePricePerHour { get; set; } // Базовая цена за час

        public List<int> SelectedServiceIds { get; set; } // Список ID выбранных услуг

    }
}
