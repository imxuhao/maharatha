using EntityFramework.DynamicFilters;
using CAPS.CORPACCOUNTING.EntityFramework;

namespace CAPS.CORPACCOUNTING.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly CORPACCOUNTINGDbContext _context;

        public TestDataBuilder(CORPACCOUNTINGDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new TestOrganizationUnitsBuilder(_context).Create();

            _context.SaveChanges();
        }
    }
}
