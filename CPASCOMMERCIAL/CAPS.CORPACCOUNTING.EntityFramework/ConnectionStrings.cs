using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING
{
    static class ConnectionStrings
    {
        static readonly Dictionary<int, string> Dict = new Dictionary<int, string>
    {
            // Store the Connection string based on your Tenant. This is the TenantID and Connection string dictionary.

                {1, @"Server =.\cpascorp; Database = CORPACCOUNTING1; Trusted_Connection = True;"},
                {2, @"Server =.\cpascorp; Database = CORPACCOUNTING10; Trusted_Connection = True;"},
                {3, @"Server =.\cpascorp; Database = CORPACCOUNTING10; Trusted_Connection = True;"},
                {4, @"Server =.\cpascorp; Database = CORPACCOUNTING1; Trusted_Connection = True;"},
                {5, @"Server =.\cpascorp; Database = CORPACCOUNTING10; Trusted_Connection = True;"},
                {6, @"Server =.\cpascorp; Database = CORPACCOUNTING1; Trusted_Connection = True;"},
                {7, @"Server =.\cpascorp; Database = CORPACCOUNTING1; Trusted_Connection = True;"},
                {8, @"Server =.\cpascorp; Database = CORPACCOUNTING1; Trusted_Connection = True;"},
                {9, @"Server =.\cpascorp; Database = CORPACCOUNTING1; Trusted_Connection = True;"},
                {10, @"Server =.\cpascorp; Database = CORPACCOUNTING1; Trusted_Connection = True;"}
    };

        /// <summary>
        /// Access the Dictionary from external sources
        /// </summary>
        public static string GetConnectionString(int intTenantId)
        {
            // Try to get the result in the static Dictionary
            string result;
            return Dict.TryGetValue(intTenantId, out result) ? result : @"Server =.\cpascorp; Database = CORPACCOUNTING1; Trusted_Connection = True;";
        }
    }
}
