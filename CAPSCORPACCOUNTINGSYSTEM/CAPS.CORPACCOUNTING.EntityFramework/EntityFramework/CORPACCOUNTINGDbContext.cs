using System;
using System.Data.Common;
using System.Data.Entity;
using System.Threading;
using System.Threading.Tasks;
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
using CAPS.CORPACCOUNTING.Preferencees;
using CAPS.CORPACCOUNTING.Common;
using CAPS.CORPACCOUNTING.Financials.Preferences;
using CAPS.CORPACCOUNTING.Localization;
using CAPS.CORPACCOUNTING.Security;
using Z.EntityFramework.Plus;
using CAPS.CORPACCOUNTING.EFAuditLog;
using CAPS.CORPACCOUNTING.Organization;
using System.Reflection;
using System.Collections.Generic;
using Abp.Configuration;
using CAPS.CORPACCOUNTING.Configuration;
using CAPS.CORPACCOUNTING.CoreHelper;
using Abp.Organizations;

namespace CAPS.CORPACCOUNTING.EntityFramework
{

    public class CORPACCOUNTINGDbContext : AbpZeroDbContext<Tenant, Role, User>
    {

        private readonly ISettingManager _settingManager;

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

        /// <summary> ArInvoiceEntryDocumentUnit Declaration</summary>
        public virtual IDbSet<ArInvoiceEntryDocumentUnit> ArInvoiceEntryDocumentUnit { get; set; }

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


        /// <summary> ArStatementDetailUnit Declaration</summary>
        public virtual IDbSet<ArStatementDetailUnit> ArStatementDetailUnit { get; set; }

        /// <summary> PreferenceChoiceGroupUnit Declaration</summary>
        public virtual IDbSet<PreferenceChoiceGroupUnit> PreferenceChoiceGroupUnit { get; set; }

        /// <summary> TypeOfPreferenceUnit Declaration</summary>
        public virtual IDbSet<TypeOfPreferenceUnit> TypeOfPreferenceUnit { get; set; }

        /// <summary> DefaultPreferenceUnit Declaration</summary>
        public virtual IDbSet<DefaultPreferenceUnit> DefaultPreferenceUnit { get; set; }

        /// <summary> SystemPreferenceUnit Declaration</summary>
        public virtual IDbSet<SystemPreferenceUnit> SystemPreferenceUnit { get; set; }

        /// <summary> ErrorMessageUnit Declaration</summary>
        public virtual IDbSet<ErrorMessageUnit> ErrorMessageUnit { get; set; }

        /// <summary> TypeOfAccountClassificationUnit Declaration</summary>
        public virtual IDbSet<TypeOfAccountClassificationUnit> TypeOfAccountClassificationUnit { get; set; }

        /// <summary> TypeOfAccountUnit Declaration</summary>
        public virtual IDbSet<TypeOfAccountUnit> TypeOfAccountUnit { get; set; }

        /// <summary> WebAddressUnit Declaration</summary>
        public virtual IDbSet<WebAddressUnit> WebAddressUnit { get; set; }

        /// <summary> VendorGroupUnit Declaration</summary>
        public virtual IDbSet<VendorGroupUnit> VendorGroupUnit { get; set; }


        /// <summary> Code1099T4RateUnit Declaration</summary>
        public virtual IDbSet<Code1099T4RateUnit> Code1099T4RateUnit { get; set; }

        /// <summary> Code1099T4TypeUnit Declaration</summary>
        public virtual IDbSet<Code1099T4TypeUnit> Code1099T4TypeUnit { get; set; }

        /// <summary> EmailAddressUnit Declaration</summary>
        public virtual IDbSet<EmailAddressUnit> EmailAddressUnit { get; set; }

        /// <summary> EmailMsgLogUnit Declaration</summary>
        public virtual IDbSet<EmailMsgLogUnit> EmailMsgLogUnit { get; set; }


        /// <summary> EmailMsgAddressUnit Declaration</summary>
        public virtual IDbSet<EmailMsgAddressUnit> EmailMsgAddressUnit { get; set; }

        /// <summary> FedExTranslationUnit Declaration</summary>
        public virtual IDbSet<FedExTranslationUnit> FedExTranslationUnit { get; set; }

