using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using YAPMT.Domain.CommandHandlers.Commands.Project;
using YAPMT.Domain.Dtos;
using YAPMT.Domain.Entities;
using YAPMT.Domain.Repositories;
using YAPMT.Domain.Services;
using YAPMT.Framework.Constants;
using YAPMT.Framework.Controllers;

namespace YAPMT.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[Controller]")]
    [EnableCors(AppConstants.ALLOWALLHEADERS)]
    public class ProjectController :
        BaseCrudController<
            IProjectRepository,
            Project,
            ProjectInsertCommand,
            ProjectUpdateCommand,
            ProjectDeleteCommand,
            ProjectDto>
    {
        public ProjectController(IMapper mapper, IMediator mediator, 
            IProjectRepository projectRepository, IProjectService projectService)
            : base(mapper, mediator, projectRepository)
        {
            ProjectRepository = projectRepository;
            this.ProjectService = projectService;
        }

        public IProjectRepository ProjectRepository { get; }
        public IProjectService ProjectService { get; }

        [HttpGet]
        [Route("{projectId:int}/status")]
        public async Task<IActionResult> Status(int projectId)
        {
            var dto = await this.ProjectService.GetProjectStatus(projectId);

            return Ok(dto);
        }
    }
}