using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace YAPMT.Framework.Specifications
{
    public class NumberMoreThanZero : BaseSpecification<int>
    {
        public NumberMoreThanZero(string fieldName)
        {
            this.FieldName = fieldName;
        }

        public override string Description =>
            $"The field {this.FieldName} must be more than zero.";

        public string FieldName { get; }

        protected override Expression<Func<int, bool>> GetFinalExpression()
            => intValue => intValue > 0;
    }
}