        /// <summary> TypeOfProfitCalcUnit Declaration</summary>
        public virtual IDbSet<TypeOfProfitCalcUnit> TypeOfProfitCalcUnit { get; set; }

        /// <summary> ProfitRuleUnit Declaration</summary>
        public virtual IDbSet<ProfitRuleUnit> ProfitRuleUnit { get; set; }

        /// <summary> SecureObjectUnit Declaration</summary>
        public virtual IDbSet<SecureObjectUnit> SecureObjectUnit { get; set; }

        /// <summary> TaxRebateUnit Declaration</summary>
        public virtual IDbSet<TaxRebateUnit> TaxRebateUnit { get; set; }

        /// <summary> WorkSheet1099Unit Declaration</summary>
        public virtual IDbSet<WorkSheet1099Unit> WorkSheet1099Unit { get; set; }

        /// <summary> NewCOATrimUnit Declaration</summary>
        public virtual IDbSet<NewCOATrimUnit> NewCOATrimUnit { get; set; }

        /// <summary> ControlAccountUnit Declaration</summary>
        public virtual IDbSet<ControlAccountUnit> ControlAccountUnit { get; set; }
        /// <summary> ConsolidationGroupUnit Declaration</summary>
        public virtual IDbSet<ConsolidationGroupUnit> ConsolidationGroupUnit { get; set; }

        /// <summary> ConsolidationDetailUnit Declaration</summary>
        public virtual IDbSet<ConsolidationDetailUnit> ConsolidationDetailUnit { get; set; }
        /// <summary> ApprovalUnit Declaration</summary>
        public virtual IDbSet<ApprovalUnit> ApprovalUnit { get; set; }

        /// <summary> AccountingControlUnit Declaration</summary>
        public virtual IDbSet<AccountingControlUnit> AccountingControlUnit { get; set; }

        /// <summary> ApprovedSoxUnit Declaration</summary>
        public virtual IDbSet<ApprovedSoxUnit> ApprovedSoxUnit { get; set; }
        /// <summary> AttachedObjectUnit Declaration</summary>
        public virtual IDbSet<AttachedObjectUnit> AttachedObjectUnit { get; set; }

        /// <summary> NotedObjectUnit Declaration</summary>
        public virtual IDbSet<NotedObjectUnit> NotedObjectUnit { get; set; }

        /// <summary> ProfitVariableUnit Declaration</summary>
        public virtual IDbSet<ProfitVariableUnit> ProfitVariableUnit { get; set; }

        /// <summary> TypeOfSicCodeUnit Declaration</summary>
        public virtual IDbSet<TypeOfSicCodeUnit> TypeOfSicCodeUnit { get; set; }

        /// <summary> SICCodeUnit Declaration</summary>
        public virtual IDbSet<SICCodeUnit> SICCodeUnit { get; set; }

        /// <summary> UploadNameUnit Declaration</summary>
        public virtual IDbSet<UploadNameUnit> UploadNameUnit { get; set; }

        /// <summary> VendorShipMethodUnit Declaration</summary>
        public virtual IDbSet<VendorShipMethodUnit> VendorShipMethodUnit { get; set; }

        /// <summary> WorkFlowUnit Declaration</summary>
        public virtual IDbSet<WorkFlowUnit> WorkFlowUnit { get; set; }

        /// <summary> WorkFlowLogUnit Declaration</summary>
        public virtual IDbSet<WorkFlowLogUnit> WorkFlowLogUnit { get; set; }

        /// <summary> ReceiptHistoryUnit Declaration</summary>
        public virtual IDbSet<ReceiptHistoryUnit> ReceiptHistoryUnit { get; set; }

        /// <summary> PaymentPrintHistoryUnit Declaration</summary>
        public virtual IDbSet<PaymentPrintHistoryUnit> PaymentPrintHistoryUnit { get; set; }

        /// <summary> EmailUnit Declaration</summary>
        public virtual IDbSet<EmailUnit> EmailUnit { get; set; }

        /// <summary> CustomerGroupUnit Declaration</summary>
        public virtual IDbSet<CustomerGroupUnit> CustomerGroupUnit { get; set; }

