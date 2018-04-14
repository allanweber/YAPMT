using MediatR;
using YAPMT.Framework.CommandHandlers;
using YAPMT.Framework.Entities;

namespace YAPMT.Domain.CommandHandlers.Commands.Project
{
    public class ProjectDeleteCommand: BaseEntity, IRequest<ICommandResult>
    {
    }
}
