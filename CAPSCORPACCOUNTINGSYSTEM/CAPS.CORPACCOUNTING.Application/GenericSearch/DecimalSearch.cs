using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace CAPS.CORPACCOUNTING.GenericSearch
{
    public class DecimalSearch : AbstractSearch
    {
        public decimal? SearchTerm { get; set; }
        public decimal? SearchTerm2 { get; set; }
        public NumericComparators Comparator { get; set; }

        //protected override Expression BuildExpression(MemberExpression property)
        //{
        //    if (!this.SearchTerm.HasValue)
        //    {
        //        return null;
        //    }

        //    Expression searchExpression = this.GetFilterExpression(property);

        //    return searchExpression;
        //}

        protected override Expression BuildExpression(MemberExpression property)
        {
            Expression searchExpression1 = null;
            Expression searchExpression2 = null;

            if (this.SearchTerm.HasValue)
            {
                searchExpression1 = this.GetFilterExpression(property);
            }

            if (this.Comparator == NumericComparators.InRange && this.SearchTerm2.HasValue)
            {
                searchExpression2 = Expression.LessThanOrEqual(property, Expression.Constant(this.SearchTerm2.Value));
            }

            if (searchExpression1 == null && searchExpression2 == null)
            {
                return null;
            }
            else if (searchExpression1 != null && searchExpression2 != null)
            {
                var combinedExpression = Expression.AndAlso(searchExpression1, searchExpression2);
                return combinedExpression;
            }
            else if (searchExpression1 != null)
            {
                return searchExpression1;
            }
            else
            {
                return searchExpression2;
            }
        }

        private Expression GetFilterExpression(MemberExpression property)
        {
            switch (this.Comparator)
            {
                case NumericComparators.Less:
                    return Expression.LessThan(property, Expression.Constant(this.SearchTerm.Value));
                case NumericComparators.LessOrEqual:
                    return Expression.LessThanOrEqual(property, Expression.Constant(this.SearchTerm.Value));
                case NumericComparators.Equal:
                    return Expression.Equal(property, Expression.Constant(this.SearchTerm.Value));
                case NumericComparators.GreaterOrEqual:
                    return Expression.GreaterThanOrEqual(property, Expression.Constant(this.SearchTerm.Value));
                case NumericComparators.Greater:
                    return Expression.GreaterThan(property, Expression.Constant(this.SearchTerm.Value));
                case NumericComparators.InRange:
                    return Expression.GreaterThanOrEqual(property, Expression.Constant(this.SearchTerm.Value));
                default:
                    throw new InvalidOperationException("Comparator not supported.");
            }
        }
    }    
}