        /// <summary> CustomerGroupUnit Declaration</summary>
        public virtual IDbSet<TypeOfCurrencyRateUnit> TypeOfCurrencyRateUnit { get; set; }

        /// <summary> CurrencyTypeUnit Declaration</summary>
        public virtual IDbSet<CurrencyTypeUnit> CurrencyTypeUnit { get; set; }

        /// <summary> CurrencyTypeOfRateUnit Declaration</summary>
        public virtual IDbSet<CurrencyTypeOfRateUnit> CurrencyTypeOfRateUnit { get; set; }

        /// <summary> TypeOfAllocationStatusUnit Declaration</summary>
        public virtual IDbSet<TypeOfAllocationStatusUnit> TypeOfAllocationStatusUnit { get; set; }

        /// <summary> TypeOfApprovalStatusUnit Declaration</summary>
        public virtual IDbSet<TypeOfApprovalStatusUnit> TypeOfApprovalStatusUnit { get; set; }

        /// <summary> TypeOfChargeUnit Declaration</summary>
        public virtual IDbSet<TypeOfChargeUnit> TypeOfChargeUnit { get; set; }

        /// <summary> TypeOfCreditRatingUnit Declaration</summary>
        public virtual IDbSet<TypeOfCreditRatingUnit> TypeOfCreditRatingUnit { get; set; }

        /// <summary> TypeOfMasterAccountUnit Declaration</summary>
        public virtual IDbSet<TypeOfMasterAccountUnit> TypeOfMasterAccountUnit { get; set; }

        /// <summary> TypeOfMessageLogUnit Declaration</summary>
        public virtual IDbSet<TypeOfMessageLogUnit> TypeOfMessageLogUnit { get; set; }

        /// <summary> TypeOfMessageUnit Declaration</summary>
        public virtual IDbSet<TypeOfMessageUnit> TypeOfMessageUnit { get; set; }

        /// <summary> TypeOfModificationRequestUnit Declaration</summary>
        public virtual IDbSet<TypeOfModificationRequestUnit> TypeOfModificationRequestUnit { get; set; }

        /// <summary> TypeOfModificationUnit Declaration</summary>
        public virtual IDbSet<TypeOfModificationUnit> TypeOfModificationUnit { get; set; }

        /// <summary> TypeOfPostingCycleUnit Declaration</summary>
        public virtual IDbSet<TypeOfPostingCycleUnit> TypeOfPostingCycleUnit { get; set; }

        /// <summary> TypeOfSeverityLevelUnit Declaration</summary>
        public virtual IDbSet<TypeOfSeverityLevelUnit> TypeOfSeverityLevelUnit { get; set; }

        /// <summary> TypeOfProcessStatusUnit Declaration</summary>
        public virtual IDbSet<TypeOfProcessStatusUnit> TypeOfProcessStatusUnit { get; set; }

        /// <summary> TypeOfOtherNameUnit Declaration</summary>
        public virtual IDbSet<TypeOfOtherNameUnit> TypeOfOtherNameUnit { get; set; }

        /// <summary> TypeOfModificationStatusUnit Declaration</summary>
        public virtual IDbSet<TypeOfModificationStatusUnit> TypeOfModificationStatusUnit { get; set; }

        /// <summary> TypeOfImportUnit Declaration</summary>
        public virtual IDbSet<TypeOfImportUnit> TypeOfImportUnit { get; set; }

        /// <summary> TypeOfGenericProcessUnit Declaration</summary>
        public virtual IDbSet<TypeOfGenericProcessUnit> TypeOfGenericProcessUnit { get; set; }

        /// <summary> TypeOfDocumentConsolidationUnit Declaration</summary>
        public virtual IDbSet<TypeOfDocumentConsolidationUnit> TypeOfDocumentConsolidationUnit { get; set; }

        /// <summary> JobTypeUnit Declaration</summary>
        public virtual IDbSet<JobTypeUnit> JobTypeUnit { get; set; }


        /// <summary> TypeOfPayrollUnit Declaration</summary>
        public virtual IDbSet<TypeOfPayrollUnit> TypeOfPayrollUnit { get; set; }

