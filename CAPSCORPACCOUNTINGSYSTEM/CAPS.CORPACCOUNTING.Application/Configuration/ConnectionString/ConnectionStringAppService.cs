using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using CAPS.CORPACCOUNTING.Configuration.ConnectionString.Dto;
using CAPS.CORPACCOUNTING.Configuration.Tenants;
using CAPS.CORPACCOUNTING.Masters;
using Abp.AutoMapper;
using Abp.Runtime.Security;

namespace CAPS.CORPACCOUNTING.Configuration.ConnectionString
{
    public class ConnectionStringAppService : CORPACCOUNTINGAppServiceBase, IConnectionStringAppService
    {
        private readonly IRepository<ConnectionStringUnit> _connectionStringRepository;
        public ConnectionStringAppService(IRepository<ConnectionStringUnit> connectionStringRepository)
        {
            _connectionStringRepository = connectionStringRepository;
        }

        /// <summary>
        /// Create ConnectionStrings
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task CreateConnectionStringUnit(ConnectionStringInput input)
        {
            string connectionstring = string.Empty;

            var accountUnit = input.MapTo<ConnectionStringUnit>();

            connectionstring = "Server =" + input.ServerName;
            if (!string.IsNullOrEmpty(input.InstanceName))
                connectionstring = connectionstring + @"\" + input.InstanceName;
            connectionstring = connectionstring + "; Database=" + input.Database +";";
            if (!input.TrustedConnection)
            {
                connectionstring = connectionstring + "; UserId=" + input.Database + "; Password =" + input.Password +
                                   "Trusted_Connection=false";
            }
            else
            {
                connectionstring =connectionstring+ "Trusted_Connection=true";

            }
            accountUnit.ConnectionString = SimpleStringCipher.Instance.Encrypt(connectionstring);
            await _connectionStringRepository.InsertAsync(accountUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

        }
    }
}
