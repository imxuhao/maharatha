using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Helpers
{
    public enum SearchPattern
    {
        Or,
        And
    }
    /// <summary>
    /// http://www.codeproject.com/Tips/582450/Build-Where-Clause-Dynamically-in-Linq
    /// </summary>
    public static class ExpressionBuilder
    {
        private static MethodInfo containsMethod = typeof(string).GetMethod("Contains");
        private static MethodInfo startsWithMethod =
        typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
        private static MethodInfo endsWithMethod =
        typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
        private static MethodInfo stringRangeWithMethod =
     typeof(string).GetMethod("CompareTo", new[] { typeof(string) });


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filters"></param>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> GetExpression<T>(IList<Filters> filters, SearchPattern searchPattern = SearchPattern.And)
        {
            if (filters.Count == 0)
                return null;

            ParameterExpression param = Expression.Parameter(typeof(T), "t");
            Expression exp = null;

            if (filters.Count == 1)
                exp = GetExpression<T>(param, filters[0]);
            else if (filters.Count == 2)
                exp = GetExpression<T>(param, filters[0], filters[1],searchPattern);
            else
            {
                while (filters.Count > 0)
                {
                    var f1 = filters[0];
                    var f2 = filters[1];

                    if (exp == null)
                        exp = GetExpression<T>(param, filters[0], filters[1], searchPattern);
                    else
                        if (searchPattern == SearchPattern.Or)
                        exp = Expression.Or(exp, GetExpression<T>(param, filters[0], filters[1], searchPattern));
                    else
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1], searchPattern));

                    filters.Remove(f1);
                    filters.Remove(f2);

                    if (filters.Count == 1)
                    {
                        if (searchPattern == SearchPattern.Or)
                            exp = Expression.Or(exp, GetExpression<T>(param, filters[0]));
                        else
                            exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));
                        filters.RemoveAt(0);
                    }
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        private static Expression GetExpression<T>(ParameterExpression param, Filters filter)
        {
            try
            {


                MemberExpression member = Expression.Property(param, filter.Property);
                Expression constant = null;
                if (filter.Comparator != 6)
                     constant = Expression.Convert(((ConstantExpression)GetFilterExpression(member, filter.SearchTerm)), member.Type);
                Comparators value;
                Expression searchExpression1 = null;
                Expression searchExpression2 = null;

                //IN Operator
                if (filter.Comparator == 6)
                {
                    Expression combinedExpression = null;

                    foreach (string val in filter.SearchTerm.Split(','))
                    {
                        searchExpression1 = Expression.Equal(member, Expression.Convert((GetFilterExpression(member, val)), member.Type));
                        if (!ReferenceEquals(searchExpression1, null) && !ReferenceEquals(searchExpression2, null))
                        {
                            combinedExpression = Expression.OrElse(searchExpression2, searchExpression1);
                            searchExpression1 = combinedExpression;
                        }
                        searchExpression2 = searchExpression1;
                        if (filter.SearchTerm.Split(',').Length == 1)
                            combinedExpression = searchExpression2;
                    }
                    return combinedExpression;
                }
                else
                if (filter.DataType == DataTypes.Text && filter.Comparator == 0)
                {
                    value = Comparators.Contains;
                }
                else
                if (filter.DataType == DataTypes.Bool)
                {
                    return Expression.Equal(member, Expression.Constant(Convert.ToBoolean(filter.SearchTerm)));
                }
                else
                { value = EnumHelper.GetEnumValue<Comparators>(filter.Comparator); }



                switch (value)
                {
                    case Comparators.Equal:
                        return Expression.Equal(member, constant);

                    case Comparators.Greater:
                        return Expression.GreaterThan(member, constant);

                    case Comparators.GreaterOrEqual:
                        return Expression.GreaterThanOrEqual(member, constant);

                    case Comparators.Less:
                        return Expression.LessThan(member, constant);

                    case Comparators.LessOrEqual:
                        return Expression.LessThanOrEqual(member, constant);

                    case Comparators.Contains:
                        return Expression.Call(member, containsMethod, constant);

                    case Comparators.StartsWith:
                        return Expression.Call(member, startsWithMethod, constant);

                    case Comparators.EndsWith:
                        return Expression.Call(member, endsWithMethod, constant);
                    case Comparators.InRange:
                        {
                            if (filter.DataType == DataTypes.Text)
                            {
                                searchExpression1 = Expression.GreaterThanOrEqual(Expression.Call(member, stringRangeWithMethod, constant), Expression.Constant(0, typeof(int)));
                                searchExpression2 = Expression.LessThanOrEqual(Expression.Call(member, stringRangeWithMethod, GetFilterExpression(member, filter.SearchTerm2)), Expression.Constant(0, typeof(int)));
                            }
                            else
                            {
                                searchExpression1 = Expression.GreaterThanOrEqual(member, constant);
                                searchExpression2 = Expression.LessThanOrEqual(member, GetFilterExpression(member, filter.SearchTerm2));
                            }
                            return Expression.AndAlso(searchExpression1, searchExpression2);
                        }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return null;
        }

        private static BinaryExpression GetExpression<T>
        (ParameterExpression param, Filters filter1, Filters filter2, SearchPattern searchPattern = SearchPattern.And)
        {
            Expression bin1 = GetExpression<T>(param, filter1);
            Expression bin2 = GetExpression<T>(param, filter2);
            if (searchPattern == SearchPattern.Or)
                return Expression.Or(bin1, bin2);
            else
                return Expression.AndAlso(bin1, bin2);

        }
        private static Expression GetFilterExpression(MemberExpression property, string value)
        {
            Expression constantExp;
            string dataType = string.Empty;

            dataType = IsNullableType(property.Type) ? Nullable.GetUnderlyingType(property.Type).Name : property.Type.Name;

            switch (dataType)
            {
                case "Int32":
                    constantExp = Expression.Constant(Convert.ToInt32(value), typeof(Int32));
                    break;
                case "Int16":
                    constantExp = Expression.Constant(Convert.ToInt16(value), typeof(Int16));
                    break;
                case "Int64":
                    constantExp = Expression.Constant(Convert.ToInt64(value), typeof(Int64));
                    break;
                case "String":
                    constantExp = Expression.Constant(value, typeof(string));
                    break;
                case "Boolean":
                    constantExp = Expression.Constant(Convert.ToBoolean(value), typeof(bool));
                    break;
                default:
                    constantExp = Expression.Constant(value);
                    break;

            }
            return constantExp;
        }
        /// <summary>
        /// 
        /// </summary>
        public enum Comparators
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
            In,
            [Display(Name = "StartsWith")]
            StartsWith,
            [Display(Name = "EndsWith")]
            EndsWith,
            [Display(Name = "Contains")]
            Contains
        }

        static bool IsNullableType(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
