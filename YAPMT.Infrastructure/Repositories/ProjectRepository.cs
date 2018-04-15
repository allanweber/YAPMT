using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public override async Task<List<Project>> GetAllAsync()
        {
            return await this.Query().OrderBy(p => p.Id).ToListAsync();
        }
    }
}
