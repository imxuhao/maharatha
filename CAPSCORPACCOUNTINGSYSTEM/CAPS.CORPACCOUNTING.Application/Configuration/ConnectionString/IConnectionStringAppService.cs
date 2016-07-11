using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Configuration.ConnectionString.Dto;

namespace CAPS.CORPACCOUNTING.Configuration.ConnectionString
{
    /// <summary>
    /// ConnectionString Service
    /// </summary>
    public interface IConnectionStringAppService : IApplicationService
    {
        /// <summary>
        /// Create ConnectionString
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
         Task CreateConnectionStringUnit(ConnectionStringInput input);
    }


}
