namespace CAPS.CORPACCOUNTING.Web.Navigation
{
    public static class PageNames
    {
        public static class App
        {
            public static class Common
            {
                public const string Administration = "Administration";
                public const string Roles = "Administration.Roles";
                public const string Users = "Administration.Users";
                public const string AuditLogs = "Administration.AuditLogs";
                public const string OrganizationUnits = "Administration.OrganizationUnits";
                public const string Languages = "Administration.Languages";
            }

            public static class Host
            {
                public const string Tenants = "Tenants";
                public const string Editions = "Editions";
                public const string Maintenance = "Administration.Maintenance";
                public const string Settings = "Administration.Settings.Host";
            }

            public static class Tenant
            {
                public const string Dashboard = "Dashboard.Tenant";
                public const string Settings = "Administration.Settings.Tenant";
            }

            public static class  Organization
            {
                #region Financials Tabs

                public const string Financials = "Financials";
                public const string FinancialsAccounts = "Financials.Accounts";
                public const string FinancialsAccountsCoas = "Financials.Accounts.ChartOfAccounts";
                public const string FinancialsAccountsSubAccounts = "Financials.Accounts.SubAccounts";
                public const string FinancialsAccountsDivisions = "Financials.Accounts.Divisions";

                public const string FinancialsJournals = "Financials.Journals";
                public const string FinancialsJournalsEntry = "Financials.Journals.Entry";

                //public const string FinancialsBanking = "Financials.Banking";
                //public const string FinancialsBankingReceiptsOrTransfers = "Financials.Banking.Receipts/Transfers";
                //public const string FinancialsBankingAch = "Financials.Banking.ACH";
                //public const string FinancialsBankingReconciliation = "Financials.Banking.BankReconciliation";

                public const string FinancialsInquiry = "Financials.Inquiry";
                public const string FinancialsInquirySearchTransactions = "Financials.Inquiry.SearchTransactions";
                public const string FinancialsInquiryFinancials = "Financials.Inquiry.Financials";
                public const string FinancialsInquiryJournalHistory = "Financials.Inquiry.JournalHistory";
                public const string FinancialsInquiryAssetTracking = "Financials.Inquiry.AssetTracking";


                public const string FinancialsFiscalPeriod = "Financials.FiscalPeriod";
                //public const string FinancialsPosting = "Financials.Posting";
                //public const string FinancialsPostingBatched = "Financials.Posting.Batched";
                //public const string FinancialsPostingUnBatched = "Financials.Posting.UnBatched";

                //public const string FinancialsPreferences = "Financials.Preferences";
                //public const string FinancialsPreferencesFiscalPeriod = "Financials.Preferences.FiscalPeriod";
                //public const string FinancialsPreferencesBankSetup = "Financials.Preferences.BankSetup";



                #endregion

                #region Projects Tab

                public const string Projects = "Projects";
                public const string ProjectsProjectMaintenance = "Projects.ProjectMaintenance";
                public const string ProjectsMaintenanceProjects = "Projects.ProjectMaintenance.Projects";
                public const string ProjectsMaintenanceProjectCoas = "Projects.ProjectMaintenance.ProjectCOAs";
                public const string ProjectsMaintenanceContracts = "Projects.ProjectMaintenance.Contracts";
                public const string ProjectsInquiry = "Projects.Inquiry";

                #endregion

                #region Receivables Tab

                public const string Receivables = "Receivables";
                public const string ReceivablesCustomers = "Receivables.Customers";
                public const string ReceivablesCustomersHistory = "Receivables.Customers.History";
                public const string ReceivablesInvoices = "Receivables.Invoices";
                public const string ReceivablesInvoicesEntry = "Receivables.Invoices.Entry";
                public const string ReceivablesInquiry = "Receivables.Inquiry";
                public const string ReceivablesInquiryARInvoiceInquiry = "Receivables.Inquiry.ARInvoiceInquiry";
                public const string ReceivablesInquiryCustomerSummary = "Receivables.Inquiry.CustomerSummary";
                public const string ReceivablesInquiryInvoiceDetail = "Receivables.Inquiry.InvoiceDetail";
                public const string ReceivablesPreferences = "Receivables.Preferences";
                public const string ReceivablesPreferencesBillingTypes = "Receivables.Preferences.BillingTypes";
                public const string ReceivablesPreferencesTerritories = "Receivables.Preferences.Territories";
                public const string ReceivablesPreferencesPaymentTerms = "Receivables.Preferences.PaymentTerms";
                public const string ReceivablesPreferencesARInvoiceTemplate = "Receivables.Preferences.ARInvoiceTemplate";

                #endregion

                #region Payables Tab

