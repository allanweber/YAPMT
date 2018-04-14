using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YAPMT.Domain.Entities;
using YAPMT.Domain.Exceptions;
using YAPMT.Domain.Repositories;
using YAPMT.Domain.Specifications;
using YAPMT.Framework.Repositories;

namespace YAPMT.Infrastructure.Repositories
{
    public class AssignmentRepository : Repository<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(PrincipalDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<long> CountByProject(int projectId)
        {
            AssignmentsByProjectId spec = new AssignmentsByProjectId(projectId);

            return await this.CountAsync(spec);
        }

        public async Task<long> CountCompletedByProject(int projectId)
        {
            AssignmentsDoneByProjectId spec = new AssignmentsDoneByProjectId(projectId);

            return await this.CountAsync(spec);
        }

        public async Task<long> CountLateByProject(int projectId)
        {
            AssignmentsLateByProjectId spec = new AssignmentsLateByProjectId(projectId);

            return await this.CountAsync(spec);
        }

        public async Task<DateTime> DoneAssignment(int assignmentId)
        {
            var entity = await this.GetAsync(assignmentId);

            if (entity == null)
                throw new NotFoundedAssignmentException();

            entity.Done();

            await this.UpdateAsync(entity);

            await this.CommitAsync();

            return entity.DoneDate;
        }

        public async Task<IEnumerable<Assignment>> GetAllByProject(int projectId)
        {
            AssignmentsByProjectId spec = new AssignmentsByProjectId(projectId);

            return await this.QueryAsync(spec);
        }
    }
}