        /// <summary> CurrencyRateUnit Declaration</summary>
        public virtual IDbSet<CurrencyRateUnit> CurrencyRateUnit { get; set; }
        /// <summary> PCGridUnit Declaration</summary>
        public virtual IDbSet<PCGridUnit> PCGridUnit { get; set; }

        /// <summary> TypeOfJobUnit Declaration</summary>
        public virtual IDbSet<TypeOfJobUnit> TypeOfJobUnit { get; set; }

        /// <summary> GridListUnit Declaration</summary>
        public virtual IDbSet<GridListUnit> GridListUnit { get; set; }

        /// <summary> UserViewSettingsUnit Declaration</summary>
        public virtual IDbSet<UserViewSettingsUnit> UserViewSettingsUnit { get; set; }

        /// <summary> CustomLanguageTextsUnit Declaration</summary>
        public virtual IDbSet<CustomLanguageTextsUnit> CustomLanguageTextsUnit { get; set; }

        /// <summary> TypeOfCurrencyUnit Declaration</summary>
        public virtual IDbSet<TypeOfCurrencyUnit> TypeOfCurrencyUnit { get; set; }

        public virtual IDbSet<SubEntityAccessRestrictionUnit> SubEntityAccessRestrictionUnit { get; set; }

        public virtual IDbSet<SecureGroup> SecureGroup { get; set; }

        public virtual IDbSet<SecureGroupMappingUnit> SecureGroupMappingUnit { get; set; }

        public virtual IDbSet<CountryUnit> CountryUnit { get; set; }

        public virtual IDbSet<VendorAliasUnit> VendorAliasUnit { get; set; }

        public virtual IDbSet<TaxCreditUnit> TaxCreditUnit { get; set; }

        public virtual IDbSet<JobPORangeAllocationUnit> JobPORangeAllocationUnit { get; set; }

        public virtual IDbSet<SystemViewSettingsUnit> SystemViewSettingsUnit { get; set; }

        public virtual IDbSet<JournalEntryDocumentUnit> JournalEntryDocumentUnit { get; set; }


        public virtual IDbSet<SubAccountRestrictionUnit> SubAccountRestrictionUnit { get; set; }


        public virtual IDbSet<OrganizationExtended> OrganizationExtended { get; set; }

        public virtual IDbSet<PurchaseOrderHistoryUnit> PurchaseOrderHistory { get; set; }

        public virtual IDbSet<ConnectionStringUnit> ConnectionStrings { get; set; }

        public virtual IDbSet<TenantExtendedUnit> TenantExtended { get; set; }

        public DbSet<AuditEntry> AuditEntries { get; set; }
        public DbSet<AuditEntryProperty> AuditEntryProperties { get; set; }

        public DbSet<ChargeEntryDocumentUnit> ChargeEntryDocumentUnit { get; set; }

        public DbSet<TerritoriesUnit> TerritoriesUnit { get; set; }
        

        #region Modification Log


        public override int SaveChanges()
        {
            var audit = new Audit();
            var currentUser = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            audit.CreatedBy = string.IsNullOrEmpty(currentUser) ? "System" : currentUser;
            audit.PreSaveChanges(this);
            var rowAffecteds = base.SaveChanges();
            audit.PostSaveChanges();


            if (audit.Configuration.AutoSavePreAction != null)
            {
                audit.Configuration.AutoSavePreAction(this, audit);
                base.SaveChanges();
                
            }

            return rowAffecteds;
        }

