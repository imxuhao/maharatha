using CAPS.CORPACCOUNTING.EntityFramework;
using CAPS.CORPACCOUNTING.Migrations.Seed.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Migrations.Seed.Tenants
{
    public class InitialTenantDbBuilder
    {
        private readonly CORPACCOUNTINGDbContext _context;
        private readonly int _tenantId;
        public InitialTenantDbBuilder(CORPACCOUNTINGDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            //new DefaultEditionCreator(_context).Create();
            //new DefaultLanguagesCreator(_context).Create();
            //new DefaultSettingsCreator(_context).Create();

            new EnumGenerator(_context).Create();
            new DefaultGridListCreator(_context).Create();
            new DefaultCurrencyCreator(_context, _tenantId).Create();
            new DefaultTypeOfAccountClassificationCreator(_context).Create();
            new DefaultTypeOfAccountCreator(_context, _tenantId).Create();
            new DefaultTypeOfCountryCreator(_context).Create();
            new DefaultRegionCreator(_context, _tenantId).Create();
            new DefaultCountryCreator(_context, _tenantId).Create();
            new DefaultValueAddedTaxTypeCreator(_context).Create();
            new DefaultValueAddedTaxRecoveryCreator(_context).Create();
            new DefaultSystemViewSettingsCreator(_context).Create();
            new DefaultTypeofUploadFileCreator(_context).Create();
            _context.SaveChanges();


        }

    }
}
