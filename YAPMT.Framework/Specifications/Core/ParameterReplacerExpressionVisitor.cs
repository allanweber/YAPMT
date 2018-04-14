using System;
using System.Linq.Expressions;

namespace YAPMT.Framework.Specifications
{
    internal sealed class ParameterReplacerExpressionVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression parameterExpression;

        public ParameterReplacerExpressionVisitor(ParameterExpression parameterExpression)
        {
            this.parameterExpression = parameterExpression ?? throw new ArgumentNullException(nameof(parameterExpression));
        }

        public static TExpression VisitExpression<TExpression>(TExpression nodeExpression, ParameterExpression parameterExpression)
            where TExpression : Expression
        {
            if (nodeExpression == null)
                throw new ArgumentNullException(nameof(nodeExpression));

            if (parameterExpression == null)
                throw new ArgumentNullException(nameof(parameterExpression));

            return (TExpression)new ParameterReplacerExpressionVisitor(parameterExpression)
                .Visit(nodeExpression);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(this.parameterExpression);
        }
    }
}