        public override Task<int> SaveChangesAsync()
        {
            return SaveChangesAsync(CancellationToken.None);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {

                var audit = new Audit();
                var currentUser = System.Threading.Thread.CurrentPrincipal.Identity.Name;
                audit.CreatedBy = string.IsNullOrEmpty(currentUser) ? "System" : currentUser;

                audit.PreSaveChanges(this);
                var rowAffecteds = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                audit.PostSaveChanges();

                if (audit.Configuration.AutoSavePreAction != null)
                {
                    var isAduitSaveToDb = await _settingManager.GetSettingValueAsync<bool>(AppSettings.General.AuditSaveToDB);
                    if (isAduitSaveToDb)
                    {
                        audit.Configuration.AutoSavePreAction(this, audit);
                        await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                    }

                    var entries = audit.Entries;
                    var accountingItemLog = entries.Find(u => u.EntitySetName == "AccountingItemUnits" && u.EntityTypeName.Contains("PurchaseOrderEntryDo"));
                    if (!ReferenceEquals(accountingItemLog, null))
                    {
                        await CreatePurchaseOrderHistory(cancellationToken, accountingItemLog);
                    }
                }


                return rowAffecteds;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }





        #endregion



        /* Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         * But it may cause problems when working Migrate.exe of EF. ABP works either way.         * 
         */


        public CORPACCOUNTINGDbContext()
       : base("Default")
        {

        }

        public CORPACCOUNTINGDbContext(ISettingManager settingManager)
            : base("Default")
        {
            _settingManager = settingManager;
        }


        /* This constructor is used by ABP to pass connection string defined in CORPACCOUNTINGDataModule.PreInitialize.
       * Notice that, actually you will not directly create an instance of CORPACCOUNTINGDbContext since ABP automatically handles it.
       */
        public CORPACCOUNTINGDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {


        }

        /* This constructor is used by ABP to pass connection string defined in CORPACCOUNTINGDataModule.PreInitialize.
         * Notice that, actually you will not directly create an instance of CORPACCOUNTINGDbContext since ABP automatically handles it.
         */
        public CORPACCOUNTINGDbContext(string nameOrConnectionString, ISettingManager settingManager)
            : base(nameOrConnectionString)
        {
            _settingManager = settingManager;

        }

        /* This constructor is used in tests to pass a fake/mock connection.
        */
        public CORPACCOUNTINGDbContext(DbConnection dbConnection)
            : base(dbConnection, true)
        {

        }

        /* This constructor is used in tests to pass a fake/mock connection.
         */
        public CORPACCOUNTINGDbContext(DbConnection dbConnection, ISettingManager settingManager)
            : base(dbConnection, true)
        {
            _settingManager = settingManager;
        }

        /// <summary>
        /// Removing the Abp Prefix. You can use any Prefix. We are using CAPS_
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ChangeAbpTablePrefix<Tenant, Role, User>("CAPS_");
            AuditManager.DefaultConfiguration.IgnorePropertyUnchanged = false;
            AuditManager.DefaultConfiguration.Exclude(x => true); // Exclude ALL
            AuditManager.DefaultConfiguration.Include<INeedModLog>();
            AuditManager.DefaultConfiguration.AutoSavePreAction = (context, audit) =>
            // ADD "Where(x => x.AuditEntryID == 0)" to allow multiple SaveChanges with same Audit
             ((CORPACCOUNTINGDbContext)context).AuditEntries.AddRange(audit.Entries);
        }


