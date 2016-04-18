using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace CAPS.CORPACCOUNTING.GenericSearch
{
    public class NumericSearch : AbstractSearch
    {
        public long? SearchTerm { get; set; }
        public long? SearchTerm2 { get; set; }
        public string SearchTerms { get; set; }

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
            if (!string.IsNullOrEmpty(SearchTerms) && this.Comparator == NumericComparators.In)
            {
                Expression combinedExpression = null;

                foreach (string val in SearchTerms.Split(','))
                {
                    // this.Comparator = NumericComparators.In;
                    this.SearchTerm = Convert.ToInt64(val);
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
            Expression rightExp = null;
            switch (property.Type.Name)
            {
                case "Int32":
                    rightExp = Expression.Constant(Convert.ToInt32(this.SearchTerm.Value), typeof(Int32));
                    break;
                case "Int16":
                    rightExp = Expression.Constant(Convert.ToInt16(this.SearchTerm.Value), typeof(Int16));
                    break;
                default:
                    rightExp = Expression.Constant(this.SearchTerm.Value);
                    break;
            }

            switch (this.Comparator)
            {
                case NumericComparators.Less:
                    return Expression.LessThan(property, rightExp);
                case NumericComparators.LessOrEqual:
                    return Expression.LessThanOrEqual(property, rightExp);
                case NumericComparators.Equal:
                    return Expression.Equal(property, rightExp);
                case NumericComparators.GreaterOrEqual:
                    return Expression.GreaterThanOrEqual(property, rightExp);
                case NumericComparators.Greater:
                    return Expression.GreaterThan(property, rightExp);
                case NumericComparators.InRange:
                    return Expression.GreaterThanOrEqual(property, rightExp);
                case NumericComparators.In:
                    return Expression.Equal(property, rightExp);
                default:
                    throw new InvalidOperationException("Comparator not supported.");
            }
        }
    }

    public enum NumericComparators
    {
        [Display(Name = "<")]
        Less,

        [Display(Name = "<=")]
        LessOrEqual,

        [Display(Name = "==")]
        Equal,

        [Display(Name = ">=")]
        GreaterOrEqual,

        [Display(Name = ">")]
        Greater,
        [Display(Name = "Range")]
        InRange,

        [Display(Name = "In")]
        In
    }
}
