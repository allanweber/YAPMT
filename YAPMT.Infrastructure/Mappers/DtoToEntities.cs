using AutoMapper;
using YAPMT.Domain.CommandHandlers.Commands.Assignment;
using YAPMT.Domain.CommandHandlers.Commands.Project;
using YAPMT.Domain.Entities;

namespace YAPMT.Infrastructure.Mappers
{
    public class DtoToEntities: Profile
    {
        public DtoToEntities()
        {
            this.CreateMap<ProjectInsertCommand, Project>();
            this.CreateMap<ProjectUpdateCommand, Project>();

            this.CreateMap<AssignmentInsertCommand, Assignment>();
            this.CreateMap<AssignmentUpdateCommand, Assignment>();
        }
    }
}
