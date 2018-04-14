using YAPMT.Domain.Entities;
using YAPMT.Domain.Repositories;
using YAPMT.Framework.Repositories;

namespace YAPMT.Infrastructure.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(PrincipalDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
