using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using CAPS.CORPACCOUNTING.Authorization.Roles;
using CAPS.CORPACCOUNTING.Authorization.Users;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.MultiTenancy;
using CAPS.CORPACCOUNTING.Storage;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Banking;

namespace CAPS.CORPACCOUNTING.EntityFramework
{
    public class CORPACCOUNTINGDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        /* Define an IDbSet for each entity of the application */

        public virtual IDbSet<BinaryObject> BinaryObjects { get; set; }

        /// <summary>
        /// Chart of Accounts Declaration
        /// </summary>
        public virtual IDbSet<CoaUnit> CoaUnits { get; set; }

        /// <summary>
        ///  Accounts Declaration
        /// </summary>
        public virtual IDbSet<AccountUnit> AccountUnits { get; set; }

        /// <summary>
        ///  VendorPaymentTerms Declaration
        /// </summary>
        public virtual IDbSet<VendorPaymentTermUnit> VendorPaymentTermUnits { get; set; }

        /// <summary>
        ///  CustomerPaymentTerms Declaration
        /// </summary>
        public virtual IDbSet<CustomerPaymentTermUnit> CustomerPaymentTermUnits { get; set; }

        /// <summary>
        ///  SalesReps Declaration
        /// </summary>
        public virtual IDbSet<SalesRepUnit> SalesRepUnits { get; set; }
        /// <summary>
        ///  Customers Declaration
        /// </summary>
        public virtual IDbSet<CustomerUnit> CustomersUnits { get; set; }

        /// <summary>
        ///  Employees Declaration
        /// </summary>
        public virtual IDbSet<EmployeeUnit> EmployeUnits { get; set; }
        /// <summary>
        ///  Address Declaration
        /// </summary>
        public virtual IDbSet<AddressUnit> AddressUnits { get; set; }

        /// <summary>
        ///  Address Declaration
        /// </summary>
        public virtual IDbSet<VendorUnit> VendorUnits { get; set; }

        /// <summary> RollupCenter Declaration </summary>
        public virtual IDbSet<RollupCenterUnit> RollupCenterUnits { get; set; }

        /// <summary> Job Declaration </summary>
        public virtual IDbSet<JobUnit> JobUnits { get; set; }

        /// <summary> JobLocation Declaration </summary>
        public virtual IDbSet<JobLocationUnit> JobLocationUnits { get; set; }

        /// <summary> JobCommercial Declaration<JobCommercial </summary>
        public virtual IDbSet<JobCommercialUnit> JobCommertialUnits { get; set; }

        /// <summary> ARBillingType Declaration</summary>
        public virtual IDbSet<ARBillingTypeUnit> ARBillingTypeUnits { get; set; }

        /// <summary> LinkJobs Declaration</summary>
        public virtual IDbSet<LinkJobsUnit> LinkJobsUnitUnits { get; set; }

        /// <summary> JobBudget Declaration</summary>
        public virtual IDbSet<JobBudgetUnit> JobBudgetUnits { get; set; }

        /// <summary> JobPettyCashLog Declaration</summary>
        public virtual IDbSet<JobPettyCashLogUnit> JobPettyCashLogUnits { get; set; }

        /// <summary> JobPurchaseOrderLog Declaration</summary>
        public virtual IDbSet<JobPurchaseOrderLogUnit> JobPurchaseOrderLogUnits { get; set; }

        /// <summary> JobPONumbers Declaration</summary>
        public virtual IDbSet<JobPONumbersUnit> JobPONumbersUnits { get; set; }

        /// <summary> JobPCConfig Declaration</summary>
        public virtual IDbSet<JobPCConfigUnit> JobPCConfigUnits { get; set; }

        /// <summary> ARInvoiceScheduleUnit Declaration</summary>
        public virtual IDbSet<ARInvoiceScheduleUnit> ARInvoiceScheduleUnits { get; set; }

        /// <summary> JobAccountUnit Declaration</summary>
        public virtual IDbSet<JobAccountUnit> JobAccountUnits { get; set; }

        /// <summary> DirectorUnit Declaration</summary>
        public virtual IDbSet<DirectorUnit> DirectorUnits { get; set; }

        /// <summary> DirectorAccountUnit Declaration</summary>
       public virtual IDbSet<DirectorAccountUnit> DirectorAccountUnits { get; set; }

        /// <summary> LocationSetUnit Declaration</summary>
        public virtual IDbSet<LocationSetUnit> LocationSetUnits { get; set; }

        /// <summary> ICTRelationUnit Declaration</summary>
        public virtual IDbSet<ICTRelationUnit> ICTRelationUnits { get; set; }

        /// <summary> JobBudgetDetailsUnit Declaration</summary>
        public virtual IDbSet<JobBudgetDetailsUnit> JobBudgetDetailsUnits { get; set; }

        /// <summary> TypeOfCheckStockUnit Declaration</summary>
        public virtual IDbSet<TypeOfCheckStockUnit> TypeOfCheckStockUnits { get; set; }

        /// <summary> TypeOfUploadFileUnit Declaration</summary>
        public virtual IDbSet<TypeOfUploadFileUnit> TypeOfUploadFileUnits { get; set; }

        /// <summary> BatchUnit Declaration</summary>
        public virtual IDbSet<BatchUnit> BatchUnits { get; set; }

        /// <summary> BankAccountUnit Declaration</summary>
        public virtual IDbSet<BankAccountUnit> BankAccountUnits { get; set; }

        /// <summary> BankAccountPaymentRangeUnit Declaration</summary>
        public virtual IDbSet<BankAccountPaymentRangeUnit> BankAccountPaymentRanges { get; set; }

        /// <summary> BankAccountUserUnit Declaration</summary>
        public virtual IDbSet<BankAccountUserUnit> BankAccountUsers { get; set; }

        /// <summary> BankRecControlUnit Declaration</summary>
        public virtual IDbSet<BankRecControlUnit> BankRecControls { get; set; }

        /// <summary> BankStatementDetailUnit Declaration</summary>
        public virtual IDbSet<BankStatementDetailUnit> BankStatementDetails { get; set; }



        /* Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         * But it may cause problems when working Migrate.exe of EF. ABP works either way.         * 
         */
        public CORPACCOUNTINGDbContext()
            : base("Default")
        {

        }

        /* This constructor is used by ABP to pass connection string defined in CORPACCOUNTINGDataModule.PreInitialize.
         * Notice that, actually you will not directly create an instance of CORPACCOUNTINGDbContext since ABP automatically handles it.
         */
        public CORPACCOUNTINGDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        /* This constructor is used in tests to pass a fake/mock connection.
         */
        public CORPACCOUNTINGDbContext(DbConnection dbConnection)
            : base(dbConnection, true)
        {

        }
    }
}
