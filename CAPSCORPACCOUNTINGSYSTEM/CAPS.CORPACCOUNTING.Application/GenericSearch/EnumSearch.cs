using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace CAPS.CORPACCOUNTING.GenericSearch
{
    public class EnumSearch : AbstractSearch
    {
        public string SearchTerm { get; set; }

        public EnumComparators Comparator { get; set; }

        public Type EnumType
        {
            get
            {
                if (!string.IsNullOrEmpty(this.EnumTypeName))
                    return Type.GetType(this.EnumTypeName);
                else
                    return null;

            }
        }

        public string EnumTypeName { get; set; }

        public EnumSearch()
        {
        }

        public EnumSearch(Type enumType)
        {
            this.EnumTypeName = enumType.AssemblyQualifiedName;
        }

        protected override System.Linq.Expressions.Expression BuildExpression(System.Linq.Expressions.MemberExpression property)
        {
            if (string.IsNullOrEmpty(this.SearchTerm))
            {
                return null;
            }
            object enumValue = null;

            Expression searchExpression = null;
            Expression searchExpression1 = null;
            Expression searchExpression2 = null;

            if (this.Comparator == EnumComparators.In)
            {
                Expression combinedExpression = null;
                foreach (string val in SearchTerm.Split(','))
                {


                    if (!ReferenceEquals(this.EnumType, null))
                        enumValue = Enum.Parse(this.EnumType, val);
                    else
                        enumValue = Enum.Parse(property.Type, val);
                    searchExpression1 = Expression.Equal(property, Expression.Constant(enumValue));
                    if (!ReferenceEquals(searchExpression1, null) && !ReferenceEquals(searchExpression2, null))
                    {
                        combinedExpression = Expression.OrElse(searchExpression2, searchExpression1);
                        searchExpression1 = combinedExpression;
                    }
                    searchExpression2 = searchExpression1;
                }
                return combinedExpression;
            }
            else {

                if (!ReferenceEquals(this.EnumType, null))
                    enumValue = Enum.Parse(this.EnumType, this.SearchTerm);
                else
                    enumValue = Enum.Parse(property.Type, this.SearchTerm);

                searchExpression = Expression.Equal(property, Expression.Constant(enumValue));
            }

             

            return searchExpression;
        }

        public enum EnumComparators
        {
            [Display(Name = "==")]
            Equals = 2,

            [Display(Name = "In")]
            In = 6
        }
    }
}
