using System.Linq;
using CAPS.CORPACCOUNTING.EntityFramework;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Accounting;

namespace CAPS.CORPACCOUNTING.Migrations.Seed
{
    public class DefaultTypeOfAccountClassificationCreator
    {
        public static List<TypeOfAccountClassificationUnit> InitialTypeOfAccountClassificationList { get; private set; }

        private readonly CORPACCOUNTINGDbContext _context;

        static DefaultTypeOfAccountClassificationCreator()
        {
            InitialTypeOfAccountClassificationList = new List<TypeOfAccountClassificationUnit>
            {
                new TypeOfAccountClassificationUnit(description:"Asset",caption:"Asset",isAccountSignPositive:true,isBalanceSheetAccount:true),
                new TypeOfAccountClassificationUnit(description:"Liability",caption:"Liability",isAccountSignPositive:false,isBalanceSheetAccount:true),
                new TypeOfAccountClassificationUnit(description:"Equity",caption:"Equity",isAccountSignPositive:false,isBalanceSheetAccount:true),
                new TypeOfAccountClassificationUnit(description:"Income",caption:"Income",isAccountSignPositive:false,isBalanceSheetAccount:false),
                 new TypeOfAccountClassificationUnit(description:"Expense",caption:"Expense",isAccountSignPositive:true,isBalanceSheetAccount:false),
                  new TypeOfAccountClassificationUnit(description:"Contra-Asset",caption:"Contra-Asset",isAccountSignPositive:false,isBalanceSheetAccount:true),
                  new TypeOfAccountClassificationUnit(description:"Expense (-Budget)",caption:"Expense (-Budget)",isAccountSignPositive:true,isBalanceSheetAccount:false),
            };
        }

        public DefaultTypeOfAccountClassificationCreator(CORPACCOUNTINGDbContext context)
        {
            _context = context;
        }


        public void Create()
        {
            CreateTypeOfAccountClassificationList();
        }

        private void CreateTypeOfAccountClassificationList()
        {
            foreach (var classificationList in InitialTypeOfAccountClassificationList)
            {
                AddTypeOfAccountClassificationIfNotExists(classificationList);
            }
        }

        private void AddTypeOfAccountClassificationIfNotExists(TypeOfAccountClassificationUnit typeClassification)
        {
            if (_context.TypeOfAccountClassificationUnit.Any(l => l.Description == typeClassification.Description))
            {
                return;
            }

            _context.TypeOfAccountClassificationUnit.Add(typeClassification);

            _context.SaveChanges();
        }

    }
}
