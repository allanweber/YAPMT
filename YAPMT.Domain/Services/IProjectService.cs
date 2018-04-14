using System.Threading.Tasks;
using YAPMT.Domain.Dtos;
using YAPMT.Framework.Services;

namespace YAPMT.Domain.Services
{
    public interface IProjectService: IService
    {
        Task<ProjectStatusDto> GetProjectStatus(int projectId);
    }
}
