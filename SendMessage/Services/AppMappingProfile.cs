using SendMessage.Dto;
using SendMessage.Models;
using AutoMapper;

namespace SendMessage.Services
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Message, MessageDto>();
        }
    }
}
