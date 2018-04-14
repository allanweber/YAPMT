using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace YAPMT.Framework.Specifications
{
    public class MaxLenghtSpec : BaseSpecification<string>
    {
        public MaxLenghtSpec(string fieldName, int maxLenght)
        {
            this.FieldName = fieldName;
            this.MaxLenght = maxLenght;
        }

        private string FieldName { get; set; }
        private int MaxLenght { get; set; }

        public override string Description
            => $"The field {this.FieldName} must not be greather than {this.MaxLenght} chars";

        protected override Expression<Func<string, bool>> GetFinalExpression()
            => fieldValue => fieldValue.Length <= this.MaxLenght;
    }
}
