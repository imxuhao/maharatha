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
        private readonly int _tenantId;

        public DefaultCountryCreator(CORPACCOUNTINGDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;

            InitialCountryList = new List<CountryUnit>
            {
                new CountryUnit(6,tenantId),
                 new CountryUnit(8,tenantId),
                  new CountryUnit(39,tenantId),
                   new CountryUnit(169,tenantId),
                    new CountryUnit(225,tenantId),
                     new CountryUnit(99,tenantId),
                      new CountryUnit(74,tenantId),
                       new CountryUnit(81,tenantId)

            };
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
