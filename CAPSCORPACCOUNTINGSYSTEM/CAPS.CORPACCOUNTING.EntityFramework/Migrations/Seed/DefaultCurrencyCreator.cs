using System.Linq;
using CAPS.CORPACCOUNTING.EntityFramework;
using CAPS.CORPACCOUNTING.Masters;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Migrations.Seed
{
    public class DefaultCurrencyCreator
    {
        public static List<TypeOfCurrencyUnit> InitialTypeOfCurrencyList { get; private set; }

        public static Dictionary<string, string> CurrencyCountryMappingList { get; private set; }

        private readonly CORPACCOUNTINGDbContext _context;
        private readonly int _tenantId;
        public DefaultCurrencyCreator(CORPACCOUNTINGDbContext context)
        {
            _context = context;

            InitialTypeOfCurrencyList = new List<TypeOfCurrencyUnit>
            {
                new TypeOfCurrencyUnit(description:"US Dollar", caption:"", isocurrencycode:"USD", currencysymbol:"$", organizationunitid:null),
                new TypeOfCurrencyUnit(description:"Canadian Dollar", caption:"", isocurrencycode:"CAD", currencysymbol:"C$", organizationunitid:null),
                new TypeOfCurrencyUnit(description:"Australian Dollar", caption:"", isocurrencycode:"AUD", currencysymbol:"AU$", organizationunitid:null),
                new TypeOfCurrencyUnit(description:"Belize Dollar", caption:"", isocurrencycode:"BZD", currencysymbol:"BZ$", organizationunitid:null)
            };

            CurrencyCountryMappingList = new Dictionary<string, string>
            {
            {"US","US Dollar" },
            {"CA","Canadian Dollar" },
            {"AU","Australian Dollar" },
            {"BZ","Belize Dollar" }
            };
        }

        public void Create()
        {
            CreatecurrencyList();
        }

        private void CreatecurrencyList()
        {
            foreach (var currencyList in InitialTypeOfCurrencyList)
            {
                AddCurrencyListIfNotExists(currencyList);
            }
        }

        private void AddCurrencyListIfNotExists(TypeOfCurrencyUnit currencyList)
        {
           var countryAbbreviation= CurrencyCountryMappingList.Where(u => u.Value == currencyList.Description).FirstOrDefault();
            var country = _context.CountryUnit.Where(u => u.TwoLetterAbbreviation == countryAbbreviation.Key).FirstOrDefault();
            if (_context.TypeOfCurrencyUnit.Any(l =>  l.Description == currencyList.Description && l.CountryID==country.Id))
            {
                return;
            }
            currencyList.CountryID = country.Id;
            _context.TypeOfCurrencyUnit.Add(currencyList);

            _context.SaveChanges();
        }

    }
}
