using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.JobCosting;

namespace CAPS.CORPACCOUNTING.Financials.Preferences.Dto
{
    [AutoMapTo(typeof(FiscalPeriodUnit))]
    public class FiscalPeriodUnitDto : IOutputDto
    {
        public int Id { get; set; }
        /// <summary>Gets or sets the FiscalPeriodId field. </summary>
        public int FiscalPeriodId { get; set; }
        /// <summary>Gets or sets the FiscalYearId field. </summary>
        public int FiscalYearId { get; set; }

        /// <summary>Gets or sets the PeriodStartDate field. </summary>
        public DateTime PeriodStartDate { get; set; }

        /// <summary>Gets or sets the PeriodEndDate field. </summary>
        public DateTime PeriodEndDate { get; set; }

        /// <summary>Gets or sets the IsPeriodOpen field. </summary>
        public bool IsPeriodOpen { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusID field. </summary>
        public TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the IsCPAClosed field. </summary>
        public bool? IsCpaClosed { get; set; }

        /// <summary>Gets or sets the DateCPAClosed field. </summary>
        public DateTime? DateCpaClosed { get; set; }

        /// <summary>Gets or sets the CPAUserID field. </summary>
        public int? CpaUserId { get; set; }

        /// <summary>Gets or sets the IsYearEndAdjustmentsAllowed field. </summary>
        public bool? IsYearEndAdjustmentsAllowed { get; set; }

        /// <summary>Gets or sets the IsPreClose field. </summary>
        public bool? IsPreClose { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }

    }
}
