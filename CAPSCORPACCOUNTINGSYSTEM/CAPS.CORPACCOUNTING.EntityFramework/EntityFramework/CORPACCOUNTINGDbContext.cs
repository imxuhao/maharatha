using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using CAPS.CORPACCOUNTING.Authorization.Roles;
using CAPS.CORPACCOUNTING.Authorization.Users;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.MultiTenancy;
using CAPS.CORPACCOUNTING.Storage;

namespace CAPS.CORPACCOUNTING.EntityFramework
{
    public class CORPACCOUNTINGDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        /* Define an IDbSet for each entity of the application */

        public virtual IDbSet<BinaryObject> BinaryObjects { get; set; }

        /// <summary>
        /// Chart of Accounts Declaration
        /// </summary>
        public  virtual  IDbSet<CoaUnit> CoaUnits { get; set; }

        /// <summary>
        ///  Accounts Declaration
        /// </summary>
        public virtual IDbSet<AccountUnit> AccountUnits { get; set; }


        /* Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         * But it may cause problems when working Migrate.exe of EF. ABP works either way.         * 
         */
        public CORPACCOUNTINGDbContext()
            : base("Default")
        {

        }

        /* This constructor is used by ABP to pass connection string defined in CORPACCOUNTINGDataModule.PreInitialize.
         * Notice that, actually you will not directly create an instance of CORPACCOUNTINGDbContext since ABP automatically handles it.
         */
        public CORPACCOUNTINGDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        /* This constructor is used in tests to pass a fake/mock connection.
         */
        public CORPACCOUNTINGDbContext(DbConnection dbConnection)
            : base(dbConnection, true)
        {

        }
    }
}
