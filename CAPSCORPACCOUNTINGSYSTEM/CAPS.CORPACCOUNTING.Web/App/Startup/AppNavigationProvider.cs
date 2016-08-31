using System.Collections.Generic;
using Abp.Application.Navigation;
using Abp.Localization;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Web.Navigation;

namespace CAPS.CORPACCOUNTING.Web.App.Startup
{
    /// <summary>
    ///     This class defines menus for the application.
    ///     It uses ABP's menu system.
    ///     When you add menu items here, they are automatically appear in angular application.
    ///     See .cshtml and .js files under App/Main/views/layout/header to know how to render menu.
    /// </summary>
    public class AppNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
            .AddItem(new MenuItemDefinition(
            PageNames.App.Host.Tenants,
            L("Tenants"),
            url: "host.tenants",
            icon: "icon-globe",
            requiredPermissionName: AppPermissions.Pages_Tenants
            ))
            .AddItem(new MenuItemDefinition(
            PageNames.App.Host.Editions,
            L("Editions"),
            url: "host.editions",
            icon: "icon-grid",
            requiredPermissionName: AppPermissions.Pages_Editions
            ))
            .AddItem(new MenuItemDefinition(
            PageNames.App.Tenant.Dashboard,
            L("Dashboard"),
            url: "tenant.dashboard",
            icon: "icon-home",
            requiredPermissionName: AppPermissions.Pages_Tenant_Dashboard
            ))
            .AddItem(new MenuItemDefinition(
            PageNames.App.Common.Administration,
            L("Administration"), "icon-wrench"
            ).AddItem(new MenuItemDefinition(
            PageNames.App.Common.OrganizationUnits,
            L("TenantGroups"),
            url: "organizationUnits",
            icon: "icon-layers",
            requiredPermissionName: AppPermissions.Pages_Administration_OrganizationUnits
            )
            )
            .AddItem(new MenuItemDefinition(
            PageNames.App.Common.Roles,
            L("Roles"),
            url: "roles",
            icon: "icon-briefcase",
            requiredPermissionName: AppPermissions.Pages_Administration_Roles
            )
            ).AddItem(new MenuItemDefinition(
            PageNames.App.Common.Users,
            L("Users"),
            url: "users",
            icon: "icon-users",
            requiredPermissionName: AppPermissions.Pages_Administration_Users
            )
            ).AddItem(new MenuItemDefinition(
            PageNames.App.Common.Languages,
            L("Languages"),
            url: "languages",
            icon: "icon-flag",
            requiredPermissionName: AppPermissions.Pages_Administration_Languages
            )
            ).AddItem(new MenuItemDefinition(
            PageNames.App.Common.AuditLogs,
            L("AuditLogs"),
            url: "auditLogs",
            icon: "icon-lock",
            requiredPermissionName: AppPermissions.Pages_Administration_AuditLogs
            )
            ).AddItem(new MenuItemDefinition(
            PageNames.App.Host.Maintenance,
            L("Maintenance"),
            url: "host.maintenance",
            icon: "icon-wrench",
            requiredPermissionName: AppPermissions.Pages_Administration_Host_Maintenance
            )
            )
            .AddItem(new MenuItemDefinition(
            PageNames.App.Host.Settings,
            L("Settings"),
            url: "host.settings",
            icon: "icon-settings",
            requiredPermissionName: AppPermissions.Pages_Administration_Host_Settings
            )
            )
            .AddItem(new MenuItemDefinition(
            PageNames.App.Tenant.Settings,
            L("Settings"),
            url: "companysetup",
            icon: "icon-layers",
            requiredPermissionName: AppPermissions.Pages_Administration_Tenant_Settings
            )
            ))
        .AddItem(FinancialsTabs())
        .AddItem(ProjectsTabs())
        .AddItem(ReceivablesTabs())
        .AddItem(PayablesTabs())
        .AddItem(PurchaseOrdersTabs())
        .AddItem(PurchasingTabs())
        .AddItem(PettyCashTabs())
        .AddItem(CreditCardTabs())
        .AddItem(PayrollTabs())
        .AddItem(BatchPostingTabs())
        .AddItem(BankingTabs());
        }

        #region Tabs

        private MenuItemDefinition ProjectsTabs()
        {
            return new MenuItemDefinition(
            PageNames.App.Organization.Projects,
            L("Projects"), "icon-wrench",
            requiredPermissionName: AppPermissions.Pages_Projects
            )
            .AddItem(new MenuItemDefinition(
            PageNames.App.Organization.ProjectsProjectMaintenance,
            L("Maintenance"), "icon-wrench", "projects.projectmaintenance",
            requiredPermissionName: AppPermissions.Pages_Projects_ProjectMaintenance,
            customData: MenuItemsList("Projects", "ProjectMaintenance")
            ))
            .AddItem(new MenuItemDefinition(
            PageNames.App.Organization.ProjectsInquiry,
            L("Inquiry"), "icon-wrench", "projects.inquiry",
            requiredPermissionName: AppPermissions.Pages_Projects_Inquiry,
            customData: MenuItemsList("Projects", "Inquiry")
            ));
        }

        private MenuItemDefinition FinancialsTabs()
        {
            return new MenuItemDefinition(
            PageNames.App.Organization.Financials,
            L("Financials"), "icon-wrench",
            requiredPermissionName: AppPermissions.Pages_Financials
            )
            .AddItem(new MenuItemDefinition(
            PageNames.App.Organization.FinancialsAccounts,
            L("Accounts"), "icon-wrench", "financials.accounts",
            requiredPermissionName: AppPermissions.Pages_Financials_Accounts,
            customData: MenuItemsList("Financials", "Accounts")
            ))
            .AddItem(new MenuItemDefinition(
            PageNames.App.Organization.FinancialsJournals,
            L("Journals"), "icon-wrench", "financials.journals",
            requiredPermissionName: AppPermissions.Pages_Financials_Journals,
            customData: MenuItemsList("Financials", "Journals")
            ))
            .AddItem(new MenuItemDefinition(
            PageNames.App.Organization.FinancialsInquiry,
            L("Inquiry"), "icon-wrench", "financials.inquiry",
            requiredPermissionName: AppPermissions.Pages_Financials_Inquiry,
            customData: MenuItemsList("Financials", "Inquiry")
            ))
            .AddItem(new MenuItemDefinition(
            PageNames.App.Organization.FinancialsFiscalPeriod,
            L("FiscalPeriod"), "icon-wrench", "financials.fiscalperiod",
            requiredPermissionName: AppPermissions.Pages_Financials_FiscalPeriod,
            customData: MenuItemsList("Financials", "FiscalPeriod")
            ));
        }

        private MenuItemDefinition ReceivablesTabs()
        {
            return new MenuItemDefinition(
            PageNames.App.Organization.Receivables,
            L("Receivables"), "icon-wrench",
            requiredPermissionName: AppPermissions.Pages_Receivables
            )
            .AddItem(new MenuItemDefinition(
            PageNames.App.Organization.ReceivablesCustomers,
            L("Customers"), "icon-wrench", "receivables.customers",
            requiredPermissionName: AppPermissions.Pages_Receivables_Customers,
            customData: MenuItemsList("Receivables", "Customers")
            ))
            .AddItem(new MenuItemDefinition(
            PageNames.App.Organization.ReceivablesInvoices,
            L("Invoices"), "icon-wrench", "receivables.invoices",
            requiredPermissionName: AppPermissions.Pages_Receivables_Invoices,
            customData: MenuItemsList("Receivables", "Invoices")
            ))
            .AddItem(new MenuItemDefinition(
            PageNames.App.Organization.ReceivablesInquiry,
            L("Inquiry"), "icon-wrench", "receivables.inquiry",
            requiredPermissionName: AppPermissions.Pages_Receivables_Inquiry,
            customData: MenuItemsList("Receivables", "Inquiry")
            ))
            .AddItem(new MenuItemDefinition(
            PageNames.App.Organization.ReceivablesPreferences,
            L("Preferences"), "icon-wrench", "receivables.preferences",
            requiredPermissionName: AppPermissions.Pages_Receivables_Preferences,
            customData: MenuItemsList("Receivables", "Preferences")
            ));
        }

        private MenuItemDefinition PayablesTabs()
        {
            return new MenuItemDefinition(
            PageNames.App.Organization.Payables,
            L("Payables"), "icon-wrench",
            requiredPermissionName: AppPermissions.Pages_Payables
            )
            .AddItem(new MenuItemDefinition(
            PageNames.App.Organization.PayablesVendors,
            L("Vendors"), "icon-wrench", "payables.vendors",
            requiredPermissionName: AppPermissions.Pages_Payables_Vendors,
            customData: MenuItemsList("Payables", "Vendors")
            ))
            .AddItem(new MenuItemDefinition(
            PageNames.App.Organization.PayablesInvoices,
            L("Invoices"), "icon-wrench", "payables.invoices",
            requiredPermissionName: AppPermissions.Pages_Payables_Invoices,
            customData: MenuItemsList("Payables", "Invoices")
            ))
            .AddItem(new MenuItemDefinition(
            PageNames.App.Organization.PayablesInquiry,
            L("Inquiry"), "icon-wrench", "payables.inquiry",
            requiredPermissionName: AppPermissions.Pages_Payables_Inquiry,
            customData: MenuItemsList("Payables", "Inquiry")
            ))
            .AddItem(new MenuItemDefinition(
            PageNames.App.Organization.PayablesPreferences,
            L("Preferences"), "icon-wrench", "payables.preferences",
            requiredPermissionName: AppPermissions.Pages_Payables_Preferences,
            customData: MenuItemsList("Payables", "Preferences")
            ))
            .AddItem(new MenuItemDefinition(
            PageNames.App.Organization.PayablesYEProcesses,
            L("YEProcesses"), "icon-wrench", "payables.yeprocesses",
            requiredPermissionName: AppPermissions.Pages_Payables_YEProcesses,
            customData: MenuItemsList("Payables", "YEProcesses")
            ));
        }

        private MenuItemDefinition PurchaseOrdersTabs()
        {
            return new MenuItemDefinition(
            PageNames.App.Organization.PurchaseOrders,
            L("PurchaseOrders"), "icon-wrench",
            requiredPermissionName: AppPermissions.Pages_PurchaseOrders
            )
            .AddItem(new MenuItemDefinition(
            PageNames.App.Organization.PurchaseOrdersEntry,
            L("Entry"), "icon-wrench", "purchaseorders.entry",
            requiredPermissionName: AppPermissions.Pages_PurchaseOrders_Entry,
            customData: MenuItemsList("PurchaseOrders", "Entry")
            ));
        }

        private MenuItemDefinition PurchasingTabs()
        {
            return new MenuItemDefinition(
            PageNames.App.Organization.Purchasing,
            L("Purchasing"), "icon-wrench",
            requiredPermissionName: AppPermissions.Pages_Purchasing
            )
            .AddItem(new MenuItemDefinition(
            PageNames.App.Organization.PurchasingInquiry,
            L("Inquiry"), "icon-wrench", "purchasing.inquiry",
            requiredPermissionName: AppPermissions.Pages_Purchasing_Inquiry,
            customData: MenuItemsList("Purchasing", "Inquiry")
            ));
        }

        private MenuItemDefinition PettyCashTabs()
        {
            return new MenuItemDefinition(
                PageNames.App.Organization.PettyCash,
                L("PettyCash"), "icon-wrench",
                requiredPermissionName: AppPermissions.Pages_PettyCash
                ).AddItem(new MenuItemDefinition(
                    PageNames.App.Organization.PettyCashPCVendors,
                    L("PCVendors"), "icon-wrench", "pettycash.pcvendors",
                    requiredPermissionName: AppPermissions.Pages_PettyCash_PCVendors,
                    customData: MenuItemsList("PettyCash", "PCVendors")
                    )).AddItem(new MenuItemDefinition(
                        PageNames.App.Organization.PettyCashEntry,
                        L("Entry"), "icon-wrench", "pettycash.entry",
                        requiredPermissionName: AppPermissions.Pages_PettyCash_Entry,
                        customData: MenuItemsList("PettyCash", "Entry")
                        )).AddItem(new MenuItemDefinition(
                            PageNames.App.Organization.PettyCashInquiry,
                            L("Inquiry"), "icon-wrench", "pettycash.inquiry",
                            requiredPermissionName: AppPermissions.Pages_PettyCash_Inquiry,
                            customData: MenuItemsList("PettyCash", "Inquiry")
                            ));
        }

        private MenuItemDefinition CreditCardTabs()
        {
            return new MenuItemDefinition(
            PageNames.App.Organization.CreditCard,
            L("CreditCard"), "icon-wrench",
            requiredPermissionName: AppPermissions.Pages_CreditCard
            )
            .AddItem(new MenuItemDefinition(
            PageNames.App.Organization.CreditCardEntry,
            L("Entry"), "icon-wrench", "creditcard.entry",
            requiredPermissionName: AppPermissions.Pages_CreditCard_Entry,
            customData: MenuItemsList("CreditCard", "Entry")
            ))
            .AddItem(new MenuItemDefinition(
            PageNames.App.Organization.CreditCardInquiry,
            L("Inquiry"), "icon-wrench", "creditcard.inquiry",
            requiredPermissionName: AppPermissions.Pages_CreditCard_Inquiry,
            customData: MenuItemsList("CreditCard", "Inquiry")
            ))
            .AddItem(new MenuItemDefinition(
            PageNames.App.Organization.CreditCardPreferences,
            L("Preferences"), "icon-wrench", "creditcard.preferences",
            requiredPermissionName: AppPermissions.Pages_CreditCard_Preferences,
            customData: MenuItemsList("CreditCard", "Preferences")
            ));
        }

        private MenuItemDefinition PayrollTabs()
        {
            return new MenuItemDefinition(
                PageNames.App.Organization.Payroll,
                L("Payroll"), "icon-wrench",
                requiredPermissionName: AppPermissions.Pages_Payroll
                ).AddItem(new MenuItemDefinition(
                    PageNames.App.Organization.PayrollEntry,
                    L("Entry"), "icon-wrench", "payroll.entry",
                    requiredPermissionName: AppPermissions.Pages_Payroll_Entry,
                    customData: MenuItemsList("Payroll", "Entry")
                    )).AddItem(new MenuItemDefinition(
                        PageNames.App.Organization.PayrollInquiry,
                        L("Inquiry"), "icon-wrench", "payroll.inquiry",
                        requiredPermissionName: AppPermissions.Pages_Payroll_Inquiry,
                        customData: MenuItemsList("Payroll", "Inquiry")
                        )).AddItem(new MenuItemDefinition(
                            PageNames.App.Organization.PayablesPreferences,
                            L("Preferences"), "icon-wrench", "payroll.preferences",
                            requiredPermissionName: AppPermissions.Pages_Payroll_Preferences,
                            customData: MenuItemsList("Payroll", "Preferences")
                            ));
        }

        private MenuItemDefinition BatchPostingTabs()
        {
            return new MenuItemDefinition(
                PageNames.App.Organization.BatchPosting,
                L("BatchPosting"), "icon-wrench",
                requiredPermissionName: AppPermissions.Pages_BatchPosting)
                .AddItem(new MenuItemDefinition(
                PageNames.App.Organization.BatchPostingBatches,
                L("Batches"),
                url: "batchposting.batches",
                icon: "icon-briefcase",
                requiredPermissionName: AppPermissions.Pages_BatchPosting_Batches));
        }
        private MenuItemDefinition BankingTabs()
        {
            return new MenuItemDefinition(
                PageNames.App.Organization.Banking,
                L("Banking"), "icon-wrench",
                requiredPermissionName: AppPermissions.Pages_Banking
                )
                .AddItem(new MenuItemDefinition(
                PageNames.App.Organization.BankingReceiptsOrTransfers,
                L("ReceiptsOrTransfers"),
                url: "banking.receiptsortransfers",
                icon: "icon-briefcase",
                requiredPermissionName: AppPermissions.Pages_Banking_ReceiptsOrTransfers))
                .AddItem(new MenuItemDefinition(
                PageNames.App.Organization.BankingAch,
                L("ACH"),
                url: "banking.ach",
                icon: "icon-briefcase",
                requiredPermissionName: AppPermissions.Pages_Banking_ACH))
                .AddItem(new MenuItemDefinition(
                PageNames.App.Organization.BankingPostivePay,
                L("PostivePay"),
                url: "banking.postivepay",
                icon: "icon-briefcase",
                requiredPermissionName: AppPermissions.Pages_Banking_PostivePay))
                .AddItem(new MenuItemDefinition(
                PageNames.App.Organization.BankingReconciliation,
                L("BankReconciliation"),
                url: "banking.reconciliation",
                icon: "icon-briefcase",
                requiredPermissionName: AppPermissions.Pages_Banking_Reconciliation))
                .AddItem(new MenuItemDefinition(
                PageNames.App.Organization.BankingBankSetup,
                L("BankSetup"),
                url: "banking.banksetup",
                icon: "icon-briefcase",
                requiredPermissionName: AppPermissions.Pages_Banking_BankSetup));

        }

        public List<MenuItemDefinition> MenuItemsList(string ParentTab, string ChildTab)
        {
            List<MenuItemDefinition> itemList = null;

            switch (ParentTab)
            {
                case "Financials":
                    {
                        itemList = FinancialsSubTabs(ChildTab, null);
                    }
                    break;
                case "Projects":
                    {
                        itemList = ProjectSubTabs(ChildTab, null);
                    }
                    break;
                case "Receivables":
                    {
                        itemList = ReceivablesSubTabs(ChildTab, null);
                    }
                    break;
                case "Payables":
                    {
                        itemList = PayablesSubTabs(ChildTab, null);
                    }
                    break;
                case "PurchaseOrders":
                    {
                        itemList = PurchaseOrdersSubTabs(ChildTab, null);
                    }
                    break;
                case "Purchasing":
                    {
                        itemList = PurchasingSubTabs(ChildTab, null);
                    }
                    break;
                case "PettyCash":
                    {
                        itemList = PettyCashSubTabs(ChildTab, null);
                    }
                    break;
                case "CreditCard":
                    {
                        itemList = CreditCardSubTabs(ChildTab, null);
                    }
                    break;
                case "Payroll":
                    {
                        itemList = PayrollSubTabs(ChildTab, null);
                    }
                    break;
            }
            return itemList;
        }

        private List<MenuItemDefinition> FinancialsSubTabs(string ChildTab, List<MenuItemDefinition> itemList)
        {
            switch (ChildTab)
            {
                case "Accounts":
                    {
                        itemList = new List<MenuItemDefinition>
                    {
                        new MenuItemDefinition(
                            PageNames.App.Organization.FinancialsAccountsCoas,
                            L("ChartOfAccount"),
                            url: "financials.accounts.coa",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Financials_Accounts_ChartOfAccounts),
                        new MenuItemDefinition(
                            PageNames.App.Organization.FinancialsAccountsSubAccounts,
                            L("SubAccounts"),
                            url: "financials.accounts.subaccounts",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Financials_Accounts_SubAccounts),
                        new MenuItemDefinition(
                            PageNames.App.Organization.FinancialsAccountsDivisions,
                            L("Divisions"),
                            url: "financials.accounts.divisions",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Financials_Accounts_Divisions),
                          new MenuItemDefinition(
                            PageNames.App.Organization.FinancialsAccountsTypeofClassification,
                            L("Classifications"),
                            url: "financials.accounts.classifications",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Financials_Accounts_TypeofClassification)
                    };
                    }
                    break;

                case "Journals":
                    {
                        itemList = new List<MenuItemDefinition>
                    {
                        new MenuItemDefinition(
                            PageNames.App.Organization.FinancialsJournalsEntry,
                            L("Entry"),
                            url: "financials.journals.entry",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Financials_Journals_Entry)
                    };
                    }
                    break;
                case "Inquiry":
                    {
                        itemList = new List<MenuItemDefinition>
                    {
                        new MenuItemDefinition(
                            PageNames.App.Organization.FinancialsInquirySearchTransactions,
                            L("SearchTransactions"),
                            url: "financials.inquiry.searchtransactions",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Financials_Inquiry_SearchTransactions),
                        new MenuItemDefinition(
                            PageNames.App.Organization.FinancialsInquiryFinancials,
                            L("Financials"),
                            url: "financials.inquiry.financials",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Financials_Inquiry_Financials),
                        new MenuItemDefinition(
                            PageNames.App.Organization.FinancialsInquiryJournalHistory,
                            L("journalhistory"),
                            url: "financials.inquiry.journalhistory",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Financials_Inquiry_JournalHistory),
                        new MenuItemDefinition(
                            PageNames.App.Organization.FinancialsInquiryAssetTracking,
                            L("assettracking"),
                            url: "financials.inquiry.assettracking",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Financials_Inquiry_AssetTracking)
                    };
                    }
                    break;
                case "FiscalPeriod":
                    {
                    }
                    break;
            }
            return itemList;
        }

        private static List<MenuItemDefinition> ProjectSubTabs(string ChildTab, List<MenuItemDefinition> itemList)
        {
            switch (ChildTab)
            {
                case "ProjectMaintenance":
                    {
                        itemList = new List<MenuItemDefinition>
                    {
                        new MenuItemDefinition(
                            PageNames.App.Organization.ProjectsMaintenanceProjects,
                            L("Projects"),
                            url: "projects.projectmaintenance.projects",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Projects_ProjectMaintenance_Projects),
                        new MenuItemDefinition(
                            PageNames.App.Organization.ProjectsMaintenanceProjectCoas,
                            L("ProjectCoas"),
                            url: "projects.projectmaintenance.projectcoas",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Projects_ProjectMaintenance_ProjectCOAs),
                        new MenuItemDefinition(
                            PageNames.App.Organization.ProjectsMaintenanceContracts,
                            L("Contracts"),
                            url: "projects.projectmaintenance.contracts",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Projects_ProjectMaintenance_Contracts)
                    };
                    }
                    break;
                case "Inquiry":
                    {
                    }
                    break;
            }

            return itemList;
        }

        private List<MenuItemDefinition> ReceivablesSubTabs(string ChildTab, List<MenuItemDefinition> itemList)
        {
            switch (ChildTab)
            {
                case "Customers":
                    {
                        itemList = new List<MenuItemDefinition>
                    {
                        new MenuItemDefinition(
                            PageNames.App.Organization.ReceivablesCustomersHistory,
                            L("History"),
                            url: "receivables.customers.history",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Receivables_Customers_History)
                    };
                    }
                    break;

                case "Invoices":
                    {
                        itemList = new List<MenuItemDefinition>
                    {
                        new MenuItemDefinition(
                            PageNames.App.Organization.ReceivablesInvoicesEntry,
                            L("Entry"),
                            url: "receivables.invoices.entry",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Receivables_Invoices_Entry)
                    };
                    }
                    break;
                case "Inquiry":
                    {
                        itemList = new List<MenuItemDefinition>
                    {
                        new MenuItemDefinition(
                            PageNames.App.Organization.ReceivablesInquiryARInvoiceInquiry,
                            L("ARInvoiceInquiry"),
                            url: "receivables.inquiry.arinvoiceinquiry",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Receivables_Inquiry_ARInvoiceInquiry),
                        new MenuItemDefinition(
                            PageNames.App.Organization.ReceivablesInquiryCustomerSummary,
                            L("CustomerSummary"),
                            url: "receivables.inquiry.customersummary",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Receivables_Inquiry_CustomerSummary),
                        new MenuItemDefinition(
                            PageNames.App.Organization.ReceivablesInquiryInvoiceDetail,
                            L("Invoice Detail"),
                            url: "receivables.inquiry.invoicedetail",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Receivables_Inquiry_InvoiceDetail
                            )
                    };
                    }
                    break;
                case "Preferences":
                    {
                        itemList = new List<MenuItemDefinition>
                    {
                        new MenuItemDefinition(
                            PageNames.App.Organization.ReceivablesPreferencesBillingTypes,
                            L("BillingTypes"),
                            url: "receivables.preferences.billingtypes",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Receivables_Preferences_BillingTypes),
                        new MenuItemDefinition(
                            PageNames.App.Organization.ReceivablesPreferencesTerritories,
                            L("Territories"),
                            url: "receivables.preferences.territories",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Receivables_Preferences_Territories),
                        new MenuItemDefinition(
                            PageNames.App.Organization.ReceivablesPreferencesPaymentTerms,
                            L("PaymentTerms"),
                            url: "Receivables.Preferences.PaymentTerms",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Receivables_Preferences_PaymentTerms),
                        new MenuItemDefinition(
                            PageNames.App.Organization.ReceivablesPreferencesARInvoiceTemplate,
                            L("ARInvoiceTemplate"),
                            url: "receivables.preferences.arinvoicetemplate",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Receivables_Preferences_ARInvoiceTemplate)
                    };
                    }
                    break;
            }

            return itemList;
        }

        private List<MenuItemDefinition> PayablesSubTabs(string ChildTab, List<MenuItemDefinition> itemList)
        {
            switch (ChildTab)
            {
                case "Vendors":
                    {
                    }
                    break;

                case "Invoices":
                    {
                    }
                    break;
                case "APinvoiceInquiry":
                    {
                        itemList = new List<MenuItemDefinition>
                    {
                        new MenuItemDefinition(
                            PageNames.App.Organization.PayablesInquiryAPInvoiceInquiry,
                            L("APinvoiceInquiry"),
                            url: "payables.inquiry.apinvoiceinquiry",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Payables_Inquiry_APInvoiceInquiry),
                        new MenuItemDefinition(
                            PageNames.App.Organization.PayablesInquiryPaymentHistory,
                            L("PaymentHistory"),
                            url: "Payables.Inquiry.PaymentHistory",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Payables_Inquiry_PaymentHistory),
                        new MenuItemDefinition(
                            PageNames.App.Organization.PayablesInquiryVendorSummary,
                            L("VendorSummary"),
                            url: "payables.inquiry.vendorsummary",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Payables_Inquiry_VendorSummary),
                        new MenuItemDefinition(
                            PageNames.App.Organization.PayablesInquiryInvoiceDetail,
                            L("InvoiceDetail"),
                            url: "payables.inquiry.invoicedetail",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Payables_Inquiry_InvoiceDetail)
                    };
                    }
                    break;
                case "Preferences":
                    {
                        itemList = new List<MenuItemDefinition>
                    {
                        new MenuItemDefinition(
                            PageNames.App.Organization.PayablesPreferences1099T4Codes,
                            L("1099T4Codes"),
                            url: "payables.preferences.1099t4codes",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Payables_Preferences_1099T4Codes),
                        new MenuItemDefinition(
                            PageNames.App.Organization.PayablesPreferencesVendorPaymentTerms,
                            L("VendorPaymentTerms"),
                            url: "payables.preferences.vendorpaymentterms",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Payables_Preferences_VendorPaymentTerms)
                    };
                    }
                    break;
                case "YEProcesses":
                    {
                        itemList = new List<MenuItemDefinition>
                    {
                        new MenuItemDefinition(
                            PageNames.App.Organization.PayablesYEProcesses1099s,
                            L("1099s"),
                            url: "payables.yeprocesses.1099s",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Payables_YEProcesses_1099s)
                    };
                    }
                    break;
            }

            return itemList;
        }

        private static List<MenuItemDefinition> PurchaseOrdersSubTabs(string ChildTab, List<MenuItemDefinition> itemList)
        {
            switch (ChildTab)
            {
                case "Entry":
                    {
                    }
                    break;
            }
            return itemList;
        }

        private static List<MenuItemDefinition> PurchasingSubTabs(string ChildTab, List<MenuItemDefinition> itemList)
        {
            switch (ChildTab)
            {
                case "Inquiry":
                    {
                        itemList = new List<MenuItemDefinition>
                    {
                        new MenuItemDefinition(
                            PageNames.App.Organization.PurchasingInquiryPurchaseOrderHistory,
                            L("PurchaseOrderHistory"),
                            url: "purchasing.inquiry.purchaseorderhistory",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Purchasing_Inquiry_PurchaseOrderHistory),
                        new MenuItemDefinition(
                            PageNames.App.Organization.PurchasingInquirySearchPurchaseOrders,
                            L("SearchAllPurchaseOrders"),
                            url: "purchasing.inquiry.searchpurchaseorders",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Purchasing_Inquiry_SearchPurchaseOrders),
                        new MenuItemDefinition(
                            PageNames.App.Organization.PurchasingInquiryPOAgingGrid,
                            L("POAgingGrid"),
                            url: "purchasing.inquiry.poaginggrid",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Purchasing_Inquiry_POAgingGrid)
                    };
                    }
                    break;
            }

            return itemList;
        }

        private static List<MenuItemDefinition> PettyCashSubTabs(string ChildTab, List<MenuItemDefinition> itemList)
        {
            switch (ChildTab)
            {
                case "PCVendors":
                    {
                        itemList = new List<MenuItemDefinition>
                    {
                        new MenuItemDefinition(
                            PageNames.App.Organization.PettyCashPCVendorsHistory,
                            L("History"),
                            url: "pettycash.pcvendors.history",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_PettyCash_PCVendors_History)
                    };
                    }
                    break;
                case "Entry":
                    {
                    }
                    break;
                case "Inquiry":
                    {
                        itemList = new List<MenuItemDefinition>
                    {
                        new MenuItemDefinition(
                            PageNames.App.Organization.PettyCashInquiryPCAccountSummary,
                            L("PCAccountSummary"),
                            url: "pettycash.inquiry.pcadvancedsearch",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_PettyCash_Inquiry_PCAccountSummary),
                        new MenuItemDefinition(
                            PageNames.App.Organization.PettyCashInquiryPCAdvancedSearch,
                            L("PCAdvancedSearch"),
                            url: "pettycash.inquiry.pcadvancedsearch",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_PettyCash_Inquiry_PCAdvancedSearch)
                    };
                    }
                    break;
            }

            return itemList;
        }

        private static List<MenuItemDefinition> CreditCardSubTabs(string childTab, List<MenuItemDefinition> itemList)
        {
            switch (childTab)
            {
                case "Entry":
                    {
                        itemList = new List<MenuItemDefinition>
                    {
                        new MenuItemDefinition(
                            PageNames.App.Organization.CreditCardEntryOpenStatements,
                            L("CreditCardOpenStatement"),
                            url: "creditcard.Entry.ccOpenStatements",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_CreditCard_Entry_OpenStatements),
                        new MenuItemDefinition(
                            PageNames.App.Organization.CreditCardEntryCreditCardHistory,
                            L("CreditCardHistory"),
                            url: "creditcard.Entry.ccdhistory",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_CreditCard_Entry_CreditCardHistory),
                         new MenuItemDefinition(
                            PageNames.App.Organization.CreditCardEntryCreditCardCompanies,
                            L("CreditCardCompanies"),
                            url: "creditcard.Entry.ccdcompanies",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_CreditCard_Entry_CreditCardCompanies)

                    };
                    }
                    break;
                case "Inquiry":
                    {
                        itemList = new List<MenuItemDefinition>
                    {
                        new MenuItemDefinition(
                            PageNames.App.Organization.CreditCardInquiryCreditCardUploadInfo,
                            L("CreditCardUploadInformation"),
                            url: "creditcard.inquiry.ccduploadinfo",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_CreditCard_Inquiry_CreditCardUploadInfo)
                    };
                    }
                    break;
                case "Preferences":
                    {
                        itemList = new List<MenuItemDefinition>
                        {

                        };
                    }
                    break;
            }

            return itemList;
        }

        private static List<MenuItemDefinition> PayrollSubTabs(string ChildTab, List<MenuItemDefinition> itemList)
        {
            switch (ChildTab)
            {
                case "Entry":
                    {
                    }
                    break;
                case "Inquiry":
                    {
                        itemList = new List<MenuItemDefinition>
                    {
                        new MenuItemDefinition(
                            PageNames.App.Organization.PayrollInquiryPayrollHistory,
                            L("PayrollHistory"),
                            url: "payroll.inquiry.history",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Payroll_Inquiry_PayrollHistory),
                        new MenuItemDefinition(
                            PageNames.App.Organization.PayrollInquiryPayrollUploadInfo,
                            L("PayrollUploadInformation"),
                            url: "payroll.inquiry.uploadinfo",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Payroll_Inquiry_PayrollUploadInfo),
                        new MenuItemDefinition(
                            PageNames.App.Organization.PayrollInquiryPayrollLog,
                            L("PayrollLog"),
                            url: "payroll.inquiry.payrolllog",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Payroll_Inquiry_PayrollLog)
                    };
                    }
                    break;
                case "Preferences":
                    {
                        itemList = new List<MenuItemDefinition>
                    {
                        new MenuItemDefinition(
                            PageNames.App.Organization.PayrollPreferencesPayrollCompanies,
                            L("PayrollCompanies"),
                            url: "payroll.preferences.payrollcompanies",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Payroll_Preferences_PayrollCompanies),
                        new MenuItemDefinition(
                            PageNames.App.Organization.PayrollPreferencesPaychexControl,
                            L("PaychexControl"),
                            url: "payroll.preferences.paychexcontrol",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Payroll_Preferences_PaychexControl),
                        new MenuItemDefinition(
                            PageNames.App.Organization.PayrollPreferencesPayrollFringeReallocation,
                            L("PayrollFringeReallocation"),
                            url: "payroll.preferences.fringereallocation",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Payroll_Preferences_PayrollFringeReallocation)
                    };
                    }
                    break;
            }

            return itemList;
        }

        #endregion

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CORPACCOUNTINGConsts.LocalizationSourceName);
        }
    }
}
