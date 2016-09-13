using Abp.Runtime.Validation;

namespace CAPS.CORPACCOUNTING.Configuration.Tenants.Dto
{
    public class CompanySettingsEditDto 
    {
        public bool IsAllowDuplicateAPInvoiceNos { get; set; }

        public bool IsAllowDuplicateARInvoiceNos { get; set; }

        public bool IsAllowAccountnumbersStartingwithZero { get; set; }
        public int? SetDefaultAPTerms { get; set; }

        public int? SetDefaultARTerms { get; set; }
        public bool IsImportPOlogsfromProducersActualUploads { get; set; }

        public bool BuildAPuponCCstatementPosting { get; set; }

        public bool BuildAPuponPayrollPosting { get; set; }

        public string ARAgingDate { get; set; }

        public string APAgingDate { get; set; }

        public int? DepositGracePeriods { get; set; }

        public int? PaymentsGracePeriods { get; set; }

        public string DefaultAPPostingDate { get; set; }

        public long? DefaultBank { get; set; }

        public bool AllowTransactionsJobWithGL { get; set; }

        public long? OrganizationUnitId { get; set; }

        public bool POAutoNumberingforDivisions { get; set; }
        public bool POAutoNumberingforProjects { get; set; }
    }
}
