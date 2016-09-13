using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq.Expressions;

namespace CAPS.CORPACCOUNTING.GenericSearch
{
    public class DateSearch : AbstractSearch
    {
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? SearchTerm { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? SearchTerm2 { get; set; }

        public DateComparators Comparator { get; set; }

        protected override Expression BuildExpression(MemberExpression property)
        {
            Expression searchExpression1 = null;
            Expression searchExpression2 = null;

            if (this.SearchTerm.HasValue)
            {
                searchExpression1 = this.GetFilterExpression(property);
            }

            if (this.Comparator == DateComparators.InRange && this.SearchTerm2.HasValue)
            {
                searchExpression2 = Expression.LessThanOrEqual(LeftExpressionTruncateTime(property), RightExpressionTruncateTime(this.SearchTerm2.Value));
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
            Expression leftExp = LeftExpressionTruncateTime(property);
           Expression rightExp= RightExpressionTruncateTime(this.SearchTerm.Value);
            switch (this.Comparator)
            {
                case DateComparators.Less:
                    return Expression.LessThan(leftExp, rightExp);
                case DateComparators.LessOrEqual:
                    return Expression.LessThanOrEqual(leftExp, rightExp);
                case DateComparators.Equal:
                    return Expression.Equal(leftExp, rightExp);
                case DateComparators.GreaterOrEqual:
                case DateComparators.InRange:
                    return Expression.GreaterThanOrEqual(leftExp, rightExp);
                case DateComparators.Greater:
                    return Expression.GreaterThan(leftExp, rightExp);
                default:
                    throw new InvalidOperationException("Comparator not supported.");
            }
        }

        private Expression LeftExpressionTruncateTime(MemberExpression property)
        {
            return Expression.Call(
                                typeof(DbFunctions),
                                "TruncateTime",
                                Type.EmptyTypes,
                                Expression.Convert(property, typeof(DateTime?)));
        }

        private Expression RightExpressionTruncateTime(DateTime? value)
        {
            return Expression.Call(
                                typeof(DbFunctions),
                                "TruncateTime",
                                Type.EmptyTypes,
                                Expression.Convert(Expression.Constant(value), typeof(DateTime?)));
        }
    }

    public enum DateComparators
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
        InRange
    }
}
