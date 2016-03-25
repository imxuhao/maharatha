using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace CAPS.CORPACCOUNTING.GenericSearch
{
    public class BooleanSearch : AbstractSearch
    {
        public bool? SearchTerm { get; set; }

        protected override Expression BuildExpression(MemberExpression property)
        {
            if (!this.SearchTerm.HasValue)
            {
                return null;
            }

            Expression searchExpression = this.GetFilterExpression(property);

            return searchExpression;
        }
        private Expression GetFilterExpression(MemberExpression property)
        {
            return Expression.Equal(property, Expression.Constant(this.SearchTerm.Value));
        }
    }
}
