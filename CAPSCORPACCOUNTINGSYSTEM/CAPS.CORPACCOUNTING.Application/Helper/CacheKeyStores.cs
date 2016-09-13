
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
        public const string CacheEmployeeStore = "SumitEmployee";
        public const string EmployeeKey = "Employee";

        //SubAccountRestriction Keys
        public const string CacheSubAccountRestrictionStore = "SumitSubAccountRestriction";
        public const string SubAccountRestrictionKey = "SubAccountRestriction";

        //BankAccountKeys
        public const string CacheBankAccountStore = "SumitBankAccount";
        public const string BankAccountKey = "BankAccount";



        //public static string CalculateCacheKey(string sourceName, int tenantId, long? OrganizationUnitId)
        //{
        //    OrganizationUnitId = ReferenceEquals(OrganizationUnitId, null) ? 0 : OrganizationUnitId;
        //    return (tenantId + "#" + OrganizationUnitId + "#" + sourceName);
        //}
        public static string CalculateCacheKey(string sourceName, int tenantId)
        {
          
            return (tenantId + "#" +sourceName);
        }
    }
}
