using System;
using System.Linq.Expressions;

namespace CAPS.CORPACCOUNTING.GenericSearch
{
    public class DecimalSearch : AbstractSearch
    {
        public decimal? SearchTerm { get; set; }
        public decimal? SearchTerm2 { get; set; }

        public string SearchTerms { get; set; }
        public NumericComparators Comparator { get; set; }        
        protected override Expression BuildExpression(MemberExpression property)
        {
            Expression searchExpression1 = null;
            Expression searchExpression2 = null;
            if (!string.IsNullOrEmpty(SearchTerms) && this.Comparator == NumericComparators.In)
            {
                Expression combinedExpression = null;

                foreach (string val in SearchTerms.Split(','))
                {
                    // this.Comparator = NumericComparators.In;
                    this.SearchTerm = Convert.ToDecimal(val);
                    searchExpression1 = this.GetFilterExpression(property);
                    if (!ReferenceEquals(searchExpression1, null) && !ReferenceEquals(searchExpression2, null))
                    {

                        combinedExpression = Expression.OrElse(searchExpression2, searchExpression1);
                        searchExpression1 = combinedExpression;
                    }
                    searchExpression2 = searchExpression1;
                }
                return combinedExpression;
            }
            else
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
                case NumericComparators.In:
                    return Expression.Equal(property, Expression.Constant(this.SearchTerm.Value));
                default:
                    throw new InvalidOperationException("Comparator not supported.");
            }
        }
    }    
}
