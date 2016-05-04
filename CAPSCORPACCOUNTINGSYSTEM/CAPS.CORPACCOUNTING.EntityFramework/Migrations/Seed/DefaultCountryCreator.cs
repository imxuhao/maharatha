using System.Linq;
using CAPS.CORPACCOUNTING.EntityFramework;
using CAPS.CORPACCOUNTING.Masters;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Migrations.Seed
{
    public class DefaultCountryCreator
    {
        public static List<CountryUnit> InitialCountryList { get; private set; }

        private readonly CORPACCOUNTINGDbContext _context;

        static DefaultCountryCreator()
        {
            InitialCountryList = new List<CountryUnit>
            {
                new CountryUnit(6,1),
                 new CountryUnit(8,1),
                  new CountryUnit(39,1),
                   new CountryUnit(169,1),
                    new CountryUnit(225,1),
                     new CountryUnit(99,1),
                      new CountryUnit(74,1),
                       new CountryUnit(81,1)

            };
        }

        public DefaultCountryCreator(CORPACCOUNTINGDbContext context)
        {
            _context = context;
        }


        public void Create()
        {
            CreateCountryList();
        }

        private void CreateCountryList()
        {
            foreach (var country in InitialCountryList)
            {
                AddCountryListIfNotExists(country);
            }
        }

        private void AddCountryListIfNotExists(CountryUnit countryList)
        {
            if (_context.CountryUnit.Any(l => l.TenantId == countryList.TenantId && l.TypeOfCountryId == countryList.TypeOfCountryId))
            {
                return;
            }

            _context.CountryUnit.Add(countryList);

            _context.SaveChanges();
        }

    }
}
