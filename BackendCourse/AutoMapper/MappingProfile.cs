using AutoMapper;
using BackendCourse.DTOs;
using BackendCourse.Models;

namespace BackendCourse.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<BeerInsertDTO, Beer>();
            CreateMap<Beer, BeerDTO>().ForMember(dto => dto.Id,
                                                 m => m.MapFrom(b => b.BeerId));

            CreateMap<BeerUpdateDTO, Beer>();
        
        }
    }
}
