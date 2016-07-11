using CAPS.CORPACCOUNTING.EntityFramework;

namespace CAPS.CORPACCOUNTING.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly CORPACCOUNTINGDbContext _context;

        public InitialHostDbBuilder(CORPACCOUNTINGDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new EnumGenerator(_context).Create();
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
            new DefaultGridListCreator(_context).Create();
            new DefaultCurrencyCreator(_context, 1).Create();
            new DefaultTypeOfAccountClassificationCreator(_context).Create();
            new DefaultTypeOfAccountCreator(_context, 1).Create();
            new DefaultTypeOfCountryCreator(_context).Create();
            new DefaultRegionCreator(_context, 1).Create();
            new DefaultCountryCreator(_context, 1).Create();
            new DefaultValueAddedTaxTypeCreator(_context).Create();
            new DefaultValueAddedTaxRecoveryCreator(_context).Create();
            new DefaultSystemViewSettingsCreator(_context).Create();
            new DefaultTypeofUploadFileCreator(_context).Create();
            new DefaultConnectionStringCreator(_context).Create();
            new DefaultOrganizationCreator(_context).Create();
            _context.SaveChanges();
        }
    }
}
