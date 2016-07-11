using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var accountUnit = input.MapTo<ConnectionStringUnit>();
            accountUnit.ConnectionString=   SimpleStringCipher.Instance.Encrypt(input.ConnectionString);
            await _connectionStringRepository.InsertAsync(accountUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
        }
    }
}