                public const string Payables = "Payables";
                public const string PayablesVendors = "Payables.Vendors";
                public const string PayablesInvoices = "Payables.Invoices";
                //public const string PayablesInvoicesBatch = "Payables.Invoices.Batch";
                public const string PayablesInquiry = "Payables.Inquiry";
                public const string PayablesInquiryAPInvoiceInquiry = "Payables.Inquiry.APInvoiceInquiry";
                public const string PayablesInquiryPaymentHistory = "Payables.Inquiry.PaymentHistory";
                public const string PayablesInquiryVendorSummary = "Payables.Inquiry.VendorSummary";
                public const string PayablesInquiryInvoiceDetail = "Payables.Inquiry.InvoiceDetail";
                public const string PayablesPreferences = "Payables.Preferences";
                public const string PayablesPreferences1099T4Codes = "Payables.Preferences.1099T4Codes";
                public const string PayablesPreferencesVendorPaymentTerms = "Payables.Preferences.VendorPaymentTerms";
                public const string PayablesYEProcesses = "Payables.YEProcesses";
                public const string PayablesYEProcesses1099s = "Payables.YEProcesses.1099s";

                #endregion

                #region PurchaseOrders

                public const string PurchaseOrders = "PurchaseOrders";
                public const string PurchaseOrdersEntry = "PurchaseOrders.Entry";

                #endregion

                #region Purchasing Tab

                public const string Purchasing = "Purchasing";
                public const string PurchasingInquiry = "Purchasing.Inquiry";
                public const string PurchasingInquiryPurchaseOrderHistory = "Purchasing.Inquiry.PurchaseOrderHistory";
                public const string PurchasingInquirySearchPurchaseOrders = "Purchasing.Inquiry.SearchPurchaseOrders";
                public const string PurchasingInquiryPOAgingGrid = "Purchasing.Inquiry.POAgingGrid";

                #endregion

                #region Petty Cash Tab

                public const string PettyCash = "PettyCash";
                public const string PettyCashPCVendors = "PettyCash.PCVendors";
                public const string PettyCashEntry = "PettyCash.Entry";
                public const string PettyCashInquiry = "PettyCash.Inquiry";
                public const string PettyCashPCVendorsHistory = "PettyCash.PCVendors.History";
                public const string PettyCashInquiryPCAccountSummary = "PettyCash.Inquiry.PCAccountSummary";
                public const string PettyCashInquiryPCAdvancedSearch = "PettyCash.Inquiry.PCAdvancedSearch";

                #endregion

                #region Credit Card Tab

                public const string CreditCard = "CreditCard";
                public const string CreditCardEntry = "CreditCard.Entry";
                public const string CreditCardInquiry = "CreditCard.Inquiry";
                public const string CreditCardInquiryCreditCardHistory = "CreditCard.Inquiry.CreditCardHistory";
                public const string CreditCardInquiryCreditCardUploadInfo = "CreditCard.Inquiry.CreditCardUploadInfo";
                public const string CreditCardPreferences = "CreditCard.Preferences";
                public const string CreditCardPreferencesCreditCardCompanies = "CreditCard.Preferences.CreditCardCompanies";

                #endregion

                #region Payroll Tab

                public const string Payroll = "Payroll";
                public const string PayrollEntry = "Payroll.Entry";
                public const string PayrollInquiry = "Payroll.Inquiry";
                public const string PayrollInquiryPayrollHistory = "Payroll.Inquiry.PayrollHistory ";
                public const string PayrollInquiryPayrollUploadInfo = "Payroll.Inquiry.PayrollUploadInfo";
                public const string PayrollInquiryPayrollLog = "Payroll.Inquiry.PayrollLog";
                public const string PayrollPreferences = "Payroll.Preferences";
                public const string PayrollPreferencesPayrollCompanies = "Payroll.Preferences.PayrollCompanies";
                public const string PayrollPreferencesPaychexControl = "Payroll.Preferences.PaychexControl";
                public const string PayrollPreferencesPayrollFringeReallocation = "Payroll.Preferences.PayrollFringeReallocation";

                #endregion

                #region BatchPosting Tab

                public const string BatchPosting = "BatchPosting";
                public const string BatchPostingBatches = "BatchPosting.Batches";

                #endregion

                #region Banking Tab

                public const string Banking = "Banking";
                public const string BankingReceiptsOrTransfers = "Banking.Receipts/Transfers";
                public const string BankingAch = "Banking.ACH";
                public const string BankingPostivePay = "Banking.PostivePay";
                public const string BankingBankSetup = "Banking.BankSetup";
                public const string BankingReconciliation = "Banking.BankReconciliation";
                #endregion


                #region Organization Tabs

                public const string AdministrationOrganizationUnitsCompanySetup = "Administration.OrganizationUnits.CompanySetup";
                public const string AdministrationOrganizationUnitsCompanyPreferences = "Administration.OrganizationUnits.CompanyPreferences";
                public const string AdministrationOrganizationUnitsMembers = "Administration.OrganizationUnits.Members";


                #endregion

            }
        }

        public static class Frontend
        {
            public const string Home = "Frontend.Home";
            public const string About = "Frontend.About";
        }
    }
}