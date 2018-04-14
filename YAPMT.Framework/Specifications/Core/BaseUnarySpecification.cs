using System;

namespace YAPMT.Framework.Specifications
{
    public abstract class BaseUnarySpecification<TData> : BaseSpecification<TData>
    {
        protected BaseUnarySpecification(BaseSpecification<TData> specification)
        {
            this.Specification = specification ?? throw new ArgumentNullException(nameof(specification));
        }

        public BaseSpecification<TData> Specification { get; }
    }
}
