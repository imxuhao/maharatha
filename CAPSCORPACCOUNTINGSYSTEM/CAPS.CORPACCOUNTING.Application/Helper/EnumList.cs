using CAPS.CORPACCOUNTING.Masters;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic;
using System.Linq;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.Accounting;
using CAPS.CORPACCOUNTING.JobCosting;
using CAPS.CORPACCOUNTING.Journals;

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
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).OrderBy(p => p.Name).ToList();
            return listEnums;
        }
        /// <summary>
        /// Type of chart is.
        /// </summary>
        /// <returns>HOME,PROJECT and REPORTING</returns>
        public static List<NameValueDto> GetTypeOfChartList()
        {
            var listEnums = (from TypeOfChart n in Enum.GetValues(typeof(TypeOfChart))
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
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).OrderBy(p => p.Name).ToList();
            return listEnums;
        }

        /// <summary>
        /// Get TypeOfInactiveStatus Enum as List
        /// </summary>
        /// <returns></returns>
        public static List<NameValueDto> GetTypeofSubAccountList()
        {
            var listEnums = (from TypeofSubAccount n in Enum.GetValues(typeof(TypeofSubAccount))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).OrderBy(p => p.Name).ToList();
            return listEnums;
        }


        /// <summary>
        /// Get TypeofConsolidation Enum as List
        /// </summary>
        /// <returns></returns>
        public static List<NameValueDto> GetTypeofConsolidationList()
        {
            var listEnums = (from TypeofConsolidation n in Enum.GetValues(typeof(TypeofConsolidation))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).OrderBy(p=>p.Name).ToList();
            return listEnums;
        }


        /// <summary>
        /// Get TypeofPaymentMethod Enum as List
        /// </summary>
        /// <returns></returns>
        public static List<NameValueDto> GetTypeofPaymentMethodList()
        {
            var listEnums = (from TypeofPaymentMethod n in Enum.GetValues(typeof(TypeofPaymentMethod))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).OrderBy(p => p.Name).ToList();
            return listEnums;
        }

        /// <summary>
        /// Get Typeof1099T4 Enum as List
        /// </summary>
        /// <returns></returns>
        public static List<NameValueDto> GetTypeof1099T4List()
        {
            var listEnums = (from Typeof1099T4 n in Enum.GetValues(typeof(Typeof1099T4))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).OrderBy(p => p.Name).ToList();
            return listEnums;
        }

        /// <summary>
        /// Get TypeofVendor Enum as List
        /// </summary>
        /// <returns></returns>
        public static List<NameValueDto> GetTypeofVendorList()
        {
            var listEnums = (from TypeofVendor n in Enum.GetValues(typeof(TypeofVendor))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).OrderBy(p => p.Name).ToList();
            return listEnums;
        }

        /// <summary>
        /// Get TypeofAddress Enum as List
        /// </summary>
        /// <returns></returns>
        public static List<NameValueDto> GetTypeofAddressList()
        {
            var listEnums = (from TypeofAddress n in Enum.GetValues(typeof(TypeofAddress))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).OrderBy(p => p.Name).ToList();
            return listEnums;
        }

        /// <summary>
        /// Get TypeofObject Enum as List
        /// </summary>
        /// <returns></returns>
        public static List<NameValueDto> GetTypeofObjectList()
        {
            var listEnums = (from TypeofObject n in Enum.GetValues(typeof(TypeofObject))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).OrderBy(p => p.Name).ToList();
            return listEnums;
        }

        public static List<NameValueDto> GetBudgetSoftwareList()
        {
            var listEnums = (from BudgetSoftware n in Enum.GetValues(typeof(BudgetSoftware))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).OrderBy(p => p.Name).ToList();
            return listEnums;
        }
        public static List<NameValueDto> GetProjectStatusList()
        {
            var listEnums = (from ProjectStatus n in Enum.GetValues(typeof(ProjectStatus))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).OrderBy(p => p.Name).ToList();
            return listEnums;
        }

        public static List<NameValueDto> GetProjectTypeList()
        {
            var listEnums = (from TypeofProject n in Enum.GetValues(typeof(TypeofProject))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).OrderBy(p => p.Name).ToList();
            return listEnums;
        }

        public static List<NameValueDto> GetTypeOfTaxList()
        {
            var listEnums = (from TypeofTax n in Enum.GetValues(typeof(TypeofTax))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).OrderBy(p => p.Name).ToList();
            return listEnums;
        }

        public static List<NameValueDto> GetJournalTypeList()
        {
            var listEnums = (from JournalType n in Enum.GetValues(typeof(JournalType))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).OrderBy(p => p.Name).ToList();
            return listEnums;
        }

        public static List<NameValueDto> GetBatchTypeList()
        {
            var listEnums = (from TypeOfBatch n in Enum.GetValues(typeof(TypeOfBatch))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).OrderBy(p => p.Name).ToList();
            return listEnums;
        }

        public static List<NameValueDto> GetBankAccountTypeList()
        {
            var listEnums = (from TypeOfBankAccount n in Enum.GetValues(typeof(TypeOfBankAccount))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).OrderBy(p => p.Name).ToList();
            return listEnums;
        }
        public  static List<NameValueDto> GetCheckGrouopList()
        {
            var listEnums = (from TypeOfCheckGroup n in Enum.GetValues(typeof(TypeOfCheckGroup))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).OrderBy(p => p.Name).ToList();
            return listEnums;
        }

        public static List<NameValueDto> GetTypeOfInvoiceList()
        {
            var listEnums = (from TypeOfInvoice n in Enum.GetValues(typeof(TypeOfInvoice))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).OrderBy(p => p.Name).ToList();
            return listEnums;
        }

        public static List<NameValueDto> GetCheckTypeList()
        {
            var listEnums = (from CheckType n in Enum.GetValues(typeof(CheckType))
                             select new NameValueDto { Value = ((int)n).ToString(), Name = EnumHelper.ToDisplayName(n) }).OrderBy(p => p.Name).ToList();
            return listEnums;
        }

    }
}
