using YAPMT.Framework.Entities;

namespace YAPMT.Domain.Entities
{
    public class Project : BaseEntity
    {
        public Project()
        {

        }

        public Project(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}
