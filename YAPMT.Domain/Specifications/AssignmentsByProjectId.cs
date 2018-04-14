﻿using System;
using System.Linq.Expressions;
using YAPMT.Domain.Entities;
using YAPMT.Framework.Specifications;

namespace YAPMT.Domain.Specifications
{
    public class AssignmentsByProjectId : BaseSpecification<Assignment>
    {
        public AssignmentsByProjectId(int projectId)
        {
            ProjectId = projectId;
        }

        public override string Description => string.Empty;

        public int ProjectId { get; }

        protected override Expression<Func<Assignment, bool>> GetFinalExpression()
            => assignment => assignment.ProjectId == this.ProjectId;
    }
}
