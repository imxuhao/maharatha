using System.Linq;
using CAPS.CORPACCOUNTING.EntityFramework;
using CAPS.CORPACCOUNTING.Masters;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Migrations.Seed
{
    public class DefaultGridListCreator
    {
        public static List<GridListUnit> InitialGridList { get; private set; }

        private readonly CORPACCOUNTINGDbContext _context;

        static DefaultGridListCreator()
        {
            InitialGridList = new List<GridListUnit>
            {
                new GridListUnit(gridid:1,name:"Tenants",description:@"Chaching\classic\src\view\tenants\TenantsGrid",isactive:true),
                new GridListUnit(gridid:2,name:"Editions",description:@"Chaching\classic\src\view\editions\EditionsGrid",isactive:true),
                new GridListUnit(gridid:3,name:"Languages",description:@"Chaching\classic\src\view\languages\LanguagesGrid",isactive:true),
                new GridListUnit(gridid:4,name:"LanguageTexts",description:@"Chaching\classic\src\view\languages\LanguageTextsGrid",isactive:true),
                new GridListUnit(gridid:5,name:"AuditLogs",description:@"\Chaching\classic\src\view\auditlogs\AuditLogsGrid",isactive:true),
                new GridListUnit(gridid:6,name:"Roles",description:@"Chaching\classic\src\view\roles\RolesGrid\UsersGrid",isactive:true),
                new GridListUnit(gridid:7,name:"Users",description:@"Chaching\classic\src\view\users",isactive:true),
                new GridListUnit(gridid:8,name:"LinkedAccounts",description:@"Chaching\classic\src\view\profile\linkedaccounts\AccountsGrid",isactive:true),
                new GridListUnit(gridid:9,name:"ChartOfAccounts",description:@"Chaching\classic\src\view\financials\accounts\ChartOfAccountsGrid",isactive:true),
                new GridListUnit(gridid:10,name:"SubAccounts",description:@"Chaching\classic\src\view\financials\accounts\SubAccountsGrid",isactive:true),
                new GridListUnit(gridid:11,name:"FinancialAccounts",description:@"Chaching\classic\src\view\financials\accounts\AccountsGrid",isactive:true),
                new GridListUnit(gridid:12,name:"Divisions",description:@"Chaching\classic\src\view\financials\accounts\DivisionsGrid",isactive:true),
                new GridListUnit(gridid:13,name:"ProjectCoas",description:@"Chaching\classic\src\view\projects\projectmaintenance\ProjectCOAsGrid",isactive:true),
                new GridListUnit(gridid:14,name:"LineNumbers",description:@"Chaching\classic\src\view\projects\projectmaintenance\LineNumbersGrid",isactive:true),
                new GridListUnit(gridid:15,name:"Vendors",description:@"Chaching\classic\src\view\payables\vendors\VendorsGrid",isactive:true),
                new GridListUnit(gridid:16,name:"Projects",description:@"Chaching\classic\src\view\projects\projectmaintenance\ProjectsGrid",isactive:true),
                new GridListUnit(gridid:17,name:"Address",description:@"Chaching\classic\src\view\address\AddressGrid",isactive:true),
                new GridListUnit(gridid:18,name:"Journal Entry",description:@"Chaching\classic\src\view\financials\JournalEntryGrid",isactive:true),
                new GridListUnit(gridid:19,name:"Batch",description:@"Chaching\classic\src\view\payables\invoices\BatchGrid",isactive:true),
                new GridListUnit(gridid:20,name:"Fiscal Period",description:@"Chaching\classic\src\view\financials\fiscalperiod\FiscalPeriodChildGrid",isactive:true),
                new GridListUnit(gridid:21,name:"Bank Setup",description:@"Chaching\classic\src\view\financials\preferences\BankSetupGrid",isactive:true),
                new GridListUnit(gridid:22,name:"Fiscal Year",description:@"Chaching\classic\src\view\financials\fiscalperiod\FiscalPeriodGrid",isactive:true),
                new GridListUnit(gridid:23,name:"Accounts Payable",description:@"Chaching\classic\src\view\payables\invoices\AccountsPayableGrid",isactive:true),
                new GridListUnit(gridid:24,name:"Customers",description:@"Chaching\classic\src\view\receivables\customers\CustomersGrid",isactive:true),
                new GridListUnit(gridid:25,name:"Organization Units",description:@"Chaching\classic\src\view\administration\organization\CompanySetupGrid",isactive:true),
                 new GridListUnit(gridid:26,name:"Purchase Orders",description:@"Chaching\classic\src\view\purchaseorders\entry\PurchaseOrderGrid",isactive:true)
            };
        }

        public DefaultGridListCreator(CORPACCOUNTINGDbContext context)
        {
            _context = context;
        }


        public void Create()
        {
            CreateGridList();
        }

        private void CreateGridList()
        {
            foreach (var gridList in InitialGridList)
            {
                AddGridListIfNotExists(gridList);
            }
        }

        private void AddGridListIfNotExists(GridListUnit gridList)
        {
            if (_context.GridListUnit.Any(l => l.Name == gridList.Name))
            {
                return;
            }

            _context.GridListUnit.Add(gridList);

            _context.SaveChanges();
        }

    }
}
