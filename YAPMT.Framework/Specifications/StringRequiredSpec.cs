using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace YAPMT.Framework.Specifications
{
    public class StringRequiredSpec : BaseSpecification<string>
    {
        public StringRequiredSpec(string fieldName)
        {
            FieldName = fieldName;
        }

        public string FieldName { get; }

        public override string Description
            => $"The field {this.FieldName} must be informed.";

        protected override Expression<Func<string, bool>> GetFinalExpression()
            => stringValue => !string.IsNullOrWhiteSpace(stringValue);
    }
}
