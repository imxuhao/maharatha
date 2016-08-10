using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace CAPS.CORPACCOUNTING.GenericSearch
{
    public class TextSearch : AbstractSearch
    {
        public string SearchTerm { get; set; }
        public TextComparators Comparator { get; set; }
        protected override Expression BuildExpression(MemberExpression property)
        {
            Expression searchExpression = null;
            Expression searchExpression1 = null;
            Expression searchExpression2 = null;
            if (string.IsNullOrEmpty(this.SearchTerm))
            {
                return null;
            }
            if (this.Comparator == TextComparators.In)
            {
                Expression combinedExpression = null;
                foreach (string val in SearchTerm.Split(','))
                {
                    searchExpression1 = Expression.Equal(property, Expression.Constant(val));
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
                searchExpression = Expression.Call(
                   property,
                   typeof(string).GetMethod(this.Comparator.ToString(), new[] { typeof(string) }),
                   Expression.Constant(this.SearchTerm));
            }
            return searchExpression;
        }
    }

    public enum TextComparators
    {
        [Display(Name = "Contains")]
        Contains,

        [Display(Name = "==")]
        Equals=2,
        [Display(Name = "Range")]
        InRange=5,

        [Display(Name = "In")]
        In=6
    }
}
