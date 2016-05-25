using EntityFramework.DynamicFilters;
using CAPS.CORPACCOUNTING.EntityFramework;

namespace CAPS.CORPACCOUNTING.Migrations.Seed
{
    public class InitialDbBuilder
    {
        private readonly CORPACCOUNTINGDbContext _context;

        public InitialDbBuilder(CORPACCOUNTINGDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            // new DefaultEditionCreator(_context).Create();
            //new DefaultLanguagesCreator(_context).Create();
            //new DefaultTenantRoleAndUserCreator(_context).Create();
            //new DefaultSettingsCreator(_context).Create();

            new EnumGenerator(_context).Create();
            new DefaultGridListCreator(_context).Create();
            new DefaultCurrencyCreator(_context, 1).Create();
            new DefaultTypeOfAccountClassificationCreator(_context).Create();
            new DefaultTypeOfAccountCreator(_context, 1).Create();
            new DefaultTypeOfCountryCreator(_context).Create();
            new DefaultRegionCreator(_context,1).Create();
            new DefaultCountryCreator(_context, 1).Create();
            //new DefaultValueAddedTaxTypeCreator(_context).Create();
            //new DefaultValueAddedTaxRecoveryCreator(_context).Create();
            new DefaultSystemViewSettingsCreator(_context).Create();
            _context.SaveChanges();
        }
        
    }
}
