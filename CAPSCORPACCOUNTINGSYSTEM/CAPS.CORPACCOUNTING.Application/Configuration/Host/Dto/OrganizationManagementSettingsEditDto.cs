using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Configuration.Host.Dto
{
  public class OrganizationManagementSettingsEditDto : IValidate
    {
        public bool IsAllowDuplicateAPInvoiceNos { get; set; }

        public bool IsAllowDuplicateARInvoiceNos { get; set; }

        public bool IsAllowAccountnumbersStartingwithZero  { get; set; }

        public bool IsImportPOlogsfromProducersActualUploads { get; set; }

        public bool BuildAPuponCCstatementPosting { get; set; }

        public bool BuildAPuponPayrollPosting { get; set; }

        public bool POAutoNumbering { get; set; }

        public bool ARAgingDate { get; set; }

        public bool APAgingDate { get; set; }

        public int? DepositGracePeriods { get; set; }

        public int? PaymentsGracePeriods { get; set; }

        public bool DefaultAPPostingDate { get; set; }

        public long? DefaultBank { get; set; }
        
        public bool AllowTransactionsJobWithGL { get; set; }
    }	
}
