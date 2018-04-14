using System;
using System.Linq.Expressions;

namespace YAPMT.Framework.Specifications
{
    public class DateTimeMoreThanMinSpec : BaseSpecification<DateTime>
    {
        public override string Description => $"The date must be greather than {DateTime.MinValue}";

        protected override Expression<Func<DateTime, bool>> GetFinalExpression()
        => date => date > DateTime.MinValue;
    }
}
