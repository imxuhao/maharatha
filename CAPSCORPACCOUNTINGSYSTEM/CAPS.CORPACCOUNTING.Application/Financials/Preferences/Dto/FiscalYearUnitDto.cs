using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Banking;
using CAPS.CORPACCOUNTING.JobCosting;

namespace CAPS.CORPACCOUNTING.Financials.Preferences.Dto
{
    [AutoMapTo(typeof(FiscalYearUnit))]
    public class FiscalYearUnitDto : IOutputDto
    {
        public int FiscalYearId { get; set; }
        public DateTime YearStartDate { get; set; }

        /// <summary>Gets or sets the YearEndDate field. </summary>
        public DateTime YearEndDate { get; set; }

        /// <summary>Gets or sets the IsYearOpen field. </summary>
        public bool IsYearOpen { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public bool IsApproved { get; set; }

        /// <summary>Gets or sets the TypeOfInactiveStatusID field. </summary>
        public TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }

        /// <summary>Gets or sets the IsCPAClosed field. </summary>
        public bool? IsCpaClosed { get; set; }

        /// <summary>Gets or sets the DateCPAClosed field. </summary>
        public DateTime? DateCpaClosed { get; set; }

        /// <summary>Gets or sets the CPAUserID field. </summary>
        public int? CpaUserId { get; set; }

        /// <summary>Gets or sets the IsDefaultReportingYear field. </summary>
        public bool? IsDefaultReportingYear { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }

        public List<FiscalPeriodUnitDto> FiscalPeriodList { get; set; }



    }
}
