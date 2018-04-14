using YAPMT.Framework.CommandHandlers;
using MediatR;

namespace YAPMT.Domain.CommandHandlers.Commands.Project
{
    public class ProjectInsertCommand: IRequest<ICommandResult>
    {
        public string Name { get; set; }
    }
}
