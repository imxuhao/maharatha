using System.Collections.Generic;
using System.Text;

namespace CAPS.CORPACCOUNTING.Helper
{
    public class Helper
    {
        /// <summary>
        /// To get formatted sorting string 
        /// </summary>
        /// <param name="sortList"></param>
        /// <returns></returns>
        public static string GetSortOrderAsString(SortList sortList)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var sortItem in sortList.Sort)
            {
                sb.Append((sortItem.ParentTableName.Trim().Length > 0 ? (sortItem.ParentTableName + ".") : " ") + sortItem.ColumnName + " " + sortItem.Order + ",");
            }
            return sb.ToString().TrimEnd(',');
        }

    }   
    /// <summary>
    /// 
    /// </summary>
    public class SortList
    {
        public List<Sort> Sort { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class Sort
    {
        /// <summary>
        /// Gets or sets Column ParentTable Name
        /// </summary>
        public string ParentTableName { get; set; }

        /// <summary>
        /// Gets or sets Column Name
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// Gets or sets Sorting Order
        /// </summary>
        public string Order { get; set; }
    }
   
    

}
