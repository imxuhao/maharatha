﻿using CAPS.CORPACCOUNTING.Masters;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic;
using System.Linq;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.JobCosting;

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


        /// <summary>
        /// Get TypeofConsolidation Enum as List
        /// </summary>
        /// <returns></returns>
        public static List<NameValueDto> GetTypeofConsolidationList()
        {
            var listEnums = (from TypeofConsolidation n in Enum.GetValues(typeof(TypeofConsolidation))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).ToList();
            return listEnums;
        }

        public static List<NameValueDto> GetBudgetSoftwareList()
        {
            var listEnums = (from BudgetSoftware n in Enum.GetValues(typeof(BudgetSoftware))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).ToList();
            return listEnums;
        }
        public static List<NameValueDto> GetProjectStatusList()
        {
            var listEnums = (from ProjectStatus n in Enum.GetValues(typeof(ProjectStatus))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).ToList();
            return listEnums;
        }
    }
}
