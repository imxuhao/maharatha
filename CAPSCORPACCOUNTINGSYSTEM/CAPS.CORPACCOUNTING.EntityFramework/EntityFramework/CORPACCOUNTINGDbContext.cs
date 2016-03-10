using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.Authorization.Roles;
using CAPS.CORPACCOUNTING.Authorization.Users;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.MultiTenancy;
using CAPS.CORPACCOUNTING.Storage;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Payables;
using CAPS.CORPACCOUNTING.Journals;
using CAPS.CORPACCOUNTING.Payroll;
using CAPS.CORPACCOUNTING.PurchaseOrders;
using CAPS.CORPACCOUNTING.PettyCash;
using CAPS.CORPACCOUNTING.Payments;
using CAPS.CORPACCOUNTING.CashEntry;
using CAPS.CORPACCOUNTING.Reports;
using CAPS.CORPACCOUNTING.ChargeEntry;
using CAPS.CORPACCOUNTING.AccountReceivable;

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

        /// <summary> AccountingHeaderTransactionsUnit Declaration</summary>
        public virtual IDbSet<AccountingHeaderTransactionsUnit> AccountingHeaderTransactions { get; set; }

        /// <summary> ApHeaderTransactions Declaration</summary>
        public virtual IDbSet<ApHeaderTransactions> ApHeaderTransactions { get; set; }

        /// <summary> PayrollEntryDocumentUnit Declaration</summary>
        public virtual IDbSet<PayrollEntryDocumentUnit> PayrollEntryDocuments { get; set; }

        /// <summary> PettyCashAccountUnit Declaration</summary>       
        public virtual IDbSet<PettyCashAccountUnit> PettyCashAccounts { get; set; }

        /// <summary> UploadDocumentLogUnit Declaration</summary>      
        public virtual IDbSet<UploadDocumentLogUnit> UploadDocumentLogs { get; set; }


        /// <summary> PaymentRequestHistotyUnit Declaration</summary>      
        public virtual IDbSet<PaymentRequestHistotyUnit> PaymentRequestHistoty { get; set; }

        /// <summary> PaymentEntryDocumentUnit Declaration</summary>      
        public virtual IDbSet<PaymentEntryDocumentUnit> PaymentEntryDocuments { get; set; }

        /// <summary> PurchaseOrderEntryDocumentUnit Declaration</summary>
        public virtual IDbSet<PurchaseOrderEntryDocumentUnit> PurchaseOrderEntryDocuments { get; set; }

        /// <summary> CashEntryDocumentUnit Declaration</summary>
        public virtual IDbSet<CashEntryDocumentUnit> CashEntryDocumentUnits { get; set; }

        /// <summary> TypeOfCategoryUnit Declaration</summary>
        public virtual IDbSet<TypeOfCategoryUnit> TypeOfCategoryUnits { get; set; }

        /// <summary> GroupTotalUnit Declaration</summary>
        public virtual IDbSet<GroupTotalUnit> GroupTotalUnit { get; set; }

        /// <summary> GroupItemUnit Declaration</summary>
        public virtual IDbSet<GroupItemUnit> GroupItemUnit { get; set; }

        /// <summary> GroupItemRangeUnit Declaration</summary>
        public virtual IDbSet<GroupItemRangeUnit> GroupItemRangeUnit { get; set; }

        /// <summary> ARStatementInfo Declaration</summary>
        public virtual IDbSet<ARStatementInfo> ARStatementInfo { get; set; }

        /// <summary> TypeOfHeadingUnit Declaration</summary>
        public virtual IDbSet<TypeOfHeadingUnit> TypeOfHeadingUnits { get; set; }

        /// <summary> TypeOfAccountingLayoutUnit Declaration</summary>
        public virtual IDbSet<TypeOfAccountingLayoutUnit> TypeOfAccountingLayoutUnits { get; set; }

        /// <summary> AccountingLayoutUnit Declaration</summary>
        public virtual IDbSet<AccountingLayoutUnit> AccountingLayoutUnits { get; set; }

        /// <summary> AccountingLayoutItemUnit Declaration</summary>
        public virtual IDbSet<AccountingLayoutItemUnit> AccountingLayoutItemUnits { get; set; }

        /// <summary> SubAccountUnit Declaration</summary>
        public virtual IDbSet<SubAccountUnit> SubAccountUnits { get; set; }

        /// <summary> TypeOfCountryUnit Declaration</summary>
        public virtual IDbSet<TypeOfCountryUnit> TypeOfCountryUnit { get; set; }

        /// <summary> RegionUnit Declaration</summary>
        public virtual IDbSet<RegionUnit> RegionUnit { get; set; }


        /// <summary> UploadAddressUnit Declaration</summary>
        public virtual IDbSet<UploadAddressUnit> UploadAddressUnit { get; set; }


        /// <summary> BatchReportUnit Declaration</summary>
        public virtual IDbSet<BatchReportUnit> BatchReportUnit { get; set; }

        /// <summary> TypeOfFinReportUnit Declaration</summary>
        public virtual IDbSet<TypeOfFinReportUnit> TypeOfFinReportUnit { get; set; }

        /// <summary> BatchExecutionResultUnit Declaration</summary>
        public virtual IDbSet<BatchExecutionResultUnit> BatchExecutionResultUnit { get; set; }

        /// <summary> AccountingItemUnit Declaration</summary>
        public virtual IDbSet<AccountingItemUnit> AccountingItemUnits { get; set; }

        /// <summary> PaymentEntryDocumentDetailUnit Declaration</summary>
        public virtual IDbSet<PaymentEntryDocumentDetailUnit> PaymentEntryDocumentDetailUnits { get; set; }

        /// <summary> PayrollEntryDocumentDetailUnit Declaration</summary>
        public virtual IDbSet<PayrollEntryDocumentDetailUnit> PayrollEntryDocumentDetailUnits { get; set; }

        /// <summary> PettyCashEntryDocumentDetailUnit Declaration</summary>
        public virtual IDbSet<PettyCashEntryDocumentDetailUnit> PettyCashEntryDocumentDetailUnits { get; set; }

        /// <summary> InvoiceEntryDocumentDetailUnit Declaration</summary>
        public virtual IDbSet<InvoiceEntryDocumentDetailUnit> InvoiceEntryDocumentDetailUnits { get; set; }

        /// <summary> ChargeEntryDocumentDetailUnit Declaration</summary>
        public virtual IDbSet<ChargeEntryDocumentDetailUnit> ChargeEntryDocumentDetailUnits { get; set; }

        /// <summary> PurchaseOrderEntryDocumentDetailUnit Declaration</summary>
        public virtual IDbSet<PurchaseOrderEntryDocumentDetailUnit> PurchaseOrderEntryDocumentDetailUnits { get; set; }


        /// <summary> CashEntryDocumentDetailUnit Declaration</summary>
        public virtual IDbSet<CashEntryDocumentDetailUnit> CashEntryDocumentDetailUnits { get; set; }

        /// <summary> ReceiptHistoryDetailUnit Declaration</summary>
        public virtual IDbSet<ReceiptHistoryDetailUnit> ReceiptHistoryDetailUnits { get; set; }

        /// <summary> JournalEntryDocumentDetailUnit Declaration</summary>
        public virtual IDbSet<JournalEntryDocumentDetailUnit> JournalEntryDocumentDetailUnits { get; set; }

        /// <summary> ArInvoiceEntryDocumentDetailUnit Declaration</summary>
        public virtual IDbSet<ArInvoiceEntryDocumentDetailUnit> ArInvoiceEntryDocumentDetailUnits { get; set; }

        /// <summary> BatchReportItemUnit Declaration</summary>
        public virtual IDbSet<BatchReportItemUnit> BatchReportItemUnit { get; set; }

        /// <summary> FiscalYearUnit Declaration</summary>
        public virtual IDbSet<FiscalYearUnit> FiscalYearUnit { get; set; }
        
        /// <summary> FiscalPeriodUnit Declaration</summary>
        public virtual IDbSet<FiscalPeriodUnit> FiscalPeriodUnit { get; set; }

        /// <summary> ProjectControlPeriodUnit Declaration</summary>
        public virtual IDbSet<ProjectControlPeriodUnit> ProjectControlPeriodUnit { get; set; }

        /// <summary> JobWrapEntryDocumentUnit Declaration</summary>
        public virtual IDbSet<JobWrapEntryDocumentUnit> JobWrapEntryDocumentUnit { get; set; }

        /// <summary> JobWrapDocumentLogUnit Declaration</summary>
        public virtual IDbSet<JobWrapDocumentLogUnit> JobWrapDocumentLogUnit { get; set; }

        /// <summary> JobEFCUnit Declaration</summary>
        public virtual IDbSet<JobEFCUnit> JobEFCUnit { get; set; }

        /// <summary> JobWrapCheckLogUnit Declaration</summary>
        public virtual IDbSet<JobWrapCheckLogUnit> JobWrapCheckLogUnit { get; set; }

        /// <summary> JobWrapSalesLogUnit Declaration</summary>
        public virtual IDbSet<JobWrapSalesLogUnit> JobWrapSalesLogUnit { get; set; }

        /// <summary> JobWrapAmexLogUnit Declaration</summary>
        public virtual IDbSet<JobWrapAmexLogUnit> JobWrapAmexLogUnit { get; set; }

        /// <summary> JobWrapPayrollLogUnit Declaration</summary>
        public virtual IDbSet<JobWrapPayrollLogUnit> JobWrapPayrollLogUnit { get; set; }

        /// <summary> BankRecAdjustmentUnit Declaration</summary>
        public virtual IDbSet<BankRecAdjustmentUnit> BankRecAdjustmentUnit { get; set; }

        /// <summary> BankRecClearedUnit Declaration</summary>
        public virtual IDbSet<BankRecClearedUnit> BankRecClearedUnit { get; set; }

        /// <summary> ChartOfAccountRollupUnit Declaration</summary>
        public virtual IDbSet<ChartOfAccountRollupUnit> ChartOfAccountRollupUnit { get; set; }

        /// <summary> AccountRollupUnit Declaration</summary>
        public virtual IDbSet<AccountRollupUnit> AccountRollupUnit { get; set; }

        /// <summary> TypeOfArInvoiceBuildUnit Declaration</summary>
        public virtual IDbSet<TypeOfArInvoiceBuildUnit> TypeOfArInvoiceBuildUnit { get; set; }

        /// <summary> ARInvoiceBuildUnit Declaration</summary>
        public virtual IDbSet<ArInvoiceBuildUnit> ARInvoiceBuildUnit { get; set; }

        /// <summary> InvoicePaymentUnit Declaration</summary>
        public virtual IDbSet<InvoicePaymentUnit> InvoicePaymentUnit { get; set; }

        /// <summary> AccountTotalUnit Declaration</summary>
        public virtual IDbSet<AccountTotalUnit> AccountTotalUnit { get; set; }

        /// <summary> TypeOfFormatMaskUnit Declaration</summary>
        public virtual IDbSet<TypeOfFormatMaskUnit> TypeOfFormatMaskUnit { get; set; }

        /// <summary> PayrollControlUnit Declaration</summary>
        public virtual IDbSet<PayrollControlUnit> PayrollControlUnit { get; set; }


        /// <summary> PayrollControlFringeUnit Declaration</summary>
        public virtual IDbSet<PayrollControlFringeUnit> PayrollControlFringeUnit { get; set; }

        /// <summary> ARYTDInvoiceUnit Declaration</summary>
        public virtual IDbSet<ArytdInvoiceUnit> ARYTDInvoiceUnit { get; set; }

        /// <summary> ValueAddedTaxTypeUnit Declaration</summary>
        public virtual IDbSet<ValueAddedTaxTypeUnit> ValueAddedTaxTypeUnit { get; set; }

        /// <summary> ValueAddedTaxRecoveryUnit Declaration</summary>
        public virtual IDbSet<ValueAddedTaxRecoveryUnit> ValueAddedTaxRecoveryUnit { get; set; }

        /// <summary> PhoneUnit Declaration</summary>
        public virtual IDbSet<PhoneUnit> PhoneUnit { get; set; }

        /// <summary> EntityUnit Declaration</summary>
        public virtual IDbSet<EntityUnit> EntityUnit { get; set; }

        /// <summary> SalesRegionUnit Declaration</summary>
        public virtual IDbSet<SalesRegionUnit> SalesRegionUnit { get; set; }

        /// <summary> ReportDistributionUnit Declaration</summary>
        public virtual IDbSet<ReportDistributionUnit> ReportDistributionUnit { get; set; }


        /// <summary> ReportDistributionListUnit Declaration</summary>
        public virtual IDbSet<ReportDistributionListUnit> ReportDistributionListUnit { get; set; }

        /// <summary> ReportUnit Declaration</summary>
        public virtual IDbSet<ReportUnit> ReportUnit { get; set; }

        /// <summary> UserReportUnit Declaration</summary>
        public virtual IDbSet<UserReportUnit> UserReportUnit { get; set; }


        



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

        /// <summary>
        /// Removing the Abp Prefix. You can use any Prefix. We are using CAPS_
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ChangeAbpTablePrefix<Tenant, Role, User>("CAPS_");
        }
    }
}
