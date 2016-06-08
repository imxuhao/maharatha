namespace CAPS.CORPACCOUNTING.Authorization
{
    /// <summary>
    /// Defines string constants for application's permission names.
    /// <see cref="AppAuthorizationProvider"/> for permission definitions.
    /// </summary>
    public static class AppPermissions
    {
        //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

        public const string Pages = "Pages";

        public const string Pages_Administration = "Pages.Administration";

        public const string Pages_Administration_Roles = "Pages.Administration.Roles";
        public const string Pages_Administration_Roles_Create = "Pages.Administration.Roles.Create";
        public const string Pages_Administration_Roles_Edit = "Pages.Administration.Roles.Edit";
        public const string Pages_Administration_Roles_Delete = "Pages.Administration.Roles.Delete";

        public const string Pages_Administration_Users = "Pages.Administration.Users";
        public const string Pages_Administration_Users_Create = "Pages.Administration.Users.Create";
        public const string Pages_Administration_Users_Edit = "Pages.Administration.Users.Edit";
        public const string Pages_Administration_Users_Delete = "Pages.Administration.Users.Delete";
        public const string Pages_Administration_Users_ChangePermissions = "Pages.Administration.Users.ChangePermissions";
        public const string Pages_Administration_Users_Impersonation = "Pages.Administration.Users.Impersonation";

        public const string Pages_Administration_Languages = "Pages.Administration.Languages";
        public const string Pages_Administration_Languages_Create = "Pages.Administration.Languages.Create";
        public const string Pages_Administration_Languages_Edit = "Pages.Administration.Languages.Edit";
        public const string Pages_Administration_Languages_Delete = "Pages.Administration.Languages.Delete";
        public const string Pages_Administration_Languages_ChangeTexts = "Pages.Administration.Languages.ChangeTexts";

        public const string Pages_Administration_AuditLogs = "Pages.Administration.AuditLogs";

        public const string Pages_Administration_OrganizationUnits = "Pages.Administration.OrganizationUnits";
        public const string Pages_Administration_OrganizationUnits_ManageOrganizationTree = "Pages.Administration.OrganizationUnits.ManageOrganizationTree";
        public const string Pages_Administration_OrganizationUnits_ManageMembers = "Pages.Administration.OrganizationUnits.ManageMembers";

        //TENANT-SPECIFIC PERMISSIONS

        public const string Pages_Tenant_Dashboard = "Pages.Tenant.Dashboard";

        public const string Pages_Administration_Tenant_Settings = "Pages.Administration.Tenant.Settings";

        //HOST-SPECIFIC PERMISSIONS

        public const string Pages_Editions = "Pages.Editions";
        public const string Pages_Editions_Create = "Pages.Editions.Create";
        public const string Pages_Editions_Edit = "Pages.Editions.Edit";
        public const string Pages_Editions_Delete = "Pages.Editions.Delete";

        public const string Pages_Tenants = "Pages.Tenants";
        public const string Pages_Tenants_Create = "Pages.Tenants.Create";
        public const string Pages_Tenants_Edit = "Pages.Tenants.Edit";
        public const string Pages_Tenants_ChangeFeatures = "Pages.Tenants.ChangeFeatures";
        public const string Pages_Tenants_Delete = "Pages.Tenants.Delete";
        public const string Pages_Tenants_Impersonation = "Pages.Tenants.Impersonation";

        public const string Pages_Administration_Host_Maintenance = "Pages.Administration.Host.Maintenance";

        public const string Pages_Administration_Host_Settings = "Pages.Administration.Host.Settings";

        #region Financials Tab
        // PERMISSIONS FOR CHARTOFACCOUNTS

        public const string Pages_Financials = "Pages.Financials";
        public const string Pages_Financials_Accounts = "Pages.Financials.Accounts";
        public const string Pages_Financials_Accounts_ChartOfAccounts = "Pages.Financials.Accounts.ChartOfAccounts";
        public const string Pages_Financials_Accounts_ChartOfAccounts_Create = "Pages.Financials.Accounts.ChartOfAccounts.Create";
        public const string Pages_Financials_Accounts_ChartOfAccounts_Edit = "Pages.Financials.Accounts.ChartOfAccounts.Edit";
        public const string Pages_Financials_Accounts_ChartOfAccounts_Delete = "Pages.Financials.Accounts.ChartOfAccounts.Delete";

        // PERMISSIONS FOR SUBACCOUNTS

        public const string Pages_Financials_Accounts_SubAccounts = "Pages.Financials.Accounts.SubAccounts";
        public const string Pages_Financials_Accounts_SubAccounts_Create = "Pages.Financials.Accounts.SubAccounts.Create";
        public const string Pages_Financials_Accounts_SubAccounts_Edit = "Pages.Financials.Accounts.SubAccounts.Edit";
        public const string Pages_Financials_Accounts_SubAccounts_Delete = "Pages.Financials.Accounts.SubAccounts.Delete";

        // PERMISSIONS FOR ACCOUNTS

        public const string Pages_Financials_Accounts_Accounts = "Pages.Financials.Accounts.Accounts";
        public const string Pages_Financials_Accounts_Accounts_Create = "Pages.Financials.Accounts.Accounts.Create";
        public const string Pages_Financials_Accounts_Accounts_Edit = "Pages.Financials.Accounts.Accounts.Edit";
        public const string Pages_Financials_Accounts_Accounts_Delete = "Pages.Financials.Accounts.Accounts.Delete";

        // PERMISSIONS FOR DIVISIONS

        public const string Pages_Financials_Accounts_Divisions = "Pages.Financials.Accounts.Divisions";
        public const string Pages_Financials_Accounts_Divisions_Create = "Pages.Financials.Accounts.Divisions.Create";
        public const string Pages_Financials_Accounts_Divisions_Edit = "Pages.Financials.Accounts.Divisions.Edit";
        public const string Pages_Financials_Accounts_Divisions_Delete = "Pages.Financials.Accounts.Divisions.Delete";

        // PERMISSIONS FOR JOURNALS ENTRY
        public const string Pages_Financials_Journals = "Pages.Financials.Journals";
        public const string Pages_Financials_Journals_Entry = "Pages.Financials.Journals.Entry";
        public const string Pages_Financials_Journals_Entry_Create = "Pages.Financials.Journals.Entry.Create";
        public const string Pages_Financials_Journals_Entry_Edit = "Pages.Financials.Journals.Entry.Edit";
        public const string Pages_Financials_Journals_Entry_Delete = "Pages.Financials.Journals.Entry.Delete";

        // PERMISSIONS FOR BANKING SEARCHTRANSACTIONS
        public const string Pages_Financials_Inquiry = "Pages.Financials.Inquiry";
        public const string Pages_Financials_Inquiry_SearchTransactions = "Pages.Financials.Inquiry.SearchTransactions";
        public const string Pages_Financials_Inquiry_SearchTransactions_Create = "Pages.Financials.Inquiry.SearchTransactions.Create";
        public const string Pages_Financials_Inquiry_SearchTransactions_Edit = "Pages.Financials.Inquiry.SearchTransactions.Edit";
        public const string Pages_Financials_Inquiry_SearchTransactions_Delete = "Pages.Financials.Inquiry.SearchTransactions.Delete";

        // PERMISSIONS FOR INQUIRY FINANCIALS
        public const string Pages_Financials_Inquiry_Financials = "Pages.Financials.Inquiry.Financials";
        public const string Pages_Financials_Inquiry_Financials_Create = "Pages.Financials.Inquiry.Financials.Create";
        public const string Pages_Financials_Inquiry_Financials_Edit = "Pages.Financials.Inquiry.Financials.Edit";
        public const string Pages_Financials_Inquiry_Financials_Delete = "Pages.Financials.Inquiry.Financials.Delete";

        // PERMISSIONS FOR INQUIRY JOURNALHISTORY
        public const string Pages_Financials_Inquiry_JournalHistory = "Pages.Financials.Inquiry.JournalHistory";
        public const string Pages_Financials_Inquiry_JournalHistory_Create = "Pages.Financials.Inquiry.JournalHistory.Create";
        public const string Pages_Financials_Inquiry_JournalHistory_Edit = "Pages.Financials.Inquiry.JournalHistory.Edit";
        public const string Pages_Financials_Inquiry_JournalHistory_Delete = "Pages.Financials.Inquiry.JournalHistory.Delete";

        // PERMISSIONS FOR INQUIRY ASSETTRACKING
        public const string Pages_Financials_Inquiry_AssetTracking = "Pages.Financials.Inquiry.AssetTracking";
        public const string Pages_Financials_Inquiry_AssetTracking_Create = "Pages.Financials.Inquiry.AssetTracking.Create";
        public const string Pages_Financials_Inquiry_AssetTracking_Edit = "Pages.Financials.Inquiry.AssetTracking.Edit";
        public const string Pages_Financials_Inquiry_AssetTracking_Delete = "Pages.Financials.Inquiry.AssetTracking.Delete";

        // PERMISSIONS FOR INQUIRY FISCALPERIOD
        public const string Pages_Financials_FiscalPeriod = "Pages.Financials.FiscalPeriod";
        public const string Pages_Financials_FiscalPeriod_Create = "Pages.Financials.FiscalPeriod.Create";
        public const string Pages_Financials_FiscalPeriod_Edit = "Pages.Financials.FiscalPeriod.Edit";
        public const string Pages_Financials_FiscalPeriod_Delete = "Pages.Financials.FiscalPeriod.Delete";
        #endregion

        #region Projects Tab
        // PERMISSIONS FOR PROJECTS PROJECTMAINTENANCE
        public const string Pages_Projects = "Pages.Projects";
        public const string Pages_Projects_ProjectMaintenance = "Pages.Projects.ProjectMaintenance";
        public const string Pages_Projects_ProjectMaintenance_Projects = "Pages.Projects.ProjectMaintenance.Projects";
        public const string Pages_Projects_ProjectMaintenance_Projects_Create = "Pages.Projects.ProjectMaintenance.Projects.Create";
        public const string Pages_Projects_ProjectMaintenance_Projects_Edit = "Pages.Projects.ProjectMaintenance.Projects.Edit";
        public const string Pages_Projects_ProjectMaintenance_Projects_Delete = "Pages.Projects.ProjectMaintenance.Projects.Delete";

        // PERMISSIONS FOR PROJECTS PROJECTCOAS
        public const string Pages_Projects_ProjectMaintenance_ProjectCOAs = "Pages.Projects.ProjectMaintenance.ProjectCOAs";
        public const string Pages_Projects_ProjectMaintenance_ProjectCOAs_Create = "Pages.Projects.ProjectMaintenance.ProjectCOAs.Create";
        public const string Pages_Projects_ProjectMaintenance_ProjectCOAs_Edit = "Pages.Projects.ProjectMaintenance.ProjectCOAs.Edit";
        public const string Pages_Projects_ProjectMaintenance_ProjectCOAs_Delete = "Pages.Projects.ProjectMaintenance.ProjectCOAs.Delete";

        // PERMISSIONS FOR PROJECTS PROJECTCOAS
        public const string Pages_Projects_ProjectMaintenance_Contracts = "Pages.Projects.ProjectMaintenance.Contracts";
        public const string Pages_Projects_ProjectMaintenance_Contracts_Create = "Pages.Projects.ProjectMaintenance.Contracts.Create";
        public const string Pages_Projects_ProjectMaintenance_Contracts_Edit = "Pages.Projects.ProjectMaintenance.Contracts.Edit";
        public const string Pages_Projects_ProjectMaintenance_Contracts_Delete = "Pages.Projects.ProjectMaintenance.Contracts.Delete";

        // PERMISSIONS FOR PROJECTS INQUIRY
        public const string Pages_Projects_Inquiry = "Pages.Projects.Inquiry";
        #endregion

        #region Receivables Tab


        public const string Pages_Receivables = "Pages.Receivables";
        public const string Pages_Receivables_Customers = "Pages.Receivables.Customers";
        public const string Pages_Receivables_Customers_History = "Pages.Receivables.Customers.History";
        public const string Pages_Receivables_Customers_History_Create = "Pages.Receivables.Customers.History.Create";
        public const string Pages_Receivables_Customers_History_Edit = "Pages.Receivables.Customers.History.Edit";
        public const string Pages_Receivables_Customers_History_Delete = "Pages.Receivables.Customers.History.Delete";

        public const string Pages_Receivables_Invoices = "Pages.Receivables.Invoices";
        public const string Pages_Receivables_Invoices_Entry = "Pages.Receivables.Invoices.Entry";
        public const string Pages_Receivables_Invoices_Entry_Create = "Pages.Receivables.Invoices.Entry.Create";
        public const string Pages_Receivables_Invoices_Entry_Edit = "Pages.Receivables.Invoices.Entry.Edit";
        public const string Pages_Receivables_Invoices_Entry_Delete = "Pages.Receivables.Invoices.Entry.Delete";

        public const string Pages_Receivables_Inquiry = "Pages.Receivables.Inquiry";
        public const string Pages_Receivables_Inquiry_ARInvoiceInquiry = "Pages.Receivables.Inquiry.ARInvoiceInquiry";
        public const string Pages_Receivables_Inquiry_CustomerSummary = "Pages.Receivables.Inquiry.CustomerSummary";
        public const string Pages_Receivables_Inquiry_InvoiceDetail = "Pages.Receivables.Inquiry.InvoiceDetail";

        public const string Pages_Receivables_Preferences = "Pages.Receivables.Preferences";
        public const string Pages_Receivables_Preferences_BillingTypes = "Pages.Receivables.Preferences.BillingTypes";
        public const string Pages_Receivables_Preferences_BillingTypes_Create = "Pages.Receivables.Preferences.BillingTypes.Create";
        public const string Pages_Receivables_Preferences_BillingTypes_Edit = "Pages.Receivables.Preferences.BillingTypes.Edit";
        public const string Pages_Receivables_Preferences_BillingTypes_Delete = "Pages.Receivables.Preferences.BillingTypes.Delete";

        public const string Pages_Receivables_Preferences_Territories = "Pages.Receivables.Preferences.Territories";
        public const string Pages_Receivables_Preferences_Territories_Create = "Pages.Receivables.Preferences.Territories.Create";
        public const string Pages_Receivables_Preferences_Territories_Edit = "Pages.Receivables.Preferences.Territories.Edit";
        public const string Pages_Receivables_Preferences_Territories_Delete = "Pages.Receivables.Preferences.Territories.Delete";

        public const string Pages_Receivables_Preferences_PaymentTerms = "Pages.Receivables.Preferences.PaymentTerms";
        public const string Pages_Receivables_Preferences_PaymentTerms_Create = "Pages.Receivables.Preferences.PaymentTerms.Create";
        public const string Pages_Receivables_Preferences_PaymentTerms_Edit = "Pages.Receivables.Preferences.PaymentTerms.Edit";
        public const string Pages_Receivables_Preferences_PaymentTerms_Delete = "Pages.Receivables.Preferences.PaymentTerms.Delete";

        public const string Pages_Receivables_Preferences_ARInvoiceTemplate = "Pages.Receivables.Preferences.ARInvoiceTemplate";
        public const string Pages_Receivables_Preferences_ARInvoiceTemplate_Create = "Pages.Receivables.Preferences.ARInvoiceTemplate.Create";
        public const string Pages_Receivables_Preferences_ARInvoiceTemplate_Edit = "Pages.Receivables.Preferences.ARInvoiceTemplate.Edit";
        public const string Pages_Receivables_Preferences_ARInvoiceTemplate_Delete = "Pages.Receivables.Preferences.ARInvoiceTemplate.Delete";



        #endregion

        #region Payables Tab

        public const string Pages_Payables = "Pages.Payables";
        public const string Pages_Payables_Vendors = "Pages.Payables.Vendors";
        public const string Pages_Payables_Vendors_Create = "Pages.Payables.Vendors.Create";
        public const string Pages_Payables_Vendors_Edit = "Pages.Payables.Vendors.Edit";
        public const string Pages_Payables_Vendors_Delete = "Pages.Payables.Vendors.Delete";

        public const string Pages_Payables_Invoices = "Pages.Payables.Invoices";
        public const string Pages_Payables_Invoices_Create = "Pages.Payables.Invoices.Create";
        public const string Pages_Payables_Invoices_Edit = "Pages.Payables.Invoices.Edit";
        public const string Pages_Payables_Invoices_Delete = "Pages.Payables.Invoices.Delete";

        public const string Pages_Payables_Inquiry = "Pages.Payables.Inquiry";
        public const string Pages_Payables_Inquiry_APInvoiceInquiry = "Pages.Payables.Inquiry.APInvoiceInquiry";
        public const string Pages_Payables_Inquiry_PaymentHistory = "Pages.Payables.Inquiry.PaymentHistory";
        public const string Pages_Payables_Inquiry_VendorSummary = "Pages.Payables.Inquiry.VendorSummary";
        public const string Pages_Payables_Inquiry_InvoiceDetail = "Pages.Payables.Inquiry.InvoiceDetail";
        public const string Pages_Payables_Preferences = "Pages.Payables.Preferences";
        public const string Pages_Payables_Preferences_1099T4Codes = "Pages.Payables.Preferences.1099T4Codes";
        public const string Pages_Payables_Preferences_VendorPaymentTerms = "Pages.Payables.Preferences.VendorPaymentTerms";
        public const string Pages_Payables_Preferences_VendorPaymentTerms_Create = "Pages.Payables.Preferences.VendorPaymentTerms.Create";
        public const string Pages_Payables_Preferences_VendorPaymentTerms_Edit = "Pages.Payables.Preferences.VendorPaymentTerms.Edit";
        public const string Pages_Payables_Preferences_VendorPaymentTerms_Delete = "Pages.Payables.Preferences.VendorPaymentTerms.Delete";
        public const string Pages_Payables_YEProcesses = "Pages.Payables.YEProcesses";
        public const string Pages_Payables_YEProcesses_1099s = "Pages.Payables.YEProcesses.1099s";

        #endregion

        #region Purchase Orders Tab
        public const string Pages_PurchaseOrders = "Pages.PurchaseOrders";
        public const string Pages_PurchaseOrders_Entry = "Pages.PurchaseOrders.Entry";
        #endregion
        
        #region Purchasing Tab
        public const string Pages_Purchasing = "Pages.Purchasing";
        public const string Pages_Purchasing_Inquiry = "Pages.Purchasing.Inquiry";
        public const string Pages_Purchasing_Inquiry_PurchaseOrderHistory = "Pages.Purchasing.Inquiry.PurchaseOrderHistory";
        public const string Pages_Purchasing_Inquiry_SearchPurchaseOrders = "Pages.Purchasing.Inquiry.SearchPurchaseOrders";
        public const string Pages_Purchasing_Inquiry_POAgingGrid = "Pages.Purchasing.Inquiry.POAgingGrid";
        
        #endregion

        #region Petty Cash Tab
        public const string Pages_PettyCash = "Pages.PettyCash";
        public const string Pages_PettyCash_PCVendors = "Pages.PettyCash.PCVendors";
        public const string Pages_PettyCash_Entry = "Pages.PettyCash.Entry";
        public const string Pages_PettyCash_PCVendors_History = "Pages.PettyCash.PCVendors.History";
        public const string Pages_PettyCash_Inquiry = "Pages.PettyCash.Inquiry";
        public const string Pages_PettyCash_Inquiry_PCAccountSummary = "Pages.PettyCash.Inquiry.PCAccountSummary";
        public const string Pages_PettyCash_Inquiry_PCAdvancedSearch = "Pages.PettyCash.Inquiry.PCAdvancedSearch";
        #endregion

        #region Credit Card Tab
        public const string Pages_CreditCard = "Pages.CreditCard";
        public const string Pages_CreditCard_Entry = "Pages.CreditCard.Entry";
        public const string Pages_CreditCard_Inquiry = "Pages.CreditCard.Inquiry";
        public const string Pages_CreditCard_Inquiry_CreditCardHistory = "Pages.CreditCard.Inquiry.CreditCardHistory";
        public const string Pages_CreditCard_Inquiry_CreditCardUploadInfo = "Pages.CreditCard.Inquiry.CreditCardUploadInfo";
        public const string Pages_CreditCard_Preferences = "Pages.CreditCard.Preferences";
        public const string Pages_CreditCard_Preferences_CreditCardCompanies = "Pages.CreditCard.Preferences.CreditCardCompanies";
        #endregion

        #region Payroll Tab
        public const string Pages_Payroll = "Pages.Payroll";
        public const string Pages_Payroll_Entry = "Pages.Payroll.Entry";
        public const string Pages_Payroll_Inquiry = "Pages.Payroll.Inquiry";
        public const string Pages_Payroll_Inquiry_PayrollHistory = "Pages.Payroll.Inquiry.PayrollHistory ";
        public const string Pages_Payroll_Inquiry_PayrollUploadInfo = "Pages.Payroll.Inquiry.PayrollUploadInfo";
        public const string Pages_Payroll_Inquiry_PayrollLog = "Pages.Payroll.Inquiry.PayrollLog";
        public const string Pages_Payroll_Preferences = "Pages.Payroll.Preferences";
        public const string Pages_Payroll_Preferences_PayrollCompanies = "Pages.Payroll.Preferences.PayrollCompanies";
        public const string Pages_Payroll_Preferences_PaychexControl = "Pages.Payroll.Preferences.PaychexControl";
        public const string Pages_Payroll_Preferences_PayrollFringeReallocation = "Pages.Payroll.Preferences.PayrollFringeReallocation";
        #endregion

        #region Batch Posting Tab
        public const string Pages_BatchPosting = "Pages.BatchPosting";

        public const string Pages_BatchPosting_Batches = "Pages.BatchPosting.Batches";
        public const string Pages_BatchPosting_Batches_Create = "Pages.BatchPosting.Batches.Create";
        public const string Pages_BatchPosting_Batches_Edit = "Pages.BatchPosting.Batches.Edit";
        public const string Pages_BatchPosting_Batches_Delete = "Pages.BatchPosting.Batches.Delete";

       
        #endregion

        #region Banking Tab
        public const string Pages_Banking = "Pages.Banking";
        // PERMISSIONS FOR BANKING RECEIPTS/TRANSFERS
        public const string Pages_Banking_ReceiptsOrTransfers = "Pages.Banking.ReceiptsOrTransfers";
        public const string Pages_Banking_ReceiptsOrTransfers_Create = "Pages.Banking.ReceiptsOrTransfers.Create";
        public const string Pages_Banking_ReceiptsOrTransfers_Edit = "Pages.Banking.ReceiptsOrTransfers.Edit";
        public const string Pages_Banking_ReceiptsOrTransfers_Delete = "Pages.Banking.ReceiptsOrTransfers.Delete";

        // PERMISSIONS FOR BANKING ACH
        public const string Pages_Banking_ACH = "Pages.Banking.ACH";
        public const string Pages_Banking_ACH_Create = "Pages.Banking.ACH.Create";
        public const string Pages_Banking_ACH_Edit = "Pages.Banking.ACH.Edit";
        public const string Pages_Banking_ACH_Delete = "Pages.Banking.ACH.Delete";

        // PERMISSIONS FOR BANKING RECONCILIATION
        public const string Pages_Banking_Reconciliation = "Pages.Banking.Reconciliation";
        public const string Pages_Banking_Reconciliation_Create = "Pages.Banking.Reconciliation.Create";
        public const string Pages_Banking_Reconciliation_Edit = "Pages.Banking.Reconciliation.Edit";
        public const string Pages_Banking_Reconciliation_Delete = "Pages.Banking.Reconciliation.Delete";

        // PERMISSIONS FOR BANKING POSTIVE PAY
        public const string Pages_Banking_PostivePay = "Pages.Banking.PostivePay";
        public const string Pages_Banking_PostivePay_Create = "Pages.Banking.PostivePay.Create";
        public const string Pages_Banking_PostivePay_Edit = "Pages.Banking.PostivePay.Edit";
        public const string Pages_Banking_PostivePay_Delete = "Pages.Banking.PostivePay.Delete";

        // PERMISSIONS FOR BANKING BANK SETUP
        public const string Pages_Banking_BankSetup = "Pages.Banking.BankSetup";
        public const string Pages_Banking_BankSetup_Create = "Pages.Banking.BankSetup.Create";
        public const string Pages_Banking_BankSetup_Edit = "Pages.Banking.BankSetup.Edit";
        public const string Pages_Banking_BankSetup_Delete = "Pages.Banking.BankSetup.Delete";


        #endregion

    }
}