using System.Data.Entity;
using System.Linq;
using Abp.Application.Editions;
using CAPS.CORPACCOUNTING.Editions;
using CAPS.CORPACCOUNTING.EntityFramework;
using EfEnumToLookup.LookupGenerator;

namespace CAPS.CORPACCOUNTING.Migrations.Seed
{

    /// <summary>
    /// EnumGenerator is a class which will create Enum tables in the database for refrence purpose only along with Foreign Key Relationship
    /// </summary>
   public  class EnumGenerator :DropCreateDatabaseAlways<CORPACCOUNTINGDbContext>
    {
        private readonly CORPACCOUNTINGDbContext _context;

        public EnumGenerator(CORPACCOUNTINGDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            ExampleOfUsingApply();
        }
        public void ExampleOfUsingApply()
        {
            var enumToLookup = new EnumToLookup();
               // enumToLookup.NameFieldLength = 42; // optional, example of how to override default values

                // This would normally be run inside either a db initializer Seed()
                // or the migration Seed() method which both provide access to a context.
                enumToLookup.Apply(_context);
            
        }

    }
}
