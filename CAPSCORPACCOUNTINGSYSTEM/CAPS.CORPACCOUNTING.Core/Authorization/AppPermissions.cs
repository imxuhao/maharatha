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

        // PERMISSIONS FOR BANKING RECEIPTS/TRANSFERS
        public const string Pages_Financials_Banking = "Pages.Financials.Banking";
        public const string Pages_Financials_Banking_ReceiptsOrTransfers = "Pages.Financials.Banking.ReceiptsOrTransfers";
        public const string Pages_Financials_Banking_ReceiptsOrTransfers_Create = "Pages.Financials.Banking.ReceiptsOrTransfers.Create";
        public const string Pages_Financials_Banking_ReceiptsOrTransfers_Edit = "Pages.Financials.Banking.ReceiptsOrTransfers.Edit";
        public const string Pages_Financials_Banking_ReceiptsOrTransfers_Delete = "Pages.Financials.Banking.ReceiptsOrTransfers.Delete";

        // PERMISSIONS FOR BANKING ACH
        public const string Pages_Financials_Banking_ACH = "Pages.Financials.Banking.ACH";
        public const string Pages_Financials_Banking_ACH_Create = "Pages.Financials.Banking.ACH.Create";
        public const string Pages_Financials_Banking_ACH_Edit = "Pages.Financials.Banking.ACH.Edit";
        public const string Pages_Financials_Banking_ACH_Delete = "Pages.Financials.Banking.ACH.Delete";

        // PERMISSIONS FOR BANKING RECONCILIATION
        public const string Pages_Financials_Banking_Reconciliation = "Pages.Financials.Banking.Reconciliation";
        public const string Pages_Financials_Banking_Reconciliation_Create = "Pages.Financials.Banking.Reconciliation.Create";
        public const string Pages_Financials_Banking_Reconciliation_Edit = "Pages.Financials.Banking.Reconciliation.Edit";
        public const string Pages_Financials_Banking_Reconciliation_Delete = "Pages.Financials.Banking.Reconciliation.Delete";



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

        // PERMISSIONS FOR POSTING BATCHED
        public const string Pages_Financials_Posting = "Pages.Financials.Posting";
        public const string Pages_Financials_Posting_Batched = "Pages.Financials.Posting.Batched";
        public const string Pages_Financials_Posting_Batched_Create = "Pages.Financials.Posting.Batched.Create";
        public const string Pages_Financials_Posting_Batched_Edit = "Pages.Financials.Posting.Batched.Edit";
        public const string Pages_Financials_Posting_Batched_Delete = "Pages.Financials.Posting.Batched.Delete";

        // PERMISSIONS FOR POSTING UNBATCHED

        public const string Pages_Financials_Posting_UnBatched = "Pages.Financials.Posting.UnBatched";
        public const string Pages_Financials_Posting_UnBatched_Create = "Pages.Financials.Posting.UnBatched.Create";
        public const string Pages_Financials_Posting_UnBatched_Edit = "Pages.Financials.Posting.UnBatched.Edit";
        public const string Pages_Financials_Posting_UnBatched_Delete = "Pages.Financials.Posting.UnBatched.Delete";

        // PERMISSIONS FOR PREFERENCES FISCALPERIOD
        public const string Pages_Financials_Preferences = "Pages.Financials.Preferences";
        public const string Pages_Financials_Preferences_FiscalPeriod = "Pages.Financials.Preferences.FiscalPeriod";
        public const string Pages_Financials_Preferences_FiscalPeriod_Create = "Pages.Financials.Preferences.FiscalPeriod.Create";
        public const string Pages_Financials_Preferences_FiscalPeriod_Edit = "Pages.Financials.Preferences.FiscalPeriod.Edit";
        public const string Pages_Financials_Preferences_FiscalPeriod_Delete = "Pages.Financials.Preferences.FiscalPeriod.Delete";

        // PERMISSIONS FOR PREFERENCES BANKSETUP

        public const string Pages_Financials_Preferences_BankSetup = "Pages.Financials.Preferences.BankSetup";
        public const string Pages_Financials_Preferences_BankSetup_Create = "Pages.Financials.Preferences.BankSetup.Create";
        public const string Pages_Financials_Preferences_BankSetup_Edit = "Pages.Financials.Preferences.BankSetup.Edit";
        public const string Pages_Financials_Preferences_BankSetup_Delete = "Pages.Financials.Preferences.BankSetup.Delete";
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


        public const string Pages_Receivables = "pages.Receivables";
        public const string Pages_Receivables_Customers = "pages.Receivables.Customers";
        public const string Pages_Receivables_Customers_History = "pages.Receivables.Customers.History";
        public const string Pages_Receivables_Customers_History_Create = "pages.Receivables.Customers.History.Create";
        public const string Pages_Receivables_Customers_History_Edit = "pages.Receivables.Customers.History.Edit";
        public const string Pages_Receivables_Customers_History_Delete = "pages.Receivables.Customers.History.Delete";

        public const string Pages_Receivables_Invoices = "pages.Receivables.Invoices";
        public const string Pages_Receivables_Invoices_Entry = "pages.Receivables.Invoices.Entry";
        public const string Pages_Receivables_Invoices_Entry_Create = "pages.Receivables.Invoices.Entry.Create";
        public const string Pages_Receivables_Invoices_Entry_Edit = "pages.Receivables.Invoices.Entry.Edit";
        public const string Pages_Receivables_Invoices_Entry_Delete = "pages.Receivables.Invoices.Entry.Delete";

        public const string Pages_Receivables_Inquiry = "pages.Receivables.Inquiry";
        public const string Pages_Receivables_Inquiry_ARInvoiceInquiry = "pages.Receivables.Inquiry.ARInvoiceInquiry";
        public const string Pages_Receivables_Inquiry_ARInvoiceInquiry_Create = "pages.Receivables.Inquiry.ARInvoiceInquiry.Create";
        public const string Pages_Receivables_Inquiry_ARInvoiceInquiry_Edit = "pages.Receivables.Inquiry.ARInvoiceInquiry.Edit";
        public const string Pages_Receivables_Inquiry_ARInvoiceInquiry_Delete = "pages.Receivables.Inquiry.ARInvoiceInquiry.Delete";

        public const string Pages_Receivables_Inquiry_CustomerSummary = "pages.Receivables.Inquiry.CustomerSummary";
        public const string Pages_Receivables_Inquiry_CustomerSummary_Create = "pages.Receivables.Inquiry.CustomerSummary.Create";
        public const string Pages_Receivables_Inquiry_CustomerSummary_Edit = "pages.Receivables.Inquiry.CustomerSummary.Edit";
        public const string Pages_Receivables_Inquiry_CustomerSummary_Delete = "pages.Receivables.Inquiry.CustomerSummary.Delete";

        public const string Pages_Receivables_Inquiry_InvoiceDetail = "pages.Receivables.Inquiry.InvoiceDetail";
        public const string Pages_Receivables_Inquiry_InvoiceDetail_Create = "pages.Receivables.Inquiry.InvoiceDetail.Create";
        public const string Pages_Receivables_Inquiry_InvoiceDetail_Edit = "pages.Receivables.Inquiry.InvoiceDetail.Edit";
        public const string Pages_Receivables_Inquiry_InvoiceDetail_Delete = "pages.Receivables.Inquiry.InvoiceDetail.Delete";

        public const string Pages_Receivables_Preferences = "pages.Receivables.Preferences";
        public const string Pages_Receivables_Preferences_BillingTypes = "pages.Receivables.Preferences.BillingTypes";
        public const string Pages_Receivables_Preferences_BillingTypes_Create = "pages.Receivables.Preferences.BillingTypes.Create";
        public const string Pages_Receivables_Preferences_BillingTypes_Edit = "pages.Receivables.Preferences.BillingTypes.Edit";
        public const string Pages_Receivables_Preferences_BillingTypes_Delete = "pages.Receivables.Preferences.BillingTypes.Delete";

        public const string Pages_Receivables_Preferences_Territories = "pages.Receivables.Preferences.Territories";
        public const string Pages_Receivables_Preferences_Territories_Create = "pages.Receivables.Preferences.Territories.Create";
        public const string Pages_Receivables_Preferences_Territories_Edit = "pages.Receivables.Preferences.Territories.Edit";
        public const string Pages_Receivables_Preferences_Territories_Delete = "pages.Receivables.Preferences.Territories.Delete";

        public const string Pages_Receivables_Preferences_PaymentTerms = "pages.Receivables.Preferences.PaymentTerms";
        public const string Pages_Receivables_Preferences_PaymentTerms_Create = "pages.Receivables.Preferences.PaymentTerms.Create";
        public const string Pages_Receivables_Preferences_PaymentTerms_Edit = "pages.Receivables.Preferences.PaymentTerms.Edit";
        public const string Pages_Receivables_Preferences_PaymentTerms_Delete = "pages.Receivables.Preferences.PaymentTerms.Delete";

        public const string Pages_Receivables_Preferences_ARInvoiceTemplate = "pages.Receivables.Preferences.ARInvoiceTemplate";
        public const string Pages_Receivables_Preferences_ARInvoiceTemplate_Create = "pages.Receivables.Preferences.ARInvoiceTemplate.Create";
        public const string Pages_Receivables_Preferences_ARInvoiceTemplate_Edit = "pages.Receivables.Preferences.ARInvoiceTemplate.Edit";
        public const string Pages_Receivables_Preferences_ARInvoiceTemplate_Delete = "pages.Receivables.Preferences.ARInvoiceTemplate.Delete";



        #endregion

        #region Payables Tab

        public const string Pages_Payables = "pages.Payables";

        public const string Pages_Payables_Vendors = "pages.Payables.Vendors";

        public const string Pages_Payables_Invoices = "pages.Payables.Invoices";
        public const string Pages_Payables_Invoices_Batch = "pages.Payables.Invoices.Batch";
        public const string Pages_Payables_Invoices_Batch_Create = "pages.Payables.Invoices.Batch.Create";
        public const string Pages_Payables_Invoices_Batch_Edit = "pages.Payables.Invoices.Batch.Edit";
        public const string Pages_Payables_Invoices_Batch_Delete = "pages.Payables.Invoices.Batch.Delete";

        public const string Pages_Payables_Inquiry = "pages.Payables.Inquiry";
        public const string Pages_Payables_Inquiry_APInvoiceInquiry = "pages.Payables.Inquiry.APInvoiceInquiry";
        public const string Pages_Payables_Inquiry_APInvoiceInquiry_Create = "pages.Payables.Inquiry.APInvoiceInquiry.Create";
        public const string Pages_Payables_Inquiry_APInvoiceInquiry_Edit = "pages.Payables.Inquiry.APInvoiceInquiry.Edit";
        public const string Pages_Payables_Inquiry_APInvoiceInquiry_Delete = "pages.Payables.Inquiry.APInvoiceInquiry.Delete";

        public const string Pages_Payables_Inquiry_PaymentHistory = "pages.Payables.Inquiry.PaymentHistory";
        public const string Pages_Payables_Inquiry_PaymentHistory_Create = "pages.Payables.Inquiry.PaymentHistory.Create";
        public const string Pages_Payables_Inquiry_PaymentHistory_Edit = "pages.Payables.Inquiry.PaymentHistory.Edit";
        public const string Pages_Payables_Inquiry_PaymentHistory_Delete = "pages.Payables.Inquiry.PaymentHistory.Delete";

        public const string Pages_Payables_Inquiry_VendorSummary = "pages.Payables.Inquiry.VendorSummary";
        public const string Pages_Payables_Inquiry_VendorSummary_Create = "pages.Payables.Inquiry.VendorSummary.Create";
        public const string Pages_Payables_Inquiry_VendorSummary_Edit = "pages.Payables.Inquiry.VendorSummary.Edit";
        public const string Pages_Payables_Inquiry_VendorSummary_Delete = "pages.Payables.Inquiry.VendorSummary.Delete";

        public const string Pages_Payables_Inquiry_InvoiceDetail = "pages.Payables.Inquiry.InvoiceDetail";
        public const string Pages_Payables_Inquiry_InvoiceDetail_Create = "pages.Payables.Inquiry.InvoiceDetail.Create";
        public const string Pages_Payables_Inquiry_InvoiceDetail_Edit = "pages.Payables.Inquiry.InvoiceDetail.Edit";
        public const string Pages_Payables_Inquiry_InvoiceDetail_Delete = "pages.Payables.Inquiry.InvoiceDetail.Delete";

        public const string Pages_Payables_Preferences = "pages.Payables.Preferences";
        public const string Pages_Payables_Preferences_1099T4Codes = "pages.Payables.Preferences.1099T4Codes";
        public const string Pages_Payables_Preferences_1099T4Codes_Create = "pages.Payables.Preferences.1099T4Codes.Create";
        public const string Pages_Payables_Preferences_1099T4Codes_Edit = "pages.Payables.Preferences.1099T4Codes.Edit";
        public const string Pages_Payables_Preferences_1099T4Codes_Delete = "pages.Payables.Preferences.1099T4Codes.Delete";

        public const string Pages_Payables_Preferences_VendorPaymentTerms = "pages.Payables.Preferences.VendorPaymentTerms";
        public const string Pages_Payables_Preferences_VendorPaymentTerms_Create = "pages.Payables.Preferences.VendorPaymentTerms.Create";
        public const string Pages_Payables_Preferences_VendorPaymentTerms_Edit = "pages.Payables.Preferences.VendorPaymentTerms.Edit";
        public const string Pages_Payables_Preferences_VendorPaymentTerms_Delete = "pages.Payables.Preferences.VendorPaymentTerms.Delete";

        public const string Pages_Payables_YEProcesses = "pages.Payables.YEProcesses";
        public const string Pages_Payables_YEProcesses_1099s = "pages.Payables.YEProcesses.1099s";
        public const string Pages_Payables_YEProcesses_1099s_Create = "pages.Payables.YEProcesses.1099s.Create";
        public const string Pages_Payables_YEProcesses_1099s_Edit = "pages.Payables.YEProcesses.1099s.Edit";
        public const string Pages_Payables_YEProcesses_1099s_Delete = "pages.Payables.YEProcesses.1099s.Delete";

        #endregion

        #region Purchase Orders Tab
        public const string Pages_PurchaseOrders = "pages.PurchaseOrders";

        public const string Pages_PurchaseOrders_Entry = "pages.Payables.Entry";

        #endregion
        
        #region Purchasing Tab
        public const string Pages_Purchasing = "pages.Purchasing";
        public const string Pages_Purchasing_Inquiry = "pages.Purchasing.Inquiry";

        public const string Pages_Purchasing_Inquiry_PurchaseOrderHistory = "pages.Purchasing.Inquiry.PurchaseOrderHistory";
        public const string Pages_Purchasing_Inquiry_PurchaseOrderHistory_Create = "pages.Purchasing.Inquiry.PurchaseOrderHistory.Create";
        public const string Pages_Purchasing_Inquiry_PurchaseOrderHistory_Edit = "pages.Purchasing.Inquiry.PurchaseOrderHistory.Edit";
        public const string Pages_Purchasing_Inquiry_PurchaseOrderHistory_Delete = "pages.Purchasing.Inquiry.PurchaseOrderHistory.Delete";

        public const string Pages_Purchasing_Inquiry_SearchPurchaseOrders = "pages.Purchasing.Inquiry.SearchPurchaseOrders";
        public const string Pages_Purchasing_Inquiry_SearchPurchaseOrders_Create = "pages.Purchasing.Inquiry.SearchPurchaseOrders.Create";
        public const string Pages_Purchasing_Inquiry_SearchPurchaseOrders_Edit = "pages.Purchasing.Inquiry.SearchPurchaseOrders.Edit";
        public const string Pages_Purchasing_Inquiry_SearchPurchaseOrders_Delete = "pages.Purchasing.Inquiry.SearchPurchaseOrders.Delete";

        public const string Pages_Purchasing_Inquiry_POAgingGrid = "pages.Purchasing.Inquiry.POAgingGrid";
        public const string Pages_Purchasing_Inquiry_POAgingGrid_Create = "pages.Purchasing.Inquiry.POAgingGrid.Create";
        public const string Pages_Purchasing_Inquiry_POAgingGrid_Edit = "pages.Purchasing.Inquiry.POAgingGrid.Edit";
        public const string Pages_Purchasing_Inquiry_POAgingGrid_Delete = "pages.Purchasing.Inquiry.POAgingGrid.Delete";
        #endregion

        #region Petty Cash Tab

        public const string Pages_PettyCash = "pages.PettyCash";
        public const string Pages_PettyCash_PCVendors = "pages.PettyCash.PCVendors";
        public const string Pages_PettyCash_Entry = "pages.PettyCash.Entry";

        public const string Pages_PettyCash_PCVendors_History = "pages.PettyCash.PCVendors.History";
        public const string Pages_PettyCash_PCVendors_History_Create = "pages.PettyCash.PCVendors.History.Create";
        public const string Pages_PettyCash_PCVendors_History_Edit = "pages.PettyCash.PCVendors.History.Edit";
        public const string Pages_PettyCash_PCVendors_History_Delete = "pages.PettyCash.PCVendors.History.Delete";

        public const string Pages_PettyCash_Inquiry = "pages.PettyCash.Inquiry";
        public const string Pages_PettyCash_Inquiry_PCAccountSummary = "pages.PettyCash.Inquiry.PCAccountSummary";
        public const string Pages_PettyCash_Inquiry_PCAccountSummary_Create = "pages.PettyCash.Inquiry.PCAccountSummary.Create";
        public const string Pages_PettyCash_Inquiry_PCAccountSummary_Edit = "pages.PettyCash.Inquiry.PCAccountSummary.Edit";
        public const string Pages_PettyCash_Inquiry_PCAccountSummary_Delete = "pages.PettyCash.Inquiry.PCAccountSummary.Delete";

        public const string Pages_PettyCash_Inquiry_PCAdvancedSearch = "pages.PettyCash.Inquiry.PCAdvancedSearch";
        public const string Pages_PettyCash_Inquiry_PCAdvancedSearch_Create = "pages.PettyCash.Inquiry.PCAdvancedSearch.Create";
        public const string Pages_PettyCash_Inquiry_PCAdvancedSearch_Edit = "pages.PettyCash.Inquiry.PCAdvancedSearch.Edit";
        public const string Pages_PettyCash_Inquiry_PCAdvancedSearch_Delete = "pages.PettyCash.Inquiry.PCAdvancedSearch.Delete";

        #endregion

        #region Credit Card Tab
        public const string Pages_CreditCard = "pages.CreditCard";
        public const string Pages_CreditCard_Entry = "pages.CreditCard.Entry";

        public const string Pages_CreditCard_Inquiry = "pages.CreditCard.Inquiry";
        public const string Pages_CreditCard_Inquiry_CreditCardHistory = "pages.CreditCard.Inquiry.CreditCardHistory";
        public const string Pages_CreditCard_Inquiry_CreditCardHistory_Create = "pages.CreditCard.Inquiry.CreditCardHistory.Create";
        public const string Pages_CreditCard_Inquiry_CreditCardHistory_Edit = "pages.CreditCard.Inquiry.CreditCardHistory.Edit";
        public const string Pages_CreditCard_Inquiry_CreditCardHistory_Delete = "pages.CreditCard.Inquiry.CreditCardHistory.Delete";

        public const string Pages_CreditCard_Inquiry_CreditCardUploadInfo = "pages.CreditCard.Inquiry.CreditCardUploadInfo";
        public const string Pages_CreditCard_Inquiry_CreditCardUploadInfo_Create = "pages.CreditCard.Inquiry.CreditCardUploadInfo.Create";
        public const string Pages_CreditCard_Inquiry_CreditCardUploadInfo_Edit = "pages.CreditCard.Inquiry.CreditCardUploadInfo.Edit";
        public const string Pages_CreditCard_Inquiry_CreditCardUploadInfo_Delete = "pages.CreditCard.Inquiry.CreditCardUploadInfo.Delete";

        public const string Pages_CreditCard_Preferences = "pages.CreditCard.Preferences";
        public const string Pages_CreditCard_Preferences_CreditCardCompanies = "pages.CreditCard.Preferences.CreditCardCompanies";
        public const string Pages_CreditCard_Preferences_CreditCardCompanies_Create = "pages.CreditCard.Preferences.CreditCardCompanies.Create";
        public const string Pages_CreditCard_Preferences_CreditCardCompanies_Edit = "pages.CreditCard.Preferences.CreditCardCompanies.Edit";
        public const string Pages_CreditCard_Preferences_CreditCardCompanies_Delete = "pages.CreditCard.Preferences.CreditCardCompanies.Delete";

        #endregion

        #region Payroll Tab
        public const string Pages_Payroll = "pages.Payroll";
        public const string Pages_Payroll_Entry = "pages.Payroll.Entry";

        public const string Pages_Payroll_Inquiry = "pages.Payroll.Inquiry";
        public const string Pages_Payroll_Inquiry_PayrollHistory = "pages.Payroll.Inquiry.PayrollHistory ";
        public const string Pages_Payroll_Inquiry_PayrollHistory_Create = "pages.Payroll.Inquiry.PayrollHistory.Create";
        public const string Pages_Payroll_Inquiry_PayrollHistory_Edit = "pages.Payroll.Inquiry.PayrollHistory.Edit";
        public const string Pages_Payroll_Inquiry_PayrollHistory_Delete = "pages.Payroll.Inquiry.PayrollHistory.Delete";

        public const string Pages_Payroll_Inquiry_PayrollUploadInfo = "pages.Payroll.Inquiry.PayrollUploadInfo";
        public const string Pages_Payroll_Inquiry_PayrollUploadInfo_Create = "pages.Payroll.Inquiry.PayrollUploadInfo.Create";
        public const string Pages_Payroll_Inquiry_PayrollUploadInfo_Edit = "pages.Payroll.Inquiry.PayrollUploadInfo.Edit";
        public const string Pages_Payroll_Inquiry_PayrollUploadInfo_Delete = "pages.Payroll.Inquiry.PayrollUploadInfo.Delete";

        public const string Pages_Payroll_Inquiry_PayrollLog = "pages.Payroll.Inquiry.PayrollLog";
        public const string Pages_Payroll_Inquiry_PayrollLog_Create = "pages.Payroll.Inquiry.PayrollLog.Create";
        public const string Pages_Payroll_Inquiry_PayrollLog_Edit = "pages.Payroll.Inquiry.PayrollLog.Edit";
        public const string Pages_Payroll_Inquiry_PayrollLog_Delete = "pages.Payroll.Inquiry.PayrollLog.Delete";

        public const string Pages_Payroll_Preferences = "pages.Payroll.Preferences";
        public const string Pages_Payroll_Preferences_PayrollCompanies = "pages.Payroll.Preferences.PayrollCompanies";
        public const string Pages_Payroll_Preferences_PayrollCompanies_Create = "pages.Payroll.Preferences.PayrollCompanies.Create";
        public const string Pages_Payroll_Preferences_PayrollCompanies_Edit = "pages.Payroll.Preferences.PayrollCompanies.Edit";
        public const string Pages_Payroll_Preferences_PayrollCompanies_Delete = "pages.Payroll.Preferences.PayrollCompanies.Delete";

        public const string Pages_Payroll_Preferences_PaychexControl = "pages.Payroll.Preferences.PaychexControl";
        public const string Pages_Payroll_Preferences_PaychexControl_Create = "pages.Payroll.Preferences.PaychexControl.Create";
        public const string Pages_Payroll_Preferences_PaychexControl_Edit = "pages.Payroll.Preferences.PaychexControl.Edit";
        public const string Pages_Payroll_Preferences_PaychexControl_Delete = "pages.Payroll.Preferences.PaychexControl.Delete";

        public const string Pages_Payroll_Preferences_PayrollFringeReallocation = "pages.Payroll.Preferences.PayrollFringeReallocation";
        public const string Pages_Payroll_Preferences_PayrollFringeReallocation_Create = "pages.Payroll.Preferences.PayrollFringeReallocation.Create";
        public const string Pages_Payroll_Preferences_PayrollFringeReallocation_Edit = "pages.Payroll.Preferences.PayrollFringeReallocation.Edit";
        public const string Pages_Payroll_Preferences_PayrollFringeReallocation_Delete = "pages.Payroll.Preferences.PayrollFringeReallocation.Delete";

        #endregion

        #region Posting Tab
        public const string Pages_Posting = "pages.Posting";
        #endregion

    }
}