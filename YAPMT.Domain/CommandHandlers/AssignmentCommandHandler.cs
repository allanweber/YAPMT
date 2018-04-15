using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using YAPMT.Domain.CommandHandlers.Commands.Assignment;
using YAPMT.Domain.Entities;
using YAPMT.Domain.Repositories;
using YAPMT.Framework.CommandHandlers;
using YAPMT.Framework.Specifications;

namespace YAPMT.Domain.CommandHandlers
{
    public class AssignmentCommandHandler :
        IRequestHandler<AssignmentInsertCommand, ICommandResult>,
        IRequestHandler<AssignmentUpdateCommand, ICommandResult>,
        IRequestHandler<AssignmentDeleteCommand, ICommandResult>
    {
        public AssignmentCommandHandler(IMapper mapper, IAssignmentRepository assignmentRepository, IProjectRepository projectRepository)
        {
            this.Mapper = mapper;
            this.AssignmentRepository = assignmentRepository;
            this.ProjectRepository = projectRepository;
        }

        public IMapper Mapper { get; }
        public IAssignmentRepository AssignmentRepository { get; }
        public IProjectRepository ProjectRepository { get; }

        public async Task<ICommandResult> Handle(AssignmentInsertCommand request, CancellationToken cancellationToken)
        {
            DateTime outDate;
            if (!DateTime.TryParse(request.DueDate, out outDate))
            {
                return new FailureResult("Invalid Due Date");
            }

            var entity = this.Mapper.Map<AssignmentInsertCommand, Assignment>(request);

            ICommandResult result = this.validate(entity);
            if (result.IsFailure) return result;

            result = this.validatePropjectId(entity.ProjectId);
            if (result.IsFailure) return result;

            await this.AssignmentRepository.InsertAsync(entity);

            await this.AssignmentRepository.CommitAsync();

            return new SuccessResult(entity.Id);
        }

        public async Task<ICommandResult> Handle(AssignmentUpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = this.Mapper.Map<AssignmentUpdateCommand, Assignment>(request);

            ICommandResult result = this.validate(entity);
            if (result.IsFailure) return result;

            result = this.validateId(entity);
            if (result.IsFailure) return result;

            result = this.validatePropjectId(entity.ProjectId);
            if (result.IsFailure) return result;

            await this.AssignmentRepository.UpdateAsync(entity);

            await this.AssignmentRepository.CommitAsync();

            return new SuccessResult();
        }

        public async Task<ICommandResult> Handle(AssignmentDeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await this.AssignmentRepository.GetAsync(request.Id);

            if (entity == null)
            {
                return new FailureResult { Result = "Task not founded" };
            }

            ICommandResult result = this.validateId(entity);
            if (result.IsFailure) return result;

            await this.AssignmentRepository.DeleteAsync(entity);

            await this.AssignmentRepository.CommitAsync();

            return new SuccessResult();
        }

        private ICommandResult validate(Assignment entity)
        {
            ICommandResult result = new FailureResult();
            StringRequiredSpec stringRequired = new StringRequiredSpec(nameof(entity.Description));
            if (!stringRequired.IsSatisfiedBy(entity.Description))
            {
                result.Result = stringRequired.Description;
                return result;
            }

            stringRequired = new StringRequiredSpec(nameof(entity.User));
            if (!stringRequired.IsSatisfiedBy(entity.User))
            {
                result.Result = stringRequired.Description;
                return result;
            }

            MaxLenghtSpec maxLenghtSpec = new MaxLenghtSpec(nameof(entity.Description), 250);
            if (!maxLenghtSpec.IsSatisfiedBy(entity.Description))
            {
                result.Result = maxLenghtSpec.Description;
                return result;
            }

            maxLenghtSpec = new MaxLenghtSpec(nameof(entity.User), 100);
            if (!maxLenghtSpec.IsSatisfiedBy(entity.User))
            {
                result.Result = maxLenghtSpec.Description;
                return result;
            }

            DateTimeMoreThanMinSpec dateTimeMoreThanMinSpec = new DateTimeMoreThanMinSpec();
            if (!dateTimeMoreThanMinSpec.IsSatisfiedBy(entity.DueDate))
            {
                result.Result = dateTimeMoreThanMinSpec.Description;
                return result;
            }

            return result;
        }

        private ICommandResult validateId(Assignment entity)
        {
            ICommandResult result = new FailureResult();
            NumberMoreThanZero stringRequired = new NumberMoreThanZero(nameof(entity.Id));
            if (!stringRequired.IsSatisfiedBy(entity.Id))
            {
                result.Result = stringRequired.Description;
            }

            return result;
        }

        private ICommandResult validatePropjectId(int projectId)
        {
            ICommandResult result = new FailureResult();

            var project = this.ProjectRepository.GetAsync(projectId).Result;

            if (project == null)
            {
                result.Result = "Project not founded.";
            }

            return result;
        }
    }
}
