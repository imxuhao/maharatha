using System.Linq;
using CAPS.CORPACCOUNTING.EntityFramework;
using CAPS.CORPACCOUNTING.Masters;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Migrations.Seed
{
    public class DefaultValueAddedTaxTypeCreator
    {
        public static List<ValueAddedTaxTypeUnit> InitialValueAddedTaxTypeList { get; private set; }

        private readonly CORPACCOUNTINGDbContext _context;

        static DefaultValueAddedTaxTypeCreator()
        {
            InitialValueAddedTaxTypeList = new List<ValueAddedTaxTypeUnit>
            {
                new ValueAddedTaxTypeUnit(typeOfcountryId:15 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:22 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:58 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:59 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:68 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:73 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:74 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:81 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:84 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:97 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:103 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:105 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:117 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:123 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:124 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:132 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:150 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:160 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:171 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:172 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:194 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:193 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:199 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:205 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:206 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:225 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ValueAddedTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:39 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.GoodsAndServicesTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:39 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.ProvincialSalesTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:39 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.QuebecSalesTax,isactive:true),
                new ValueAddedTaxTypeUnit(typeOfcountryId:39 ,typeOfvalueaddedtaxId:TypeOfValueAddedTax.HarmonizedSalesTax,isactive:true),

            };
        }

        public DefaultValueAddedTaxTypeCreator(CORPACCOUNTINGDbContext context)
        {
            _context = context;
        }


        public void Create()
        {
            CreateValueAddedTaxTypeList();
        }

        private void CreateValueAddedTaxTypeList()
        {
            foreach (var valueAddedTaxType in InitialValueAddedTaxTypeList)
            {
                AddValueAddedTaxTypListIfNotExists(valueAddedTaxType);
            }
        }

        private void AddValueAddedTaxTypListIfNotExists(ValueAddedTaxTypeUnit valueAddedTaxType)
        {
            if (_context.ValueAddedTaxTypeUnit.Any(l => l.TypeOfValueAddedTaxId == valueAddedTaxType.TypeOfValueAddedTaxId &&
            l.TypeOfCountryId == valueAddedTaxType.TypeOfCountryId))
            {
                return;
            }

            _context.ValueAddedTaxTypeUnit.Add(valueAddedTaxType);

            _context.SaveChanges();
        }
    }
}
