using System;
using System.Linq.Expressions;

namespace YAPMT.Framework.Specifications
{
    public sealed class AndSpecification<TData> : BaseBinarySpecification<TData>
    {
        public AndSpecification(BaseSpecification<TData> leftSpecification, BaseSpecification<TData> rightSpecification)
            : base(leftSpecification, rightSpecification)
        {
        }

        public override string Description => $"{this.LeftSpecification.Description} E {this.RightSpecification.Description}";

        protected override Expression<Func<TData, bool>> GetFinalExpression()
        {
            var parameterExpression = System.Linq.Expressions.Expression.Parameter(typeof(TData));

            var binaryExpression = System.Linq.Expressions.Expression.AndAlso(
                this.LeftSpecification.Expression.Body,
                this.RightSpecification.Expression.Body);

            binaryExpression = ParameterReplacerExpressionVisitor.VisitExpression(binaryExpression, parameterExpression);

            return System.Linq.Expressions.Expression.Lambda<Func<TData, bool>>(binaryExpression, parameterExpression);
        }
    }
}
