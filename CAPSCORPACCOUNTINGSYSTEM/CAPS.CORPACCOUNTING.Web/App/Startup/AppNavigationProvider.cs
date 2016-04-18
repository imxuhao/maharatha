using Abp.Application.Features;
using Abp.Application.Navigation;
using Abp.Localization;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Web.Navigation;
using System.Collections.Generic;
using System.Linq;

namespace CAPS.CORPACCOUNTING.Web.App.Startup
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See .cshtml and .js files under App/Main/views/layout/header to know how to render menu.
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
                    )
                ).AddItem(new MenuItemDefinition(
                    PageNames.App.Host.Editions,
                    L("Editions"),
                    url: "host.editions",
                    icon: "icon-grid",
                    requiredPermissionName: AppPermissions.Pages_Editions
                    )
                ).AddItem(new MenuItemDefinition(
                    PageNames.App.Tenant.Dashboard,
                    L("Dashboard"),
                    url: "tenant.dashboard",
                    icon: "icon-home",
                    requiredPermissionName: AppPermissions.Pages_Tenant_Dashboard
                    )
                ).AddItem(new MenuItemDefinition(
                    PageNames.App.Common.Administration,
                    L("Administration"),
                    icon: "icon-wrench"
                    ).AddItem(new MenuItemDefinition(
                        PageNames.App.Common.OrganizationUnits,
                        L("OrganizationUnits"),
                        url: "organizationUnits",
                        icon: "icon-layers",
                        requiredPermissionName: AppPermissions.Pages_Administration_OrganizationUnits
                        )
                    ).AddItem(new MenuItemDefinition(
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
                    ).AddItem(new MenuItemDefinition(
                        PageNames.App.Tenant.Settings,
                        L("Settings"),
                        url: "tenant.settings",
                        icon: "icon-settings",
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
                    .AddItem(PostingTabs());
        }

        #region Tabs
        private MenuItemDefinition ProjectsTabs()
        {
            return new MenuItemDefinition(
                                PageNames.App.Common.Projects,
                                L("Projects"),
                                icon: "icon-wrench",
                                  requiredPermissionName: AppPermissions.Pages_Projects
                                ).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.ProjectsProjectMaintenance,
                                    L("ProjectMaintenance"),
                                    icon: "icon-wrench",
                                    url: "projects.projectmaintenance",
                                  requiredPermissionName: AppPermissions.Pages_Projects_ProjectMaintenance,
                                    customData: MenuItemsList("Projects", "ProjectMaintenance")
                                    ))
                                    .AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.ProjectsInquiry,
                                    L("Inquiry"),
                                    icon: "icon-wrench",
                                      url: "projects.inquiry",
                                       requiredPermissionName: AppPermissions.Pages_Projects_Inquiry,
                                    customData: MenuItemsList("Projects", "Inquiry")
                                    ));
        }

        private MenuItemDefinition FinancialsTabs()
        {
            return new MenuItemDefinition(
                                PageNames.App.Common.Financials,
                                L("Financials"),
                                icon: "icon-wrench",
                                  requiredPermissionName: AppPermissions.Pages_Financials
                                ).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.FinancialsAccounts,
                                    L("Accounts"),
                                    icon: "icon-wrench",
                                      url: "financials.accounts",
                                  requiredPermissionName: AppPermissions.Pages_Financials_Accounts,
                                    customData: MenuItemsList("Financials", "Accounts")
                                    )).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.FinancialsJournals,
                                    L("Journals"),
                                    icon: "icon-wrench",
                                         url: "financials.journals",
                                  requiredPermissionName: AppPermissions.Pages_Financials_Journals,
                                    customData: MenuItemsList("Financials", "Journals")
                                    )).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.FinancialsBanking,
                                    L("Banking"),
                                    icon: "icon-wrench",
                                           url: "financials.banking",
                                  requiredPermissionName: AppPermissions.Pages_Financials_Banking,
                                    customData: MenuItemsList("Financials", "Banking")
                                    )).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.FinancialsInquiry,
                                    L("Inquiry"),
                                    icon: "icon-wrench",
                                            url: "financials.inquiry",
                                  requiredPermissionName: AppPermissions.Pages_Financials_Inquiry,
                                    customData: MenuItemsList("Financials", "Inquiry")
                                    )).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.FinancialsPosting,
                                    L("Posting"),
                                    icon: "icon-wrench",
                                     url: "financials.posting",
                                  requiredPermissionName: AppPermissions.Pages_Financials_Posting,
                                    customData: MenuItemsList("Financials", "Posting")
                                    )).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.FinancialsPreferences,
                                    L("Preferences"),
                                    icon: "icon-wrench",
                                       url: "financials.preferences",
                                  requiredPermissionName: AppPermissions.Pages_Financials_Preferences,
                                    customData: MenuItemsList("Financials", "Preferences")
                                    ));
        }

        private MenuItemDefinition ReceivablesTabs()
        {
            return new MenuItemDefinition(
                                PageNames.App.Common.Receivables,
                                L("Receivables"),
                                icon: "icon-wrench",
                                  requiredPermissionName: AppPermissions.Pages_Receivables
                                ).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.ReceivablesCustomers,
                                    L("Customers"),
                                    icon: "icon-wrench",
                                        url: "receivables.customers",
                                  requiredPermissionName: AppPermissions.Pages_Receivables_Customers,
                                    customData: MenuItemsList("Receivables", "Customers")
                                    )).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.ReceivablesInvoices,
                                    L("Invoices"),
                                    icon: "icon-wrench",
                                         url: "receivables.invoices",
                                  requiredPermissionName: AppPermissions.Pages_Receivables_Invoices,
                                    customData: MenuItemsList("Receivables", "Invoices")
                                    )).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.ReceivablesInquiry,
                                    L("Inquiry"),
                                    icon: "icon-wrench",
                                    url: "receivables.inquiry",
                                    requiredPermissionName: AppPermissions.Pages_Receivables_Inquiry,
                                    customData: MenuItemsList("Receivables", "Inquiry")
                                    )).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.ReceivablesPreferences,
                                    L("Preferences"),
                                    icon: "icon-wrench",
                                     url: "receivables.preferences",
                                    requiredPermissionName: AppPermissions.Pages_Receivables_Preferences,
                                    customData: MenuItemsList("Receivables", "Preferences")
                                    ));
        }

        private MenuItemDefinition PayablesTabs()
        {
            return new MenuItemDefinition(
                                PageNames.App.Common.Payables,
                                L("Payables"),
                                icon: "icon-wrench",
                                  requiredPermissionName: AppPermissions.Pages_Payables
                                ).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.PayablesVendors,
                                    L("Vendors"),
                                    icon: "icon-wrench",
                                    url: "payables.vendors",
                                    requiredPermissionName: AppPermissions.Pages_Payables_Vendors,
                                    customData: MenuItemsList("Payables", "Vendors")
                                    )).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.PayablesInvoices,
                                    L("Invoices"),
                                    icon: "icon-wrench",
                                     url: "payables.invoices",
                                    requiredPermissionName: AppPermissions.Pages_Payables_Invoices,
                                    customData: MenuItemsList("Payables", "Invoices")
                                    )).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.PayablesInquiry,
                                    L("Inquiry"),
                                    icon: "icon-wrench",
                                     url: "payables.inquiry",
                                    requiredPermissionName: AppPermissions.Pages_Payables_Inquiry,
                                    customData: MenuItemsList("Payables", "Inquiry")
                                    )).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.PayablesPreferences,
                                    L("Preferences"),
                                    icon: "icon-wrench",
                                      url: "payables.preferences",
                                    requiredPermissionName: AppPermissions.Pages_Payables_Preferences,
                                    customData: MenuItemsList("Payables", "Preferences")
                                    )).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.PayablesYEProcesses,
                                    L("YEProcesses"),
                                    icon: "icon-wrench",
                                      url: "payables.yeprocesses",
                                    requiredPermissionName: AppPermissions.Pages_Payables_YEProcesses,
                                    customData: MenuItemsList("Payables", "YEProcesses")
                                    ));
        }

        private MenuItemDefinition PurchaseOrdersTabs()
        {
            return new MenuItemDefinition(
                                PageNames.App.Common.PurchaseOrders,
                                L("PurchaseOrders"),
                                icon: "icon-wrench",
                                   requiredPermissionName: AppPermissions.Pages_PurchaseOrders
                                ).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.PurchaseOrdersEntry,
                                    L("Entry"),
                                    icon: "icon-wrench",
                                    url: "purchaseorders.entry",
                                   requiredPermissionName: AppPermissions.Pages_PurchaseOrders_Entry,
                                    customData: MenuItemsList("PurchaseOrders", "Entry")
                                    ));
        }

        private MenuItemDefinition PurchasingTabs()
        {
            return new MenuItemDefinition(
                                PageNames.App.Common.Purchasing,
                                L("Purchasing"),
                                icon: "icon-wrench",
                                 requiredPermissionName: AppPermissions.Pages_Purchasing
                                ).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.PurchasingInquiry,
                                    L("Inquiry"),
                                    icon: "icon-wrench",
                                     url: "purchasing.inquiry",
                                   requiredPermissionName: AppPermissions.Pages_Purchasing_Inquiry,
                                    customData: MenuItemsList("Purchasing", "Inquiry")
                                    ));
        }

        private MenuItemDefinition PettyCashTabs()
        {
            return new MenuItemDefinition(
                                PageNames.App.Common.PettyCash,
                                L("PettyCash"),
                                icon: "icon-wrench",
                                requiredPermissionName: AppPermissions.Pages_PettyCash
                                ).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.PettyCashPCVendors,
                                    L("PCVendors"),
                                    icon: "icon-wrench",
                                    url: "pettycash.pcvendors",
                                   requiredPermissionName: AppPermissions.Pages_PettyCash_PCVendors,
                                    customData: MenuItemsList("PettyCash", "PCVendors")
                                    )).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.PettyCashEntry,
                                    L("Entry"),
                                    icon: "icon-wrench",
                                       url: "pettycash.entry",
                                    requiredPermissionName: AppPermissions.Pages_PettyCash_Entry,
                                    customData: MenuItemsList("PettyCash", "Entry")
                                    )).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.PettyCashInquiry,
                                    L("Inquiry"),
                                    icon: "icon-wrench",
                                        url: "pettycash.inquiry",
                                    requiredPermissionName: AppPermissions.Pages_PettyCash_Inquiry,
                                    customData: MenuItemsList("PettyCash", "Inquiry")
                                    ));
        }

        private MenuItemDefinition CreditCardTabs()
        {
            return new MenuItemDefinition(
                                PageNames.App.Common.CreditCard,
                                L("CreditCard"),
                                icon: "icon-wrench",
                                requiredPermissionName: AppPermissions.Pages_CreditCard
                                ).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.CreditCardEntry,
                                    L("Entry"),
                                    icon: "icon-wrench",
                                       url: "creditcard.entry",
                                 requiredPermissionName: AppPermissions.Pages_CreditCard_Entry,
                                    customData: MenuItemsList("CreditCard", "Entry")
                                    )).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.CreditCardInquiry,
                                    L("Inquiry"),
                                    icon: "icon-wrench",
                                    url: "creditcard.inquiry",
                                 requiredPermissionName: AppPermissions.Pages_CreditCard_Inquiry,
                                    customData: MenuItemsList("CreditCard", "Inquiry")
                                    )).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.CreditCardPreferences,
                                    L("Preferences"),
                                    icon: "icon-wrench",
                                      url: "creditcard.preferences",
                                 requiredPermissionName: AppPermissions.Pages_CreditCard_Preferences,
                                    customData: MenuItemsList("CreditCard", "Preferences")
                                    ));
        }

        private MenuItemDefinition PayrollTabs()
        {
            return new MenuItemDefinition(
                                PageNames.App.Common.Payroll,
                                L("Payroll"),
                                icon: "icon-wrench",
                                 requiredPermissionName: AppPermissions.Pages_Payroll
                                ).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.PayrollEntry,
                                    L("Entry"),
                                    icon: "icon-wrench",
                                      url: "payroll.entry",
                                 requiredPermissionName: AppPermissions.Pages_Payroll_Entry,
                                    customData: MenuItemsList("Payroll", "Entry")
                                    )).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.PayrollInquiry,
                                    L("Inquiry"),
                                    icon: "icon-wrench",
                                         url: "payroll.inquiry",
                                 requiredPermissionName: AppPermissions.Pages_Payroll_Inquiry,
                                    customData: MenuItemsList("Payroll", "Inquiry")
                                    )).AddItem(new MenuItemDefinition(
                                    PageNames.App.Common.PayablesPreferences,
                                    L("Preferences"),
                                    icon: "icon-wrench",
                                         url: "payroll.preferences",
                                 requiredPermissionName: AppPermissions.Pages_Payroll_Preferences,
                                    customData: MenuItemsList("Payroll", "Preferences")
                                    ));
        }

        private MenuItemDefinition PostingTabs()
        {
            return new MenuItemDefinition(
                                PageNames.App.Common.Posting,
                                L("Posting"),
                                icon: "icon-wrench",
                                url:"posting",
                               requiredPermissionName: AppPermissions.Pages_Posting
                                );
        }

        public List<MenuItemDefinition> MenuItemsList(string ParentTab, string ChildTab)
        {
            List<MenuItemDefinition> itemList = null;

            switch (ParentTab)
            {
                case "Financials":
                    {
                        itemList = FinancialsSubTabs(ChildTab, itemList);
                    }
                    break;
                case "Projects":
                    {
                        itemList = ProjectSubTabs(ChildTab, itemList);
                    }
                    break;
                case "Receivables":
                    {
                        itemList = ReceivablesSubTabs(ChildTab, itemList);
                    }
                    break;
                case "Payables":
                    {
                        itemList = PayablesSubTabs(ChildTab, itemList);
                    }
                    break;
                case "PurchaseOrders":
                    {
                        itemList = PurchaseOrdersSubTabs(ChildTab, itemList);
                    }
                    break;
                case "Purchasing":
                    {
                        itemList = PurchasingSubTabs(ChildTab, itemList);
                    }
                    break;
                case "PettyCash":
                    {
                        itemList = PettyCashSubTabs(ChildTab, itemList);
                    }
                    break;
                case "CreditCard":
                    {
                        itemList = CreditCardSubTabs(ChildTab, itemList);
                    }
                    break;
                case "Payroll":
                    {
                        itemList = PayrollSubTabs(ChildTab, itemList);
                    }
                    break;
                case "Posting":
                    {
                        itemList = PostingSubTabs(ChildTab, itemList);
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
                        itemList = new List<MenuItemDefinition>() { {new MenuItemDefinition(
                                     PageNames.App.Common.FinancialsAccountsCoas,
                                     L("ChartOfAccount"),
                                     url: "financials.accounts.coa",
                                     icon: "icon-briefcase",
                                     requiredPermissionName: AppPermissions.Pages_Financials_Accounts_ChartOfAccounts) },
                                        { new MenuItemDefinition(
                                    PageNames.App.Common.FinancialsAccountsSubAccounts,
                                    L("SubAccounts"),
                                    url: "financials.accounts.subaccounts",
                                    icon: "icon-briefcase",
                                    requiredPermissionName: AppPermissions.Pages_Financials_Accounts_SubAccounts)},
                                        {new MenuItemDefinition(
                                    PageNames.App.Common.FinancialsAccountsDivisions,
                                    L("Divisions"),
                                    url: "financials.accounts.divisions",
                                    icon: "icon-briefcase",
                                    requiredPermissionName: AppPermissions.Pages_Financials_Accounts_Divisions) }
                                    };
                    }
                    break;

                case "Journals":
                    {
                        itemList = new List<MenuItemDefinition>() {
                                                new MenuItemDefinition(
                                                PageNames.App.Common.FinancialsJournalsEntry,
                                                L("Entry"),
                                                url: "financials.journals.entry",
                                                icon: "icon-briefcase",
                                                requiredPermissionName: AppPermissions.Pages_Financials_Journals_Entry)};
                    }
                    break;
                case "Banking":
                    {
                        itemList = new List<MenuItemDefinition>() { {new MenuItemDefinition(
                                                PageNames.App.Common.FinancialsBankingReceiptsOrTransfers,
                                                L("ReceiptsOrTransfers"),
                                                url: "financials.banking.receiptsortransfers",
                                                icon: "icon-briefcase",
                                                requiredPermissionName: AppPermissions.Pages_Financials_Banking_ReceiptsOrTransfers) },
                                                                             {new MenuItemDefinition(
                                                PageNames.App.Common.FinancialsBankingAch,
                                                L("ACH"),
                                                url: "financials.banking.ach",
                                                icon: "icon-briefcase",
                                                requiredPermissionName: AppPermissions.Pages_Financials_Banking_ACH) },
                                                                            {new MenuItemDefinition(
                                                PageNames.App.Common.FinancialsBankingReconciliation,
                                                L("BankReconciliation"),
                                                url: "financials.banking.reconciliation",
                                                icon: "icon-briefcase",
                                                requiredPermissionName: AppPermissions.Pages_Financials_Banking_Reconciliation
                                                ) }
                                    };
                    }
                    break;
                case "Inquiry":
                    {
                        itemList = new List<MenuItemDefinition>(){ {new MenuItemDefinition(
                                             PageNames.App.Common.FinancialsInquirySearchTransactions,
                                             L("SearchTransactions"),
                                             url: "financials.inquiry.searchtransactions",
                                             icon: "icon-briefcase",
                                             requiredPermissionName: AppPermissions.Pages_Financials_Inquiry_SearchTransactions) },
                                                                             {new MenuItemDefinition(
                                             PageNames.App.Common.FinancialsInquiryFinancials,
                                             L("Financials"),
                                             url: "financials.inquiry.financials",
                                             icon: "icon-briefcase",
                                             requiredPermissionName: AppPermissions.Pages_Financials_Inquiry_Financials) },
                                                                                { new MenuItemDefinition(
                                             PageNames.App.Common.FinancialsInquiryJournalHistory,
                                             L("journalhistory"),
                                             url: "financials.inquiry.journalhistory",
                                             icon: "icon-briefcase",
                                             requiredPermissionName: AppPermissions.Pages_Financials_Inquiry_JournalHistory)},
                                                                                {new MenuItemDefinition(
                                             PageNames.App.Common.FinancialsInquiryAssetTracking,
                                             L("assettracking"),
                                             url: "financials.inquiry.assettracking",
                                             icon: "icon-briefcase",
                                             requiredPermissionName: AppPermissions.Pages_Financials_Inquiry_AssetTracking) }
                                    };

                    }
                    break;

                case "Posting":
                    {
                        itemList = new List<MenuItemDefinition>() { {new MenuItemDefinition(
                                                PageNames.App.Common.FinancialsPostingBatched,
                                                L("Batched"),
                                                url: "financials.posting.batched",
                                                icon: "icon-briefcase",
                                                requiredPermissionName: AppPermissions.Pages_Financials_Posting_Batched) },
                                                                             {new MenuItemDefinition(
                                                PageNames.App.Common.FinancialsPostingUnBatched,
                                                L("UnBatched"),
                                                url: "financials.posting.unbatched",
                                                icon: "icon-briefcase",
                                                requiredPermissionName: AppPermissions.Pages_Financials_Posting_UnBatched) }};
                    }
                    break;

                case "Preferences":
                    {
                        itemList = new List<MenuItemDefinition>() { {new MenuItemDefinition(
                                                PageNames.App.Common.FinancialsPreferencesFiscalPeriod,
                                                L("FiscalPeriod"),
                                                url: "financials.preferences.fiscalperiod",
                                                icon: "icon-briefcase",
                                                requiredPermissionName: AppPermissions.Pages_Financials_Preferences_FiscalPeriod) },
                                                                             {new MenuItemDefinition(
                                                PageNames.App.Common.FinancialsPreferencesBankSetup,
                                                L("BankSetup"),
                                                url: "financials.preferences.banksetup",
                                                icon: "icon-briefcase",
                                                requiredPermissionName: AppPermissions.Pages_Financials_Preferences_BankSetup) }};
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
                        itemList = new List<MenuItemDefinition>() { {new MenuItemDefinition(
                                     PageNames.App.Common.ProjectsMaintenanceProjects,
                                     L("Projects"),
                                     url: "projects.projectmaintenance.projects",
                                     icon: "icon-briefcase",
                                     requiredPermissionName: AppPermissions.Pages_Projects_ProjectMaintenance_Projects) },
                                        { new MenuItemDefinition(
                                    PageNames.App.Common.ProjectsMaintenanceProjectCoas,
                                    L("ProjectCoas"),
                                    url: "projects.projectmaintenance.projectcoas",
                                    icon: "icon-briefcase",
                                    requiredPermissionName: AppPermissions.Pages_Projects_ProjectMaintenance_ProjectCOAs)},
                                        {new MenuItemDefinition(
                                    PageNames.App.Common.ProjectsMaintenanceContracts,
                                    L("Contracts"),
                                    url: "projects.projectmaintenance.contracts",
                                    icon: "icon-briefcase",
                                    requiredPermissionName: AppPermissions.Pages_Projects_ProjectMaintenance_Contracts) }
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
                        itemList = new List<MenuItemDefinition>() { {new MenuItemDefinition(
                                     PageNames.App.Common.ReceivablesCustomersHistory,
                                     L("History"),
                                     url: "receivables.customers.history",
                                     icon: "icon-briefcase",
                                     requiredPermissionName: AppPermissions.Pages_Receivables_Customers_History) }
                                    };
                    }
                    break;

                case "Invoices":
                    {
                        itemList = new List<MenuItemDefinition>() {
                                                new MenuItemDefinition(
                                                PageNames.App.Common.ReceivablesInvoicesEntry,
                                                L("Entry"),
                                                url: "receivables.invoices.entry",
                                                icon: "icon-briefcase",
                                                requiredPermissionName: AppPermissions.Pages_Receivables_Invoices_Entry)};
                    }
                    break;
                case "Inquiry":
                    {
                        itemList = new List<MenuItemDefinition>() { {new MenuItemDefinition(
                                                PageNames.App.Common.ReceivablesInquiryARInvoiceInquiry,
                                                L("ARInvoiceInquiry"),
                                                url: "receivables.inquiry.arinvoiceinquiry",
                                                icon: "icon-briefcase",
                                                requiredPermissionName: AppPermissions.Pages_Receivables_Inquiry_ARInvoiceInquiry) },
                                                                             {new MenuItemDefinition(
                                                PageNames.App.Common.ReceivablesInquiryCustomerSummary,
                                                L("CustomerSummary"),
                                                url: "receivables.inquiry.customersummary",
                                                icon: "icon-briefcase",
                                                requiredPermissionName: AppPermissions.Pages_Receivables_Inquiry_CustomerSummary) },
                                                                            {new MenuItemDefinition(
                                                PageNames.App.Common.ReceivablesInquiryInvoiceDetail,
                                                L("Invoice Detail"),
                                                url: "receivables.inquiry.invoicedetail",
                                                icon: "icon-briefcase",
                                                requiredPermissionName: AppPermissions.Pages_Receivables_Inquiry_InvoiceDetail
                                                ) }
                                    };
                    }
                    break;
                case "Preferences":
                    {
                        itemList = new List<MenuItemDefinition>(){ {new MenuItemDefinition(
                                             PageNames.App.Common.ReceivablesPreferencesBillingTypes,
                                             L("BillingTypes"),
                                             url: "receivables.preferences.billingtypes",
                                             icon: "icon-briefcase",
                                             requiredPermissionName: AppPermissions.Pages_Receivables_Preferences_BillingTypes) },
                                                                             {new MenuItemDefinition(
                                             PageNames.App.Common.ReceivablesPreferencesTerritories,
                                             L("Territories"),
                                             url: "receivables.preferences.territories",
                                             icon: "icon-briefcase",
                                             requiredPermissionName: AppPermissions.Pages_Receivables_Preferences_Territories) },
                                                                                { new MenuItemDefinition(
                                             PageNames.App.Common.ReceivablesPreferencesPaymentTerms,
                                             L("PaymentTerms"),
                                             url: "Receivables.Preferences.PaymentTerms",
                                             icon: "icon-briefcase",
                                             requiredPermissionName: AppPermissions.Pages_Receivables_Preferences_PaymentTerms)},
                                                                                {new MenuItemDefinition(
                                             PageNames.App.Common.ReceivablesPreferencesARInvoiceTemplate ,
                                             L("ARInvoiceTemplate"),
                                             url: "receivables.preferences.arinvoicetemplate",
                                             icon: "icon-briefcase",
                                             requiredPermissionName: AppPermissions.Pages_Receivables_Preferences_ARInvoiceTemplate) }
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
                        itemList = new List<MenuItemDefinition>() {
                                                new MenuItemDefinition(
                                                PageNames.App.Common.PayablesInvoicesBatch,
                                                L("Batch"),
                                                url: "payables.invoices.batch",
                                                icon: "icon-briefcase",
                                                requiredPermissionName: AppPermissions.Pages_Payables_Invoices_Batch)};
                    }
                    break;
                case "APinvoiceInquiry":
                    {
                        itemList = new List<MenuItemDefinition>() { {new MenuItemDefinition(
                                                PageNames.App.Common.PayablesInquiryAPInvoiceInquiry,
                                                L("APinvoiceInquiry"),
                                                url: "payables.inquiry.apinvoiceinquiry",
                                                icon: "icon-briefcase",
                                                requiredPermissionName: AppPermissions.Pages_Payables_Inquiry_APInvoiceInquiry) },
                                                                             {new MenuItemDefinition(
                                                PageNames.App.Common.PayablesInquiryPaymentHistory,
                                                L("PaymentHistory"),
                                                url: "Payables.Inquiry.PaymentHistory",
                                                icon: "icon-briefcase",
                                                requiredPermissionName: AppPermissions.Pages_Payables_Inquiry_PaymentHistory) },
                                                                            {new MenuItemDefinition(
                                                PageNames.App.Common.PayablesInquiryVendorSummary,
                                                L("VendorSummary"),
                                                url: "payables.inquiry.vendorsummary",
                                                icon: "icon-briefcase",
                                                requiredPermissionName: AppPermissions.Pages_Payables_Inquiry_VendorSummary) },
                                                                             {new MenuItemDefinition(
                                                PageNames.App.Common.PayablesInquiryInvoiceDetail,
                                                L("InvoiceDetail"),
                                                url: "payables.inquiry.invoicedetail",
                                                icon: "icon-briefcase",
                                                requiredPermissionName: AppPermissions.Pages_Payables_Inquiry_InvoiceDetail) }
                                    };
                    }
                    break;
                case "Preferences":
                    {
                        itemList = new List<MenuItemDefinition>(){ {new MenuItemDefinition(
                                             PageNames.App.Common.PayablesPreferences1099T4Codes,
                                             L("1099T4Codes"),
                                             url: "payables.preferences.1099t4codes",
                                             icon: "icon-briefcase",
                                             requiredPermissionName: AppPermissions.Pages_Payables_Preferences_1099T4Codes) },
                                                                             {new MenuItemDefinition(
                                             PageNames.App.Common.PayablesPreferencesVendorPaymentTerms,
                                             L("VendorPaymentTerms"),
                                             url: "payables.preferences.vendorpaymentterms",
                                             icon: "icon-briefcase",
                                             requiredPermissionName: AppPermissions.Pages_Payables_Preferences_VendorPaymentTerms) }
                                    };

                    }
                    break;
                case "YEProcesses":
                    {
                        itemList = new List<MenuItemDefinition>(){ {new MenuItemDefinition(
                                             PageNames.App.Common.PayablesYEProcesses1099s,
                                             L("1099s"),
                                             url: "payables.yeprocesses.1099s",
                                             icon: "icon-briefcase",
                                             requiredPermissionName: AppPermissions.Pages_Payables_YEProcesses_1099s) }};

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
                        itemList = new List<MenuItemDefinition>() { {new MenuItemDefinition(
                                     PageNames.App.Common.PurchasingInquiryPurchaseOrderHistory,
                                     L("PurchaseOrderHistory"),
                                     url: "purchasing.inquiry.purchaseorderhistory",
                                     icon: "icon-briefcase",
                                     requiredPermissionName: AppPermissions.Pages_Purchasing_Inquiry_PurchaseOrderHistory) },
                                        { new MenuItemDefinition(
                                    PageNames.App.Common.PurchasingInquirySearchPurchaseOrders,
                                    L("SearchAllPurchaseOrders"),
                                    url: "purchasing.inquiry.searchpurchaseorders",
                                    icon: "icon-briefcase",
                                    requiredPermissionName: AppPermissions.Pages_Purchasing_Inquiry_SearchPurchaseOrders)},
                                        {new MenuItemDefinition(
                                    PageNames.App.Common.PurchasingInquiryPOAgingGrid,
                                    L("POAgingGrid"),
                                    url: "purchasing.inquiry.poaginggrid",
                                    icon: "icon-briefcase",
                                    requiredPermissionName: AppPermissions.Pages_Purchasing_Inquiry_POAgingGrid) }
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
                        itemList = new List<MenuItemDefinition>() { {new MenuItemDefinition(
                                     PageNames.App.Common.PettyCashPCVendorsHistory,
                                     L("History"),
                                     url: "pettycash.pcvendors.history",
                                     icon: "icon-briefcase",
                                     requiredPermissionName: AppPermissions.Pages_PettyCash_PCVendors_History) } };
                    }
                    break;
                case "Entry":
                    {

                    }
                    break;
                case "Inquiry":
                    {
                        itemList = new List<MenuItemDefinition>() { {new MenuItemDefinition(
                                     PageNames.App.Common.PettyCashInquiryPCAccountSummary,
                                     L("PCAccountSummary"),
                                     url: "pettycash.inquiry.pcadvancedsearch",
                                     icon: "icon-briefcase",
                                     requiredPermissionName: AppPermissions.Pages_PettyCash_Inquiry_PCAccountSummary) },
                                        { new MenuItemDefinition(
                                    PageNames.App.Common.PettyCashInquiryPCAdvancedSearch,
                                    L("PCAdvancedSearch"),
                                    url: "pettycash.inquiry.pcadvancedsearch",
                                    icon: "icon-briefcase",
                                    requiredPermissionName: AppPermissions.Pages_PettyCash_Inquiry_PCAdvancedSearch)},
                                    };
                    }
                    break;
            }

            return itemList;
        }

        private static List<MenuItemDefinition> CreditCardSubTabs(string ChildTab, List<MenuItemDefinition> itemList)
        {
            switch (ChildTab)
            {
                case "Entry":
                    {

                    }
                    break;
                case "Inquiry":
                    {
                        itemList = new List<MenuItemDefinition>() { {new MenuItemDefinition(
                                     PageNames.App.Common.CreditCardInquiryCreditCardHistory,
                                     L("CreditCardHistory"),
                                     url: "creditcard.inquiry.ccdhistory",
                                     icon: "icon-briefcase",
                                     requiredPermissionName: AppPermissions.Pages_CreditCard_Inquiry_CreditCardHistory) },
                                        { new MenuItemDefinition(
                                    PageNames.App.Common.CreditCardInquiryCreditCardUploadInfo,
                                    L("CreditCardUploadInformation"),
                                    url: "creditcard.inquiry.ccduploadinfo",
                                    icon: "icon-briefcase",
                                    requiredPermissionName: AppPermissions.Pages_CreditCard_Inquiry_CreditCardUploadInfo)},
                                    };
                    }
                    break;
                case "Preferences":
                    {
                        itemList = new List<MenuItemDefinition>() { {new MenuItemDefinition(
                                     PageNames.App.Common.CreditCardPreferencesCreditCardCompanies,
                                     L("CreditCardCompanies"),
                                     url: "creditcard.preferences.ccdcompanies",
                                     icon: "icon-briefcase",
                                     requiredPermissionName: AppPermissions.Pages_CreditCard_Preferences_CreditCardCompanies) }};
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
                        itemList = new List<MenuItemDefinition>() { {new MenuItemDefinition(
                                     PageNames.App.Common.PayrollInquiryPayrollHistory,
                                     L("PayrollHistory"),
                                     url: "payroll.inquiry.history",
                                     icon: "icon-briefcase",
                                     requiredPermissionName: AppPermissions.Pages_Payroll_Inquiry_PayrollHistory) },
                                        { new MenuItemDefinition(
                                    PageNames.App.Common.PayrollInquiryPayrollUploadInfo,
                                    L("PayrollUploadInformation"),
                                    url: "payroll.inquiry.uploadinfo",
                                    icon: "icon-briefcase",
                                    requiredPermissionName: AppPermissions.Pages_Payroll_Inquiry_PayrollUploadInfo)},
                                         { new MenuItemDefinition(
                                    PageNames.App.Common.PayrollInquiryPayrollLog,
                                    L("PayrollLog"),
                                    url: "payroll.inquiry.payrolllog",
                                    icon: "icon-briefcase",
                                    requiredPermissionName: AppPermissions.Pages_Payroll_Inquiry_PayrollLog)},
                                    };
                    }
                    break;
                case "Preferences":
                    {
                        itemList = new List<MenuItemDefinition>() { {new MenuItemDefinition(
                                     PageNames.App.Common.PayrollPreferencesPayrollCompanies,
                                     L("PayrollCompanies"),
                                     url: "payroll.preferences.payrollcompanies",
                                     icon: "icon-briefcase",
                                     requiredPermissionName: AppPermissions.Pages_Payroll_Preferences_PayrollCompanies) },
                                        { new MenuItemDefinition(
                                    PageNames.App.Common.PayrollPreferencesPaychexControl,
                                    L("PaychexControl"),
                                    url: "payroll.preferences.paychexcontrol",
                                    icon: "icon-briefcase",
                                    requiredPermissionName: AppPermissions.Pages_Payroll_Preferences_PaychexControl)},
                                         { new MenuItemDefinition(
                                    PageNames.App.Common.PayrollPreferencesPayrollFringeReallocation,
                                    L("PayrollFringeReallocation"),
                                    url: "payroll.preferences.fringereallocation",
                                    icon: "icon-briefcase",
                                    requiredPermissionName: AppPermissions.Pages_Payroll_Preferences_PayrollFringeReallocation)},
                                    };
                    }
                    break;
            }

            return itemList;
        }

        private static List<MenuItemDefinition> PostingSubTabs(string ChildTab, List<MenuItemDefinition> itemList)
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
        #endregion

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CORPACCOUNTINGConsts.LocalizationSourceName);
        }
    }
}
