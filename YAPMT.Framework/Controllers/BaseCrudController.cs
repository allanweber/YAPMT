using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using YAPMT.Framework.CommandHandlers;
using YAPMT.Framework.Dtos;
using YAPMT.Framework.Entities;
using YAPMT.Framework.Repositories;

namespace YAPMT.Framework.Controllers
{
    public class BaseCrudController<TRepository, TEntity, TInsertCommand, TUpdateCommand, TDeleteCommand, TGetDto> : Controller
        where TRepository : IRepository<TEntity>
        where TEntity : BaseEntity
        where TInsertCommand : IRequest<ICommandResult>
        where TUpdateCommand : IRequest<ICommandResult>
        where TDeleteCommand : BaseEntity, IRequest<ICommandResult>, new()
        where TGetDto : IDto
    {
        public IMapper Mapper { get; }
        public IMediator Mediator { get; }
        public IRepository<TEntity> Repository { get; }

        protected BaseCrudController(IMapper mapper, IMediator mediator, IRepository<TEntity> repository)
        {
            this.Mapper = mapper;
            this.Mediator = mediator;
            this.Repository = repository;
        }

        public BaseCrudController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var entity = await this.Repository.GetAllAsync();

            var dto = Mapper.Map<List<TEntity>, List<TGetDto>>(entity);

            return this.Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TInsertCommand request)
        {
            ICommandResult result = await this.Mediator.Send(request);

            return this.Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TUpdateCommand request)
        {
            ICommandResult result = await this.Mediator.Send(request);

            return this.Ok(result);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var entity = await this.Repository.GetAsync(id);

            if (entity == null) return NotFound();

            var dto = Mapper.Map<TEntity, TGetDto>(entity);

            return this.Ok(dto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            TDeleteCommand delete = new TDeleteCommand();
            delete.Id = id;

            ICommandResult result = await this.Mediator.Send(delete);

            return this.Ok(result);
        }
    }
}
