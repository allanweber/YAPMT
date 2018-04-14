using MediatR;
using System;
using YAPMT.Framework.CommandHandlers;

namespace YAPMT.Domain.CommandHandlers.Commands.Assignment
{
    public class AssignmentInsertCommand: IRequest<ICommandResult>
    {
        public string Description { get; set; }

        public string User { get; set; }

        public DateTime DueDate { get; set; }

        public bool Completed { get; set; }

        public int ProjectId { get; set; }
    }
}
