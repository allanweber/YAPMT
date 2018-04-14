using System;

namespace YAPMT.Framework.Specifications
{
    public abstract class BaseBinarySpecification<TData> : BaseSpecification<TData>
    {
        protected BaseBinarySpecification(BaseSpecification<TData> leftSpecification, BaseSpecification<TData> rightSpecification)
        {
            this.LeftSpecification = leftSpecification ?? throw new ArgumentNullException(nameof(leftSpecification));
            this.RightSpecification = rightSpecification ?? throw new ArgumentNullException(nameof(rightSpecification));
        }

        public BaseSpecification<TData> LeftSpecification { get; }

        public BaseSpecification<TData> RightSpecification { get; }
    }
}
