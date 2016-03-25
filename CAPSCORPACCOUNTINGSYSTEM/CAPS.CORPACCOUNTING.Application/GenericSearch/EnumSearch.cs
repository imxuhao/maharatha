using System;
using System.Linq.Expressions;

namespace CAPS.CORPACCOUNTING.GenericSearch
{
    public class EnumSearch : AbstractSearch
    {
        public string SearchTerm { get; set; }

        public Type EnumType
        {
            get
            {
                return Type.GetType(this.EnumTypeName);
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

            var enumValue = Enum.Parse(this.EnumType, this.SearchTerm);

            Expression searchExpression = Expression.Equal(property, Expression.Constant(enumValue));

            return searchExpression;
        }
    }
}
