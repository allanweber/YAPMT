using System;
using System.Linq.Expressions;

namespace YAPMT.Framework.Specifications
{
    public class GenericSpecification<TData> : BaseSpecification<TData>
    {
        private readonly Expression<Func<TData, bool>> expression;

        public GenericSpecification(string description, Expression<Func<TData, bool>> expression)
            : this(expression)
        {
            this.Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public GenericSpecification(Expression<Func<TData, bool>> expression)
        {
            this.expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public override string Description { get; }

        protected override Expression<Func<TData, bool>> GetFinalExpression()
        {
            return this.expression;
        }
    }
}
