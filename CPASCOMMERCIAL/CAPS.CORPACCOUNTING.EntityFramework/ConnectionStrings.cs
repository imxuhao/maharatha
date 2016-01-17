using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING
{
    static class ConnectionStrings
    {
        static Dictionary<int, string> _dict = new Dictionary<int, string>
    {
            // Store the Connection string based on your Tenant. This is the TenantID and Connection string dictionary.

                {1, @"Server =.\cpascorp; Database = CORPACCOUNTING1; Trusted_Connection = True;"},
                {2, @"Server =.\cpascorp; Database = CORPACCOUNTING1; Trusted_Connection = True;"},
                {3, @"Server =.\cpascorp; Database = CORPACCOUNTING1; Trusted_Connection = True;"},
                {4, @"Server =.\cpascorp; Database = CORPACCOUNTING1; Trusted_Connection = True;"},
                {5, @"Server =.\cpascorp; Database = CORPACCOUNTING2; Trusted_Connection = True;"},
                {6, @"Server =.\cpascorp; Database = CORPACCOUNTING1; Trusted_Connection = True;"},
                {7, @"Server =.\cpascorp; Database = CORPACCOUNTING1; Trusted_Connection = True;"},
                {8, @"Server =.\cpascorp; Database = CORPACCOUNTING1; Trusted_Connection = True;"},
                {9, @"Server =.\cpascorp; Database = CORPACCOUNTING1; Trusted_Connection = True;"},
                {10, @"Server =.\cpascorp; Database = CORPACCOUNTING1; Trusted_Connection = True;"}
    };

        /// <summary>
        /// Access the Dictionary from external sources
        /// </summary>
        public static string GetConnectionString(int intTenantID)
        {
            // Try to get the result in the static Dictionary
            string result;
            if (_dict.TryGetValue(intTenantID, out result))
            {
                return result;
            }
            else
            {
                return @"Server =.\cpascorp; Database = CORPACCOUNTING1; Trusted_Connection = True;";
            }
        }
    }
}
