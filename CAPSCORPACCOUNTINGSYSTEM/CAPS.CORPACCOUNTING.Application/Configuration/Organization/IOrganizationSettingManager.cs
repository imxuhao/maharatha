using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Configuration;
using CAPS.CORPACCOUNTING.Authorization.Users;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

namespace CAPS.CORPACCOUNTING.Configuration.Organization
{
    public interface IOrganizationSettingManager
    {
      
      /// <summary>
      /// 
      /// </summary>
      /// <param name="organizationUnitId"></param>
      /// <param name="name"></param>
      /// <param name="value"></param>
      /// <returns></returns>

        Task ChangeSettingForOrganizationAsync(long organizationUnitId, string name, string value);

         Task<string> GetSettingValueForOrganization(long OrganizationUnitId, string name);
     

    }


}
