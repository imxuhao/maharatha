using System.Collections.Generic;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class UpdateEmployeeUnitInput : IInputDto
    {
        /// <summary>Gets or sets the EmployeeId field. </summary>
        public int EmployeeId { get; set; }

        /// <summary>Gets or sets the LastName field. </summary>
        [Required]
        [StringLength(EmployeeUnit.MaxName)]
        public string LastName { get; set; }

        /// <summary>Gets or sets the FirstName field. </summary>
        [StringLength(EmployeeUnit.MaxName)]
        public string FirstName { get; set; }

        /// <summary>Gets or sets the EmployeeRegion field. </summary>
        [StringLength(EmployeeUnit.MaxRegionLength)]
        public string EmployeeRegion { get; set; }

        /// <summary>Gets or sets the SSNTaxID field. </summary>
        [StringLength(EmployeeUnit.MaxTaxIdLength)]
        public string SSNTaxId { get; set; }

        /// <summary>Gets or sets the FederalTaxID field. </summary>
        [StringLength(EmployeeUnit.MaxTaxIdLength)]
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

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
       
        /// <summary> Gets or Sets the Addresses of the Employee </summary>
        public List<UpdateAddressUnitInput> Addresses { get; set; }
    }

    
}
