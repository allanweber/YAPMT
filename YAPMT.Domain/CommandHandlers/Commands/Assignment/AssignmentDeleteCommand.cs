using MediatR;
using YAPMT.Framework.CommandHandlers;
using YAPMT.Framework.Entities;

namespace YAPMT.Domain.CommandHandlers.Commands.Assignment
{
    public class AssignmentDeleteCommand : BaseEntity, IRequest<ICommandResult>
    {
    }
}
