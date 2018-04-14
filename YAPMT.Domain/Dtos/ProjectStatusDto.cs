using YAPMT.Framework.Dtos;

namespace YAPMT.Domain.Dtos
{
    public class ProjectStatusDto: IDto
    {
        public long Completed { get; set; }

        public long Late { get; set; }

        public long Total { get; set; }
    }
}
