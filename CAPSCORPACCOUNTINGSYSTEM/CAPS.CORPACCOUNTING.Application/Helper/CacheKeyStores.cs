
namespace CAPS.CORPACCOUNTING.Helpers
{
    public class CacheKeyStores
    {
        //Division Keys
        public const string CacheDivisionStore = "SumitDivision";
        public const string DivisionKey = "Division";

        //AccountKeys
        public const string CacheAccountStore = "SumitAccount";
        public const string AccountKey = "Account";
        
        //CustomerKeys
        public const string CacheCustomerStore = "SumitCustomer";
        public const string CustomerKey = "Customer";

        //VendorKeys
        public const string CacheVendorStore = "SumitVendor";
        public const string VendorKey = "Vendor";

        //SubAccountsKeys
        public const string CacheSubAccountStore = "SumitSubAccount";
        public const string SubAccountKey = "SubAccount";

        //Employee Keys
        public const string CachEmployeeStore = "SumitEmployee";
        public const string EmployeeKey = "Employee";



        public static string CalculateCacheKey(string sourceName, int tenantId, long? OrganizationUnitId)
        {
            OrganizationUnitId = ReferenceEquals(OrganizationUnitId, null) ? 0 : OrganizationUnitId;
            return (tenantId + "#" + OrganizationUnitId + "#" + sourceName);
        }
    }
}
