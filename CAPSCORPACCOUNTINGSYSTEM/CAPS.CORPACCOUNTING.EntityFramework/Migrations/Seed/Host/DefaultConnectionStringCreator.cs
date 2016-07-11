using System.Linq;
using Abp.Application.Editions;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Editions;
using CAPS.CORPACCOUNTING.EntityFramework;
using System.Collections.Generic;
using Abp.Runtime.Security;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Organization;

namespace CAPS.CORPACCOUNTING.Migrations.Seed.Host
{
    public class DefaultConnectionStringCreator
    {
        private readonly CORPACCOUNTINGDbContext _context;

        public static List<ConnectionStringUnit> InitialConnectionStrings { get; private set; }

        public DefaultConnectionStringCreator(CORPACCOUNTINGDbContext context)
        {
            _context = context;
        }
        
        static DefaultConnectionStringCreator()
        {
            string connectionstring =
                SimpleStringCipher.Instance.Encrypt(ConnectionStringHelper.GetConnectionString("Default"));
            InitialConnectionStrings = new List<ConnectionStringUnit>
            {
                new ConnectionStringUnit(connectionstring)

            };
        }
        public void Create()
        {
            CreateConnectionStrings();
        }

        private void CreateConnectionStrings()
        {
            foreach (var connectionString in InitialConnectionStrings)
            {
                AddConnectionStringListIfNotExists(connectionString);
            }
        }

        private void AddConnectionStringListIfNotExists(ConnectionStringUnit connectionString)
        {
            if (_context.ConnectionStrings.Any(l => l.Name == connectionString.Name))
            {
                return;
            }

            _context.ConnectionStrings.Add(connectionString);
            _context.SaveChanges();
        }

    }
}