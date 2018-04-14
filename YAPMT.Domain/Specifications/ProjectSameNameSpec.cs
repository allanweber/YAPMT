using System;
using System.Linq.Expressions;
using YAPMT.Domain.Entities;
using YAPMT.Framework.Specifications;

namespace YAPMT.Domain.Specifications
{
    public class ProjectSameNameSpec: BaseSpecification<Project>
    {
        public ProjectSameNameSpec(Project project)
        {
            this.Project = project;
        }

        public override string Description => $"Already exist a project with name {Project.Name}";

        public Project Project { get; }

        protected override Expression<Func<Project, bool>> GetFinalExpression()
            => project => project.Name == this.Project.Name && project.Id != this.Project.Id;
    }
}
