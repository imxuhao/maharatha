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

        public DefaultCountryCreator(CORPACCOUNTINGDbContext context)
        {
            _context = context;
            InitialCountryList = new List<CountryUnit>
            {
            new  CountryUnit(description:"United States",twoLetterAbbreviation:"US",threeLetterAbbreviation:"USA",isoNumber:"840"),
            new  CountryUnit(description:"United Kingdom",twoLetterAbbreviation:"GB",threeLetterAbbreviation:"GBR",isoNumber:"826"),
            new  CountryUnit(description:"Canada",twoLetterAbbreviation:"CA",threeLetterAbbreviation:"CAN",isoNumber:"124"),
            new  CountryUnit(description:"New Zealand",twoLetterAbbreviation:"NZ",threeLetterAbbreviation:"NZL",isoNumber:"554"),
            new  CountryUnit(description:"Australia",twoLetterAbbreviation:"AU",threeLetterAbbreviation:"AUS",isoNumber:"036"),
             new  CountryUnit(description:"Belize",twoLetterAbbreviation:"BZ",threeLetterAbbreviation:"BLZ",isoNumber:"084"),
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
            if (_context.CountryUnit.Any(l => l.Description == countryList.Description))
            {
                return;
            }

            _context.CountryUnit.Add(countryList);

            _context.SaveChanges();
        }

    }
}
