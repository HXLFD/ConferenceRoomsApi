using ConferenceRoomsApi.ConferenceDbContext;
using ConferenceRoomsApi.Dto;
using ConferenceRoomsApi.Interfaces;
using ConferenceRoomsApi.Models;
using ConferenceRoomsApi.Models.Bookings;
using ConferenceRoomsApi.Models.ConferenceRoom;
using ConferenceRoomsApi.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConferenceRoomController(IConferenceRoomCommands command, AppDbContext context) : ControllerBase
    {
        private readonly IConferenceRoomCommands _rooms = command;

        private readonly AppDbContext _context = context;

        [HttpGet("Rooms")]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _context.Rooms.ToListAsync();
            return Ok(rooms);
        }

        [HttpPost("Room")]
        public async Task<IActionResult> AddRoom([FromBody] Room room)
        {
            if (room == null)
            {
                return BadRequest("Room data is required.");
            }
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddRoom), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]

        public async Task<bool> UpdateRoom(int id, [FromBody] Room room)
        {
            var existingRoom = await _context.Rooms.FindAsync(id);
            if (existingRoom == null) return false;

            existingRoom.Name = room.Name;
            existingRoom.Capacity = room.Capacity;
            await _context.SaveChangesAsync();
            return true;
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            // Ищем комнату по ID
            var room = await _context.Rooms
                .Include(r => r.RoomServices)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (room == null)
            {
                return NotFound(new { Message = "Room not found" });
            }

            // Удаляем связанные записи в таблице RoomServices
            if (room.RoomServices != null && room.RoomServices.Count > 0)
            {
                _context.RoomServices.RemoveRange(room.RoomServices);
            }

            // Удаляем саму комнату
            _context.Rooms.Remove(room);

            // Сохраняем изменения в базе данных
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Room deleted successfully" });
        }

        [HttpGet]
        public async Task<IActionResult> SearchRooms([FromQuery] DateTime date, [FromQuery] TimeSpan startTime, [FromQuery] TimeSpan endTime, [FromQuery] int capacity)
        {
            var availableRooms = await _rooms.GetAvailableRooms(date, startTime, endTime, capacity);
            return Ok(availableRooms);
        }

        [HttpPost("book")]

        public IActionResult BookRoom([FromBody] Booking booking)
        {
            try
            {
                // Вызываем метод бронирования из сервиса
                var tryBooking = _rooms.BookRoom(
                    roomId: booking.RoomId,
                    duration: booking.Duration,
                    startTime: booking.StartTime,
                    selectedServices: booking.SelectedServices
                );

                // Возвращаем успешный ответ с данными бронирования
                return Ok(new { Message = "Room booked successfully", Booking = tryBooking });
            }
            catch (Exception ex)
            {
                // Возвращаем ошибку в случае, если что-то пошло не так
                return BadRequest(new { ex.Message });
            }
        }
    }
}