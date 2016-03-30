using System.Linq;
using CAPS.CORPACCOUNTING.EntityFramework;
using CAPS.CORPACCOUNTING.Masters;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Migrations.Seed
{
    public class DefaultGridListCreator
    {
        public static List<GridListUnit> InitialGridList { get; private set; }

        private readonly CORPACCOUNTINGDbContext _context;

        static DefaultGridListCreator()
        {
            InitialGridList = new List<GridListUnit>
            {
                new GridListUnit(gridid:1,name:"Tenants",description:"Tenants Information Showing in Grid")
            };
        }

        public DefaultGridListCreator(CORPACCOUNTINGDbContext context)  
        {
            _context = context;
        }


        public void Create()
        {
            CreateGridList();
        }

        private void CreateGridList()
        {
            foreach (var gridList in InitialGridList)
            {
                AddGridListIfNotExists(gridList);
            }
        }

        private void AddGridListIfNotExists(GridListUnit gridList)
        {
            if (_context.GridListUnit.Any(l => l.TenantId == gridList.TenantId && l.Name == gridList.Name))
            {
                return;
            }

            _context.GridListUnit.Add(gridList);

            _context.SaveChanges();
        }

    }
}