        #region PO Logging
        /// <summary>
        /// Tracking purchaseOrderHistory
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <param name="accountingItemLog"></param>
        /// <returns></returns>
        private async Task CreatePurchaseOrderHistory(CancellationToken cancellationToken, AuditEntry accountingItemLog)
        {
            try
            {

                var poNewHistory = new PurchaseOrderHistoryUnit();
                var poOldHistory = new PurchaseOrderHistoryUnit();

                var PropertiesList = accountingItemLog.Properties.FindAll(u => u.PropertyName != "LajitId" && u.PropertyName != "Id");
                var propAccountingItemId = accountingItemLog.Properties.Find(u => u.PropertyName == "Id");
                var isClose = accountingItemLog.Properties.Find(u => u.PropertyName == "IsClose");

                if (accountingItemLog.StateName == AuditEntryState.EntityAdded.ToString())
                {
                    poNewHistory = GetPoNewValues(PropertiesList);
                    poNewHistory.AccountingItemId = Convert.ToInt64(propAccountingItemId.NewValue);
                    poNewHistory.ModificationTypeId = ModificationType.Created;
                    PurchaseOrderHistory.Add(poNewHistory);
                    await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                }
                else if (accountingItemLog.StateName == AuditEntryState.EntityModified.ToString())
                {
                    poNewHistory = GetPoNewValues(PropertiesList);
                    poOldHistory = GetPoOldValues(PropertiesList);

                    poNewHistory.AccountingItemId = Convert.ToInt64(propAccountingItemId.NewValue);
                    poOldHistory.AccountingItemId = Convert.ToInt64(propAccountingItemId.OldValue);



                    //If Job OR Line Changed
                    if (poOldHistory.JobId != poNewHistory.JobId || poOldHistory.AccountId != poNewHistory.AccountId)
                    {
                        poOldHistory.Amount = -poOldHistory.Amount;
                        poOldHistory.ModificationTypeId = ModificationType.Linechange;
                        if (poNewHistory.SourceTypeId != SourceType.PO)
                            poOldHistory.SourceTypeId = poNewHistory.SourceTypeId;

                        PurchaseOrderHistory.Add(poOldHistory);
                        await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                        poOldHistory.Amount = Math.Abs(poOldHistory.Amount.Value);

                        poNewHistory.Amount = Math.Abs(poOldHistory.Amount.Value);
                        poNewHistory.ModificationTypeId = ModificationType.Linechange;
                        PurchaseOrderHistory.Add(poNewHistory);
                        await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                    }

                    //if amount Changed
                    poNewHistory = GetPoNewValues(PropertiesList);
                    int value = decimal.Compare(poNewHistory.Amount.Value, poOldHistory.Amount.Value);
                    if (value != 0)
                    {
                        poNewHistory.AccountingItemId = Convert.ToInt64(propAccountingItemId.NewValue);
                        poNewHistory.ChangeInAmount = poNewHistory.Amount.Value - poOldHistory.Amount.Value;
                        poNewHistory.ModificationTypeId = poNewHistory.SourceTypeId == SourceType.PO ? (value > 0 ? ModificationType.IncreasedAmount : ModificationType.DecreasedAmount) : ModificationType.Reduced;
                        PurchaseOrderHistory.Add(poNewHistory);
                        await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                    }

                    //if PO is Closed
                    if (isClose.NewValue.ToString().Length > 0 && Convert.ToBoolean(isClose.NewValue))
                    {
                        poNewHistory = GetPoNewValues(PropertiesList);
                        poNewHistory.AccountingItemId = Convert.ToInt64(propAccountingItemId.NewValue);
                        poNewHistory.ModificationTypeId = ModificationType.Closed;
                        PurchaseOrderHistory.Add(poNewHistory);
                        await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                    }

                }
                else if (accountingItemLog.StateName == AuditEntryState.EntityDeleted.ToString())
                {
                    poOldHistory = GetPoOldValues(PropertiesList);
                    poOldHistory.ModificationTypeId = ModificationType.Deleted;
                    poOldHistory.AccountingItemId = Convert.ToInt64(propAccountingItemId.OldValue);
                    PurchaseOrderHistory.Add(poOldHistory);
                    await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private PurchaseOrderHistoryUnit GetPoOldValues(List<AuditEntryProperty> PropertiesList)
        {
            PurchaseOrderHistoryUnit poHistory = new PurchaseOrderHistoryUnit();
            Type poHistoryType = poHistory.GetType();
            foreach (var item in PropertiesList)
            {
                PropertyInfo poPropertyInfo = poHistoryType.GetProperty(item.PropertyName);
                if (item.OldValue.ToString().Length > 0 && CoreHelpers.HasProperty(poHistoryType, item.PropertyName))
                    poPropertyInfo.SetValue(poHistory, CoreHelper.CoreHelpers.ChangeType(item.OldValue, poPropertyInfo.PropertyType), null);
            }
            return poHistory;
        }
        private PurchaseOrderHistoryUnit GetPoNewValues(List<AuditEntryProperty> PropertiesList)
        {
            PurchaseOrderHistoryUnit poHistory = new PurchaseOrderHistoryUnit();
            Type poHistoryType = poHistory.GetType();
            foreach (var item in PropertiesList)
            {
                PropertyInfo poPropertyInfo = poHistoryType.GetProperty(item.PropertyName);
                if (item.NewValue.ToString().Length > 0 && CoreHelpers.HasProperty(poHistoryType, item.PropertyName))
                    poPropertyInfo.SetValue(poHistory, CoreHelper.CoreHelpers.ChangeType(item.NewValue, poPropertyInfo.PropertyType), null);
            }
            return poHistory;
        }

        #endregion

    }
}
