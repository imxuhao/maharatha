using Abp.Domain.Uow;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.EntityFramework;
using CAPS.CORPACCOUNTING.Organization;
using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Migrations.Seed
{
    public class DefaultSecurityGroupCreator
    {
        public static List<OrganizationUnit> InitialSecurityGroupList { get; private set; }

        private readonly CORPACCOUNTINGDbContext _context;
        private readonly int _tenantId;

        public DefaultSecurityGroupCreator(CORPACCOUNTINGDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;


            InitialSecurityGroupList = new List<OrganizationUnit>
            {
                new OrganizationUnit(tenantId,"Standard Accessiblity"),
                  new OrganizationUnit(tenantId,"Controller Accessiblity"),
                    new OrganizationUnit(tenantId,"Extremely Sensitive Data"),
            };
        }
        public void Create()
        {
            CreateSecurityGroupList();
        }
        [UnitOfWork]
        private void CreateSecurityGroupList()
        {
            foreach (var securityGroup in InitialSecurityGroupList)
            {
                AddSecurityGroupIfNotExists(securityGroup);
            }
            
        }

        private void AddSecurityGroupIfNotExists(OrganizationUnit securityGroup)
        {
            if (_context.OrganizationUnits.Any(l => l.TenantId == securityGroup.TenantId && l.DisplayName == securityGroup.DisplayName))
            {
                return;
            }
            securityGroup.Code = GetNextChildCode(securityGroup.ParentId);
            //securityGroup.Code = GetNextChildCode(securityGroup.ParentId);
            _context.OrganizationUnits.Add(securityGroup);
            _context.SaveChanges();

        }

      
        #region Organization Code Generated
        public string GetNextChildCode(long? parentId)
        {
            var lastChild = GetLastChildOrNull(parentId);
            if (lastChild == null)
            {
                var parentCode = parentId != null ? GetCode(parentId.Value) : null;
                return OrganizationUnit.AppendCode(parentCode, OrganizationUnit.CreateCode(1));
            }

            return OrganizationUnit.CalculateNextCode(lastChild.Code);
        }
        public OrganizationUnit GetLastChildOrNull(long? parentId)
        {
            var children = _context.OrganizationUnits.Where(ou => ou.ParentId == parentId).ToList();
            return children.OrderBy(c => c.Code).LastOrDefault();
        }

        public string GetCode(long id)
        {
            return (_context.OrganizationUnits.FirstOrDefault(u => u.Id == id).Code);
        }

        #endregion

    }
}
