using ConferenceApi.Models;
using ConferenceRoomsApi.Interfaces;
using ConferenceRoomsApi.Models.Bookings;
using ConferenceRoomsApi.Models.ConferenceRoom;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ConferenceRoomsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConferenceRoomController(IConferenceRoomCommands command) : ControllerBase
    {
        private readonly IConferenceRoomCommands _rooms = command;

        [HttpPost("Room")]
        public async Task<IActionResult> AddRoom([FromBody] Room room)
        {
            var createdRoom = await _rooms.AddRoom(room);
            return CreatedAtAction(nameof(AddRoom), new { id = createdRoom.Id }, createdRoom);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateRoom(int id, [FromBody] Room room)
        {
            var updated = await _rooms.UpdateRoom(id, room);
            if (!updated) return NotFound();
            return NoContent();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var deleted = await _rooms.DeleteRoom(id);
            if (!deleted) return NotFound();
            return Ok(new { Message = "Room deleted successfully"});
        }

        [HttpGet]
        public async Task<IActionResult> SearchRooms([FromQuery] DateTime date, [FromQuery] TimeSpan startTime, [FromQuery] TimeSpan endTime, [FromQuery] int capacity)
        {
            var availableRooms = await _rooms.GetAvailableRooms(date, startTime, endTime, capacity);
            return Ok(availableRooms);
        }

        [HttpPost("book")]

        public IActionResult BookRoom([FromBody]Booking booking)
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