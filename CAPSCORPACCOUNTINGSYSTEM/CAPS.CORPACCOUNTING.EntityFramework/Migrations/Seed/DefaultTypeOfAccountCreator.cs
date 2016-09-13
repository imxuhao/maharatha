using System.Linq;
using CAPS.CORPACCOUNTING.EntityFramework;
using CAPS.CORPACCOUNTING.Masters;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Accounting;


namespace CAPS.CORPACCOUNTING.Migrations.Seed
{
    public class DefaultTypeOfAccountCreator
    {
        public static List<TypeOfAccountUnit> InitialTypeOfAccountList { get; private set; }

        private readonly CORPACCOUNTINGDbContext _context;
        private int _tenantId;

        public DefaultTypeOfAccountCreator(CORPACCOUNTINGDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
            TypeOfAccountList();
        }
        public void Create()
        {
            CreateTypeOfAccountList();
        }

        private void CreateTypeOfAccountList()
        {
            foreach (var typeOfAccount in InitialTypeOfAccountList)
            {
                AddTypeOfAccountListIfNotExists(typeOfAccount);
            }
        }
        private void AddTypeOfAccountListIfNotExists(TypeOfAccountUnit typeOfAccount)
        {
            if (_context.TypeOfAccountUnit.Any(l => l.TenantId == _tenantId && l.Description == typeOfAccount.Description))
            {
                return;
            }
            typeOfAccount.TenantId = _tenantId;
            typeOfAccount.IsEditable = false;
            _context.TypeOfAccountUnit.Add(typeOfAccount);

            _context.SaveChanges();
        }

        private static void TypeOfAccountList()
        {
            InitialTypeOfAccountList = new List<TypeOfAccountUnit>
            {
                    new TypeOfAccountUnit(description:"Income",caption:"Income",typeOfAccountClassificationId:4,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Deferred Income (Revenue Recognition)",caption:"Income",typeOfAccountClassificationId:4,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Income (Interco Eliminations)",caption:"Income (ICT Elim)",typeOfAccountClassificationId:4,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Overage Income",caption:"NULL",typeOfAccountClassificationId:4,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Editorial/Post Income",caption:"NULL",typeOfAccountClassificationId:4,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Income Accrual",caption:"NULL",typeOfAccountClassificationId:4,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Other Income",caption:"Other Income",typeOfAccountClassificationId:4,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Expense",caption:"Expense",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Production Expense",caption:"Production Expense",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Corporate Expense",caption:"Corporate Expense",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Payroll Expense",caption:"Payroll Expense",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Other Expense",caption:"Other Expense",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Other Expense (-Budget only)",caption:"Exp (-Budget only)",typeOfAccountClassificationId:7,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Cost Of Goods Sold",caption:"COGS",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Cash",caption:"Cash",typeOfAccountClassificationId:1,isCurrencyCodeRequired:false,isPaymentType:true),
                    new TypeOfAccountUnit(description:"Petty Cash",caption:"Petty Cash",typeOfAccountClassificationId:1,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Bank Account",caption:"Bank",typeOfAccountClassificationId:1,isCurrencyCodeRequired:true,isPaymentType:true),
                    new TypeOfAccountUnit(description:"Debit Card",caption:"Debit Card",typeOfAccountClassificationId:1,isCurrencyCodeRequired:true,isPaymentType:true),
                    new TypeOfAccountUnit(description:"Current Asset",caption:"Current Asset",typeOfAccountClassificationId:1,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Inventory Asset",caption:"Inventory",typeOfAccountClassificationId:1,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Fixed Asset",caption:"Fixed Asset",typeOfAccountClassificationId:1,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Other Asset",caption:"Other Asset",typeOfAccountClassificationId:1,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Credit Card",caption:"Credit Card",typeOfAccountClassificationId:2,isCurrencyCodeRequired:false,isPaymentType:true),
                    new TypeOfAccountUnit(description:"Inter-Company Transfer",caption:"Inter-Company",typeOfAccountClassificationId:1,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Shipping Account",caption:"Shipping Account",typeOfAccountClassificationId:2,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Payroll Liability",caption:"Payroll Liab",typeOfAccountClassificationId:2,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Current Liability",caption:"Current Liab",typeOfAccountClassificationId:2,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Contra-Asset",caption:"Contra-Asset",typeOfAccountClassificationId:6,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Long-Term Liability",caption:"Long-Term Liab",typeOfAccountClassificationId:2,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Equity",caption:"Equity",typeOfAccountClassificationId:3,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Profit/Loss Current Year",caption:"P&L",typeOfAccountClassificationId:3,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Insurance",caption:"NULL",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Sales Commission",caption:"NULL",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"DGA Fringe",caption:"NULL",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"DGA/Creative Fee",caption:"NULL",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Director Profit",caption:"NULL",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section A Fringe",caption:"Section A Fringe",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section A Fee",caption:"Section A Fee",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section B Fringe",caption:"Section B Fringe",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section B Fee",caption:"Section B Fee",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section G Fringe",caption:"Section G Fringe",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section G Fee",caption:"Section G Fee",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section L Fringe",caption:"Section L Fringe",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section L Fee",caption:"Section L Fee",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section M Fringe",caption:"Section M Fringe",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section M Fee",caption:"Section M Fee",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section O Fringe",caption:"Section O Fringe",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section O Fee",caption:"Section O Fee",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section C Fringe",caption:"Section C Fringe",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section C Fee",caption:"Section C Fee",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section D Fringe",caption:"Section D Fringe",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section D Fee",caption:"Section D Fee",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section E Fringe",caption:"Section E Fringe",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section E Fee",caption:"Section E Fee",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section F Fringe",caption:"Section F Fringe",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section F Fee",caption:"Section F Fee",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section H Fringe",caption:"Section H Fringe",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section H Fee",caption:"Section H Fee",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section I Fringe",caption:"Section I Fringe",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section I Fee",caption:"Section I Fee",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section J Fringe",caption:"Section J Fringe",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section J Fee",caption:"Section J Fee",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section K Fringe",caption:"Section K Fringe",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section K Fee",caption:"Section K Fee",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section N Fringe",caption:"Section N Fringe",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section N Fee",caption:"Section N Fee",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section P Fringe",caption:"Section P Fringe",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Section P Fee",caption:"Section P Fee",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false),
                    new TypeOfAccountUnit(description:"Expense Accrual",caption:"Expense Accrual",typeOfAccountClassificationId:5,isCurrencyCodeRequired:false,isPaymentType:false)
            };
        }

    }
}
