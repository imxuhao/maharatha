using System.Linq;
using CAPS.CORPACCOUNTING.EntityFramework;
using CAPS.CORPACCOUNTING.Masters;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Migrations.Seed
{
    public class DefaultCurrencyCreator
    {
        public static List<TypeOfCurrencyUnit> InitialTypeOfCurrencyList { get; private set; }

        private readonly CORPACCOUNTINGDbContext _context;
        private readonly int _tenantId;

        public DefaultCurrencyCreator(CORPACCOUNTINGDbContext context, int tenantId)
        {
            _context = context;

            InitialTypeOfCurrencyList = new List<TypeOfCurrencyUnit>
            {
                new TypeOfCurrencyUnit(description:"US Dollar", caption:"", isocurrencycode:"USD", currencysymbol:"$", tenantid:tenantId, organizationunitid:null),
                new TypeOfCurrencyUnit(description:"Canadian Dollar", caption:"", isocurrencycode:"CAD", currencysymbol:"C$", tenantid:tenantId, organizationunitid:null),
                new TypeOfCurrencyUnit(description:"Australian Dollar", caption:"", isocurrencycode:"AUD", currencysymbol:"AU$", tenantid:tenantId, organizationunitid:null),
                new TypeOfCurrencyUnit(description:"Belize Dollar", caption:"", isocurrencycode:"BZD", currencysymbol:"BZ$", tenantid:tenantId, organizationunitid:null)
            };
        }


        public void Create()
        {
            CreateGridList();
        }

        private void CreateGridList()
        {
            foreach (var currencyList in InitialTypeOfCurrencyList)
            {
                AddCurrencyListIfNotExists(currencyList);
            }
        }

        private void AddCurrencyListIfNotExists(TypeOfCurrencyUnit currencyList)
        {
            if (_context.TypeOfCurrencyUnit.Any(l => l.Description == currencyList.Description))
            {
                return;
            }

            _context.TypeOfCurrencyUnit.Add(currencyList);

            _context.SaveChanges();
        }

    }
}
