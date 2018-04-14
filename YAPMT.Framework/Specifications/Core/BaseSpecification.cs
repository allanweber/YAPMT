using System;
using System.Linq.Expressions;

namespace YAPMT.Framework.Specifications
{
    public abstract class BaseSpecification<TData>
    {
        public abstract string Description { get; }

        public Expression<Func<TData, bool>> Expression => this.GetFinalExpression();

        public BaseSpecification<TData> And(BaseSpecification<TData> specification)
        {
            return new AndSpecification<TData>(this, specification);
        }

        public BaseSpecification<TData> Or(BaseSpecification<TData> specification)
        {
            return new OrSpecification<TData>(this, specification);
        }

        public BaseSpecification<TData> Not()
        {
            return new NotSpecification<TData>(this);
        }

        public virtual bool IsSatisfiedBy(TData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var predicate = this.Expression.Compile();

            return predicate(data);
        }

        protected abstract Expression<Func<TData, bool>> GetFinalExpression();
    }
}
