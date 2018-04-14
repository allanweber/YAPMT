using AutoMapper;
using YAPMT.Domain.Dtos;
using YAPMT.Domain.Entities;

namespace YAPMT.Infrastructure.Mappers
{
    public class EntitiesToDto: Profile
    {
        public EntitiesToDto()
        {
            this.CreateMap<Project, ProjectDto>();

            this.CreateMap<Assignment, AssignmentDto>();
        }
    }
}
