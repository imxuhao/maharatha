using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Authorization.Roles.Dto;
using CAPS.CORPACCOUNTING.Authorization.Users;
using CAPS.CORPACCOUNTING.Authorization.Users.Dto;
using CAPS.CORPACCOUNTING.Dto;

namespace CAPS.CORPACCOUNTING.Authorization.Custom
{
    /// <summary>
    /// Partial class for UserAppService
    /// </summary>
    public partial class UserAppService : CORPACCOUNTINGAppServiceBase
    {
        /// <summary>
        /// CreateOrUpdateUserUnit from User Permissions
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task UpdateUserUnit()
        {
            //await UserAppService. UpdateUserAsync(input);
        }
        
    }
}
