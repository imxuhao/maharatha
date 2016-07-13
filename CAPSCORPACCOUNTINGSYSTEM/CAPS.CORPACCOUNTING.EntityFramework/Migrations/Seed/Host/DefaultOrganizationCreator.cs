using System.Linq;
using Abp.Application.Editions;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Editions;
using CAPS.CORPACCOUNTING.EntityFramework;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Organization;

namespace CAPS.CORPACCOUNTING.Migrations.Seed.Host
{
    public class DefaultOrganizationCreator
    {
        private readonly CORPACCOUNTINGDbContext _context;

        public static List<OrganizationExtended> InitialOrganization { get; private set; }

        public DefaultOrganizationCreator(CORPACCOUNTINGDbContext context)
        {
            _context = context;
        }

        static DefaultOrganizationCreator()
        { 
        
            InitialOrganization = new List<OrganizationExtended>
            {
                new OrganizationExtended(null)

            };
        }
        public void Create()
        {
            CreateOrganizations();
        }

        private void CreateOrganizations()
        {
            foreach (var organization in InitialOrganization)
            {
                AddOrganizationListIfNotExists(organization);
            }
        }
        private void AddOrganizationListIfNotExists(OrganizationExtended organization)
        {
            if (_context.OrganizationExtended.Any(l => l.DisplayName == organization.DisplayName))
            {
                return;
            }

            _context.OrganizationExtended.Add(organization);
            _context.SaveChanges();
        }

    }
}