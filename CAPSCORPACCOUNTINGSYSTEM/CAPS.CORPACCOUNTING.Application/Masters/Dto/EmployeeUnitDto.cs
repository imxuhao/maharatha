using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Collections.ObjectModel;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapFrom(typeof(EmployeeUnit))]  
    public class EmployeeUnitDto : IOutputDto
    {
        /// <summary>Gets or sets the  EmployeeId</summary>
        public int EmployeeId { get; set; }

        /// <summary>Gets or sets the LastName field. </summary>
        public string LastName { get; set; }

        /// <summary>Gets or sets the FirstName field. </summary>
        public string FirstName { get; set; }

        /// <summary>Gets or sets the EmployeeRegion field. </summary>
        public string EmployeeRegion { get; set; }

        /// <summary>Gets or sets the SSNTaxId field. </summary>
        public string SSNTaxId { get; set; }

        /// <summary>Gets or sets the FederalTaxID field. </summary>
        public string FederalTaxId { get; set; }

        /// <summary>Gets or sets the Is1099 field. </summary>
        public bool Is1099 { get; set; }

        /// <summary>Gets or sets the IsW9onFile field. </summary>
        public bool IsW9OnFile { get; set; }

        /// <summary>Gets or sets the IsIndependantContractor field. </summary>
        public bool IsIndependantContractor { get; set; }

        /// <summary>Gets or sets the IsCorporation field. </summary>
        public bool IsCorporation { get; set; }

        /// <summary>Gets or sets the IsProducer field. </summary>
        public bool IsProducer { get; set; }

        /// <summary>Gets or sets the IsDirector field. </summary>
        public bool IsDirector { get; set; }

        /// <summary>Gets or sets the IsDirPhoto field. </summary>
        public bool IsDirPhoto { get; set; }

        /// <summary>Gets or sets the IsSetDesigner field. </summary>
        public bool IsSetDesigner { get; set; }

        /// <summary>Gets or sets the IsEditor field. </summary>
        public bool IsEditor { get; set; }

        /// <summary>Gets or sets the IsArtDirector field. </summary>
        public bool IsArtDirector { get; set; }

        /// <summary>Gets or sets the IsApproved field. </summary>
        public bool IsApproved { get; set; }

        /// <summary>Gets or sets the IsActivet field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the TenantId field. </summary>
        public int TenantId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
        
        /// <summary> Gets or Sets the Addresses of the Employee </summary>
        public Collection<AddressUnitDto> Addresses { get; set; }
    }
}
