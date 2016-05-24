using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace CAPS.CORPACCOUNTING.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));

            var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: MultiTenancySides.Host);

            #region financials Tab
            // PERMISSIONS FOR CHARTOFACCOUNTS

            var financials = pages.CreateChildPermission(AppPermissions.Pages_Financials, L("Financials"));
            var accounts = financials.CreateChildPermission(AppPermissions.Pages_Financials_Accounts, L("Accounts"));

            var chartOfAccounts = accounts.CreateChildPermission(AppPermissions.Pages_Financials_Accounts_ChartOfAccounts, L("ChartOfAccount"));
            chartOfAccounts.CreateChildPermission(AppPermissions.Pages_Financials_Accounts_ChartOfAccounts_Create, L("Create"));
            chartOfAccounts.CreateChildPermission(AppPermissions.Pages_Financials_Accounts_ChartOfAccounts_Edit, L("Edit"));
            chartOfAccounts.CreateChildPermission(AppPermissions.Pages_Financials_Accounts_ChartOfAccounts_Delete, L("Delete"));

            // PERMISSIONS FOR SUBACCOUNTS
            var subAccounts = accounts.CreateChildPermission(AppPermissions.Pages_Financials_Accounts_SubAccounts, L("SubAccounts"));
            subAccounts.CreateChildPermission(AppPermissions.Pages_Financials_Accounts_SubAccounts_Create, L("Create"));
            subAccounts.CreateChildPermission(AppPermissions.Pages_Financials_Accounts_SubAccounts_Edit, L("Edit"));
            subAccounts.CreateChildPermission(AppPermissions.Pages_Financials_Accounts_SubAccounts_Delete, L("Delete"));



            #region Accounts
            var financialsAccounts = accounts.CreateChildPermission(AppPermissions.Pages_Financials_Accounts_Accounts, L("Accounts"));
            financialsAccounts.CreateChildPermission(AppPermissions.Pages_Financials_Accounts_Accounts_Create, L("Create"));
            financialsAccounts.CreateChildPermission(AppPermissions.Pages_Financials_Accounts_Accounts_Edit, L("Edit"));
            financialsAccounts.CreateChildPermission(AppPermissions.Pages_Financials_Accounts_Accounts_Delete, L("Delete"));
        #endregion

        // PERMISSIONS FOR DIVISIONS
        var divisions = accounts.CreateChildPermission(AppPermissions.Pages_Financials_Accounts_Divisions, L("Divisions"));
            divisions.CreateChildPermission(AppPermissions.Pages_Financials_Accounts_Divisions_Create, L("Create"));
            divisions.CreateChildPermission(AppPermissions.Pages_Financials_Accounts_Divisions_Edit, L("Edit"));
            divisions.CreateChildPermission(AppPermissions.Pages_Financials_Accounts_Divisions_Delete, L("Delete"));

            var journals = financials.CreateChildPermission(AppPermissions.Pages_Financials_Journals, L("Journals"));
            var entry = journals.CreateChildPermission(AppPermissions.Pages_Financials_Journals_Entry, L("Entry"));
            entry.CreateChildPermission(AppPermissions.Pages_Financials_Journals_Entry_Create, L("Create"));
            entry.CreateChildPermission(AppPermissions.Pages_Financials_Journals_Entry_Edit, L("Edit"));
            entry.CreateChildPermission(AppPermissions.Pages_Financials_Journals_Entry_Delete, L("Delete"));

            var banking = financials.CreateChildPermission(AppPermissions.Pages_Financials_Banking, L("Banking"));
            var receiptsOrTransfers = banking.CreateChildPermission(AppPermissions.Pages_Financials_Banking_ReceiptsOrTransfers, L("ReceiptsOrTransfers"));
            receiptsOrTransfers.CreateChildPermission(AppPermissions.Pages_Financials_Banking_ReceiptsOrTransfers_Create, L("Create"));
            receiptsOrTransfers.CreateChildPermission(AppPermissions.Pages_Financials_Banking_ReceiptsOrTransfers_Edit, L("Edit"));
            receiptsOrTransfers.CreateChildPermission(AppPermissions.Pages_Financials_Banking_ReceiptsOrTransfers_Delete, L("Delete"));

            var ach = banking.CreateChildPermission(AppPermissions.Pages_Financials_Banking_ACH, L("ACH"));
            ach.CreateChildPermission(AppPermissions.Pages_Financials_Banking_ACH_Create, L("Create"));
            ach.CreateChildPermission(AppPermissions.Pages_Financials_Banking_ACH_Edit, L("Edit"));
            ach.CreateChildPermission(AppPermissions.Pages_Financials_Banking_ACH_Delete, L("Delete"));

            var reconciliation = banking.CreateChildPermission(AppPermissions.Pages_Financials_Banking_Reconciliation, L("BankReconciliation"));
            reconciliation.CreateChildPermission(AppPermissions.Pages_Financials_Banking_Reconciliation_Create, L("Create"));
            reconciliation.CreateChildPermission(AppPermissions.Pages_Financials_Banking_Reconciliation_Edit, L("Edit"));
            reconciliation.CreateChildPermission(AppPermissions.Pages_Financials_Banking_Reconciliation_Delete, L("Delete"));
            var inquiry = financials.CreateChildPermission(AppPermissions.Pages_Financials_Inquiry, L("Inquiry"));
            var searchTransactions = inquiry.CreateChildPermission(AppPermissions.Pages_Financials_Inquiry_SearchTransactions, L("SearchTransactions"));
            var inquiryFinancials = inquiry.CreateChildPermission(AppPermissions.Pages_Financials_Inquiry_Financials, L("Financials"));
            var journalHistory = inquiry.CreateChildPermission(AppPermissions.Pages_Financials_Inquiry_JournalHistory, L("JournalHistory"));
            var assetTracking = inquiry.CreateChildPermission(AppPermissions.Pages_Financials_Inquiry_AssetTracking, L("AssetTracking"));
            var financialsPosting = financials.CreateChildPermission(AppPermissions.Pages_Financials_Posting, L("Posting"));
            var batched = financialsPosting.CreateChildPermission(AppPermissions.Pages_Financials_Posting_Batched, L("Batched"));
            var unBatched = financialsPosting.CreateChildPermission(AppPermissions.Pages_Financials_Posting_UnBatched, L("UnBatched"));
            var preferences = financials.CreateChildPermission(AppPermissions.Pages_Financials_Preferences, L("Preferences"));
            var fiscalPeriod = preferences.CreateChildPermission(AppPermissions.Pages_Financials_Preferences_FiscalPeriod, L("FiscalPeriod"));
            fiscalPeriod.CreateChildPermission(AppPermissions.Pages_Financials_Preferences_FiscalPeriod_Create, L("Create"));
            fiscalPeriod.CreateChildPermission(AppPermissions.Pages_Financials_Preferences_FiscalPeriod_Edit, L("Edit"));
            fiscalPeriod.CreateChildPermission(AppPermissions.Pages_Financials_Preferences_FiscalPeriod_Delete, L("Delete"));

            var bankSetup = preferences.CreateChildPermission(AppPermissions.Pages_Financials_Preferences_BankSetup, L("BankSetup"));
            bankSetup.CreateChildPermission(AppPermissions.Pages_Financials_Preferences_BankSetup_Create, L("Create"));
            bankSetup.CreateChildPermission(AppPermissions.Pages_Financials_Preferences_BankSetup_Edit, L("Edit"));
            bankSetup.CreateChildPermission(AppPermissions.Pages_Financials_Preferences_BankSetup_Delete, L("Delete"));
            #endregion

            #region Projects Tab
            var projects = pages.CreateChildPermission(AppPermissions.Pages_Projects, L("Projects"));
            var projectMaintenance = projects.CreateChildPermission(AppPermissions.Pages_Projects_ProjectMaintenance, L("ProjectMaintenance"));
            var projectMaintenanceProjects = projectMaintenance.CreateChildPermission(AppPermissions.Pages_Projects_ProjectMaintenance_Projects, L("Projects"));
            projectMaintenanceProjects.CreateChildPermission(AppPermissions.Pages_Projects_ProjectMaintenance_Projects_Create, L("Create"));
            projectMaintenanceProjects.CreateChildPermission(AppPermissions.Pages_Projects_ProjectMaintenance_Projects_Edit, L("Edit"));
            projectMaintenanceProjects.CreateChildPermission(AppPermissions.Pages_Projects_ProjectMaintenance_Projects_Delete, L("Delete"));

            var projectCOAs = projectMaintenance.CreateChildPermission(AppPermissions.Pages_Projects_ProjectMaintenance_ProjectCOAs, L("ProjectCOAs"));
            projectCOAs.CreateChildPermission(AppPermissions.Pages_Projects_ProjectMaintenance_ProjectCOAs_Create, L("Create"));
            projectCOAs.CreateChildPermission(AppPermissions.Pages_Projects_ProjectMaintenance_ProjectCOAs_Edit, L("Edit"));
            projectCOAs.CreateChildPermission(AppPermissions.Pages_Projects_ProjectMaintenance_ProjectCOAs_Delete, L("Delete"));

            var contracts = projectMaintenance.CreateChildPermission(AppPermissions.Pages_Projects_ProjectMaintenance_Contracts, L("Contracts"));
            contracts.CreateChildPermission(AppPermissions.Pages_Projects_ProjectMaintenance_Contracts_Create, L("Create"));
            contracts.CreateChildPermission(AppPermissions.Pages_Projects_ProjectMaintenance_Contracts_Edit, L("Edit"));
            contracts.CreateChildPermission(AppPermissions.Pages_Projects_ProjectMaintenance_Contracts_Delete, L("Delete"));

            var projectInquiry = projects.CreateChildPermission(AppPermissions.Pages_Projects_Inquiry, L("Inquiry"));
            #endregion

            #region Receivables Tab
            var receivables = pages.CreateChildPermission(AppPermissions.Pages_Receivables, L("Receivables"));
            var customers = receivables.CreateChildPermission(AppPermissions.Pages_Receivables_Customers, L("Customers"));
            var history = customers.CreateChildPermission(AppPermissions.Pages_Receivables_Customers_History, L("History"));
            var invoices = receivables.CreateChildPermission(AppPermissions.Pages_Receivables_Invoices, L("Invoices"));
            var invoicesEntry = invoices.CreateChildPermission(AppPermissions.Pages_Receivables_Invoices_Entry, L("Entry"));
            invoicesEntry.CreateChildPermission(AppPermissions.Pages_Receivables_Invoices_Entry_Create, L("Create"));
            invoicesEntry.CreateChildPermission(AppPermissions.Pages_Receivables_Invoices_Entry_Edit, L("Edit"));
            invoicesEntry.CreateChildPermission(AppPermissions.Pages_Receivables_Invoices_Entry_Delete, L("Delete"));
            var receivablesInquiry = receivables.CreateChildPermission(AppPermissions.Pages_Receivables_Inquiry, L("Inquiry"));
            var aRInvoiceInquiry = receivablesInquiry.CreateChildPermission(AppPermissions.Pages_Receivables_Inquiry_ARInvoiceInquiry, L("ARInvoiceInquiry"));
            var customerSummary = receivablesInquiry.CreateChildPermission(AppPermissions.Pages_Receivables_Inquiry_CustomerSummary, L("CustomerSummary"));
            var invoiceDetail = receivablesInquiry.CreateChildPermission(AppPermissions.Pages_Receivables_Inquiry_InvoiceDetail, L("InvoiceDetail"));
            var receivablesPreferences = receivables.CreateChildPermission(AppPermissions.Pages_Receivables_Preferences, L("Preferences"));

            var billingTypes = receivables.CreateChildPermission(AppPermissions.Pages_Receivables_Preferences_BillingTypes, L("BillingTypes"));
            billingTypes.CreateChildPermission(AppPermissions.Pages_Receivables_Preferences_BillingTypes_Create, L("Create"));
            billingTypes.CreateChildPermission(AppPermissions.Pages_Receivables_Preferences_BillingTypes_Edit, L("Edit"));
            billingTypes.CreateChildPermission(AppPermissions.Pages_Receivables_Preferences_BillingTypes_Delete, L("Delete"));

            var territories = receivables.CreateChildPermission(AppPermissions.Pages_Receivables_Preferences_Territories, L("Territories"));
            territories.CreateChildPermission(AppPermissions.Pages_Receivables_Preferences_Territories_Create, L("Create"));
            territories.CreateChildPermission(AppPermissions.Pages_Receivables_Preferences_Territories_Edit, L("Edit"));
            territories.CreateChildPermission(AppPermissions.Pages_Receivables_Preferences_Territories_Delete, L("Delete"));

            var paymentTerms = receivables.CreateChildPermission(AppPermissions.Pages_Receivables_Preferences_PaymentTerms, L("PaymentTerms"));
            paymentTerms.CreateChildPermission(AppPermissions.Pages_Receivables_Preferences_PaymentTerms_Create, L("Create"));
            paymentTerms.CreateChildPermission(AppPermissions.Pages_Receivables_Preferences_PaymentTerms_Edit, L("Edit"));
            paymentTerms.CreateChildPermission(AppPermissions.Pages_Receivables_Preferences_PaymentTerms_Delete, L("Delete"));

            var aRInvoiceTemplate = receivables.CreateChildPermission(AppPermissions.Pages_Receivables_Preferences_ARInvoiceTemplate, L("ARInvoiceTemplate"));
            aRInvoiceTemplate.CreateChildPermission(AppPermissions.Pages_Receivables_Preferences_ARInvoiceTemplate_Create, L("Create"));
            aRInvoiceTemplate.CreateChildPermission(AppPermissions.Pages_Receivables_Preferences_ARInvoiceTemplate_Edit, L("Edit"));
            aRInvoiceTemplate.CreateChildPermission(AppPermissions.Pages_Receivables_Preferences_ARInvoiceTemplate_Delete, L("Delete"));

            #endregion
            #region Payables Tab
            var payables = pages.CreateChildPermission(AppPermissions.Pages_Payables, L("Payables"));
            var vendors = payables.CreateChildPermission(AppPermissions.Pages_Payables_Vendors, L("Vendors"));
            vendors.CreateChildPermission(AppPermissions.Pages_Payables_Vendors_Create, L("Create"));
            vendors.CreateChildPermission(AppPermissions.Pages_Payables_Vendors_Edit, L("Edit"));
            vendors.CreateChildPermission(AppPermissions.Pages_Payables_Vendors_Delete, L("Delete"));

            var payablesInvoices = payables.CreateChildPermission(AppPermissions.Pages_Payables_Invoices, L("Invoices"));
            var batch = payablesInvoices.CreateChildPermission(AppPermissions.Pages_Payables_Invoices_Batch, L("Batch"));
            batch.CreateChildPermission(AppPermissions.Pages_Payables_Invoices_Batch_Create, L("Create"));
            batch.CreateChildPermission(AppPermissions.Pages_Payables_Invoices_Batch_Edit, L("Edit"));
            batch.CreateChildPermission(AppPermissions.Pages_Payables_Invoices_Batch_Delete, L("Delete"));



            var payablesInquiry = payables.CreateChildPermission(AppPermissions.Pages_Payables_Inquiry, L("Inquiry"));
            var aPInvoiceInquiry = payablesInquiry.CreateChildPermission(AppPermissions.Pages_Payables_Inquiry_APInvoiceInquiry, L("APInvoiceInquiry"));
            var paymentHistory = payablesInquiry.CreateChildPermission(AppPermissions.Pages_Payables_Inquiry_PaymentHistory, L("PaymentHistory"));
            var vendorSummary = payablesInquiry.CreateChildPermission(AppPermissions.Pages_Payables_Inquiry_VendorSummary, L("VendorSummary"));
            var payablesInvoiceDetail = payablesInquiry.CreateChildPermission(AppPermissions.Pages_Payables_Inquiry_InvoiceDetail, L("InvoiceDetail"));

            var payablesPreferences = payables.CreateChildPermission(AppPermissions.Pages_Payables_Preferences, L("Preferences"));
            var payables1099T4Codes = payablesPreferences.CreateChildPermission(AppPermissions.Pages_Payables_Preferences_1099T4Codes, L("1099T4Codes"));

            var vendorPaymentTerms = payablesPreferences.CreateChildPermission(AppPermissions.Pages_Payables_Preferences_VendorPaymentTerms, L("VendorPaymentTerms"));
            vendorPaymentTerms.CreateChildPermission(AppPermissions.Pages_Payables_Preferences_VendorPaymentTerms_Create, L("Create"));
            vendorPaymentTerms.CreateChildPermission(AppPermissions.Pages_Payables_Preferences_VendorPaymentTerms_Edit, L("Edit"));
            vendorPaymentTerms.CreateChildPermission(AppPermissions.Pages_Payables_Preferences_VendorPaymentTerms_Delete, L("Delete"));

            var yEProcesses = payables.CreateChildPermission(AppPermissions.Pages_Payables_YEProcesses, L("YEProcesses"));
            var yEProcessesVendor1099s = yEProcesses.CreateChildPermission(AppPermissions.Pages_Payables_YEProcesses_1099s, L("1099s"));


            #endregion

            #region  Purchase Orders Tab
            var purchaseOrders = pages.CreateChildPermission(AppPermissions.Pages_PurchaseOrders, L("PurchaseOrders"));
            var purchaseOrdersEntry = purchaseOrders.CreateChildPermission(AppPermissions.Pages_PurchaseOrders_Entry, L("Entry"));

            #endregion

            #region Purchasing Tab
            var purchasing = pages.CreateChildPermission(AppPermissions.Pages_Purchasing, L("Purchasing"));
            var purchasingInquiry = purchasing.CreateChildPermission(AppPermissions.Pages_Purchasing_Inquiry, L("Inquiry"));
            var purchaseOrderHistory = purchasingInquiry.CreateChildPermission(AppPermissions.Pages_Purchasing_Inquiry_PurchaseOrderHistory, L("PurchaseOrderHistory"));
            var searchPurchaseOrders = purchasingInquiry.CreateChildPermission(AppPermissions.Pages_Purchasing_Inquiry_SearchPurchaseOrders, L("SearchPurchaseOrders"));
            #endregion

            #region Petty Cash Tab
            var pettyCash = pages.CreateChildPermission(AppPermissions.Pages_PettyCash, L("PettyCash"));
            var pCVendors = pettyCash.CreateChildPermission(AppPermissions.Pages_PettyCash_PCVendors, L("PCVendors"));
            var pettyCashEntry = pettyCash.CreateChildPermission(AppPermissions.Pages_PettyCash_Entry, L("Entry"));
            var pettyCashInquiry = pettyCash.CreateChildPermission(AppPermissions.Pages_PettyCash_Inquiry, L("Inquiry"));
            #endregion

            #region Credit Card Tab
            var creditCard = pages.CreateChildPermission(AppPermissions.Pages_CreditCard, L("CreditCard"));
            var creditCardEntry = creditCard.CreateChildPermission(AppPermissions.Pages_CreditCard_Entry, L("Entry"));
            var creditCardInquiry = creditCard.CreateChildPermission(AppPermissions.Pages_CreditCard_Inquiry, L("Inquiry"));
            var creditCardPreferences = creditCard.CreateChildPermission(AppPermissions.Pages_CreditCard_Preferences, L("Preferences"));
            #endregion

            #region Payroll Tab
            var payroll = pages.CreateChildPermission(AppPermissions.Pages_Payroll, L("Payroll"));
            var payrollEntry = payroll.CreateChildPermission(AppPermissions.Pages_Payroll_Entry, L("Entry"));
            var payrollInquiry = payroll.CreateChildPermission(AppPermissions.Pages_Payroll_Inquiry, L("Inquiry"));
            var payrollPreferences = payroll.CreateChildPermission(AppPermissions.Pages_Payroll_Preferences, L("Preferences"));
            #endregion

            #region Posting Tab
            var posting = pages.CreateChildPermission(AppPermissions.Pages_Posting, L("Posting"));
            #endregion


    }

    private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CORPACCOUNTINGConsts.LocalizationSourceName);
        }
    }
}
