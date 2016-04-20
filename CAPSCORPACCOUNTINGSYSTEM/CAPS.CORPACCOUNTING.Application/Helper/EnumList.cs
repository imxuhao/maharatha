using CAPS.CORPACCOUNTING.Masters;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic;
using System.Linq;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Accounting;

namespace CAPS.CORPACCOUNTING.Helpers
{
    /// <summary>
    /// Get All lookup Tables and send as List
    /// </summary>
    public  class EnumList
    {
        /// <summary>
        /// Get StandardGroupTotal Enum as List
        /// </summary>
        /// <returns></returns>
        public static List<NameValueDto> GetStandardGroupTotalList()
        {
            var listEnums = (from StandardGroupTotal n in Enum.GetValues(typeof(StandardGroupTotal))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).ToList();
            return listEnums;
        }

        /// <summary>
        /// Get TypeOfInactiveStatus Enum as List
        /// </summary>
        /// <returns></returns>
        public static List<NameValueDto> GetTypeOfInactiveStatusList()
        {
            var listEnums = (from TypeOfInactiveStatus n in Enum.GetValues(typeof(TypeOfInactiveStatus))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).ToList();
            return listEnums;
        }

        /// <summary>
        /// Get TypeOfInactiveStatus Enum as List
        /// </summary>
        /// <returns></returns>
        public static List<NameValueDto> GetTypeofSubAccountList()
        {
            var listEnums = (from TypeofSubAccount n in Enum.GetValues(typeof(TypeofSubAccount))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).ToList();
            return listEnums;
        }


    }
}
