using System;
using System.Linq.Expressions;

namespace YAPMT.Framework.Specifications
{
    public sealed class NotSpecification<TData> : BaseUnarySpecification<TData>
    {
        public NotSpecification(BaseSpecification<TData> specification)
            : base(specification)
        {
        }

        public override string Description => $"NÃO {this.Specification.Description}";

        protected override Expression<Func<TData, bool>> GetFinalExpression()
        {
            var parameterExpression = System.Linq.Expressions.Expression.Parameter(typeof(TData));

            var binaryExpression = System.Linq.Expressions.Expression.Not(this.Specification.Expression.Body);

            binaryExpression = ParameterReplacerExpressionVisitor.VisitExpression(binaryExpression, parameterExpression);

            return System.Linq.Expressions.Expression.Lambda<Func<TData, bool>>(binaryExpression, parameterExpression);
        }
    }
}
