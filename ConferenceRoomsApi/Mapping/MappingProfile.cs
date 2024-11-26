using AutoMapper;
using ConferenceRoomsApi.Dto;
using ConferenceRoomsApi.Models.ConferenceRoom;
using ConferenceRoomsApi.Models.Services;

namespace ConferenceRoomsApi.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Настройка маппинга Room -> RoomDto
            CreateMap<Room, RoomDto>();
            CreateMap<Service, ServiceDto>();
        }
    }
}
