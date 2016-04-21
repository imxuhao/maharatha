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
            if (!ReferenceEquals(this.EnumType, null))
                 enumValue = Enum.Parse(this.EnumType, this.SearchTerm);
            else
                 enumValue = Enum.Parse(property.Type, this.SearchTerm);

            Expression searchExpression = Expression.Equal(property, Expression.Constant(enumValue));

            return searchExpression;
        }
    }
}
