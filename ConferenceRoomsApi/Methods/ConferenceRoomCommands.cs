using ConferenceRoomsApi.Interfaces;
using ConferenceRoomsApi.Models.Bookings;
using ConferenceRoomsApi.Models.ConferenceRoom;
using ConferenceRoomsApi.Models.Services;

namespace ConferenceRoomsApi.Methods
{
    public class ConferenceRoomCommands : IConferenceRoomCommands
    {
        private readonly List<Room> _rooms = new List<Room>();

        public ConferenceRoomCommands()
        {
            _rooms.Add(new Room { Name = "A", Id = 1, Capacity = 50, BaseRentPerHour = 2000 });
            _rooms.Add(new Room { Name = "B", Id = 2, Capacity = 100, BaseRentPerHour = 3500 });
            _rooms.Add(new Room { Name = "C", Id = 3, Capacity = 30, BaseRentPerHour = 1500 });

            var services = new List<Service>
        {
            new Service { Id = 1, Name = "Проектор", Price = 500 },
            new Service { Id = 2, Name = "Wi-Fi", Price = 300 },
            new Service { Id = 3, Name = "Звук", Price = 700 }
        };

            foreach (var room in _rooms)
            {
                room.Services.AddRange(services);
            }
        }


        public async Task<Room> AddRoom(Room room)
        {
            room.Id = _rooms.Max(r => r.Id) + 1;
            _rooms.Add(room);
            return await Task.FromResult(room);
        }

        public async Task<List<Room>> GetAvailableRooms(DateTime date, TimeSpan startTime, TimeSpan endTime, int capacity)
        {
            return await Task.FromResult(_rooms.Where(r => r.Capacity >= capacity).ToList());
        }

        public Room GetRoomById(int id)
        {
            return _rooms.FirstOrDefault(x => x.Id == id);
        }
        public async Task<bool> UpdateRoom(int id, Room room)
        {
            var existingRoom = _rooms.FirstOrDefault(r => r.Id == id);
            if (existingRoom == null) return await Task.FromResult(false);

            existingRoom.Name = room.Name;
            existingRoom.Capacity = room.Capacity;
            existingRoom.BaseRentPerHour = room.BaseRentPerHour;
            existingRoom.Services = room.Services;
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteRoom(int id)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return await Task.FromResult(false);
            _rooms.Remove(room);
            return await Task.FromResult(true);
        }

        public Booking BookRoom(int roomId, TimeSpan duration, DateTime startTime, List<string> selectedServices)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == roomId) ?? throw new Exception("Зал не найден");


            decimal totalPrice = room.BaseRentPerHour * (decimal)duration.TotalHours;

            TimeSpan startHour = startTime.TimeOfDay;

            if (startHour >= new TimeSpan(18, 0, 0) && startHour <= new TimeSpan(23, 0, 0))
            {
                totalPrice *= 20 / 100;
            }
            else if (startHour >= new TimeSpan(6, 0, 0) && startHour <= new TimeSpan(9, 0, 0))
            {
                totalPrice *= 10 / 100;
            }
            else if (startHour >= new TimeSpan(12, 0, 0) && startHour <= new TimeSpan(14, 0, 0))
            {
                totalPrice *= 15 / 100;
            }

            foreach (var serviceName in selectedServices)
            {
                var service = room.Services.FirstOrDefault(s => s.Name == serviceName);
                if (service != null)
                {
                    totalPrice += service.Price;
                }
            }
            var booking = new Booking
            {
                Id = 0,
                RoomId = room.Id,
                Duration = duration,
                StartTime = startTime,
                SelectedServices = selectedServices,
                TotalCost = totalPrice
            };
            return booking;
        }


    }
}
