using EntityFramework.DynamicFilters;
using CAPS.CORPACCOUNTING.EntityFramework;

namespace CAPS.CORPACCOUNTING.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly CORPACCOUNTINGDbContext _context;
        private readonly int _tenantId;

        public TestDataBuilder(CORPACCOUNTINGDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new TestOrganizationUnitsBuilder(_context, _tenantId).Create();

            _context.SaveChanges();
        }
    }
}
