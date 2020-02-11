using AutoMapper;
using Priority.Data.Dto;
using Priority.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Priority.Web.Mappings
{
    public class PriorityTaskMapping : Profile
    {
        public PriorityTaskMapping()
        {
            CreateMap<PriorityTaskModifyDto, PriorityTask>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.NextPriorityTask, opt => opt.MapFrom(src => src.NextPriorityTask));
        }
    }
}
