using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Collections.ObjectModel;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapFrom(typeof(EmployeeUnit))]  
    public class EmployeeUnitDto : FullAuditedEntityDto
    {
        /// <summary>Gets or sets the  EmployeeId</summary>
        public int EmployeeId { get; set; }

        /// <summary>Gets or sets the LastName field. </summary>
        public string LastName { get; set; }

        /// <summary>Gets or sets the FirstName field. </summary>
        public string FirstName { get; set; }

        /// <summary>Gets or sets the EmployeeRegion field. </summary>
        public string EmployeeRegion { get; set; }

        /// <summary>Gets or sets the SSNTaxID field. </summary>
        public string SSNTaxId { get; set; }

        /// <summary>Gets or sets the [FederalTaxID] field. </summary>
        public string FederalTaxId { get; set; }

        /// <summary>Gets or sets the Is Is1099 field. </summary>
        public bool Is1099 { get; set; }

        /// <summary>Gets or sets the Is IsW9onFile field. </summary>
        public bool IsW9OnFile { get; set; }

        /// <summary>Gets or sets the Is IsIndependantContractor field. </summary>
        public bool IsIndependantContractor { get; set; }

        /// <summary>Gets or sets the Is IsCorporation field. </summary>
        public bool IsCorporation { get; set; }

        /// <summary>Gets or sets the Is IsProducer field. </summary>
        public bool IsProducer { get; set; }

        /// <summary>Gets or sets the Is IsDirector field. </summary>
        public bool IsDirector { get; set; }

        /// <summary>Gets or sets the Is IsDirPhoto field. </summary>
        public bool IsDirPhoto { get; set; }

        /// <summary>Gets or sets the Is IsSetDesigner field. </summary>
        public bool IsSetDesigner { get; set; }

        /// <summary>Gets or sets the Is IsEditor field. </summary>
        public bool IsEditor { get; set; }

        /// <summary>Gets or sets the Is IsArtDirector field. </summary>
        public bool IsArtDirector { get; set; }

        /// <summary>Gets or sets the Is IsApproved field. </summary>
        public bool IsApproved { get; set; }

        /// <summary>Gets or sets the Is IsActivet field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
        
        /// <summary> Gets or Sets Address Collection of a Employee </summary>
        public Collection<AddressUnitDto> Address { get; set; }
    }
}
