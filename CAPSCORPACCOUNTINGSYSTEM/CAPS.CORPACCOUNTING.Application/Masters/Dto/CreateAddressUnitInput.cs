using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapTo(typeof(AddressUnit))]
    public class CreateAddressUnitInput : IInputDto
    {
        /// <summary>Gets or sets the ObjectId field. </summary>
        public long ObjectId { get; set; }
      
        /// <summary>Gets or sets the TypeofObjectId field. </summary>
        public TypeofObject TypeofObjectId { get; set; }

        /// <summary>Gets or sets the AddressTypeId field. </summary>
        public TypeofAddress AddressTypeId { get; set; }

        /// <summary>Gets or sets the ContactNumber field. </summary>
        [StringLength(AddressUnit.MaxStringNameLength)]
        public string ContactNumber { get; set; }

        /// <summary>Gets or sets the Line1 field. </summary>
        [StringLength(AddressUnit.MaxStringNameLength)]
        public string Line1 { get; set; }

        /// <summary>Gets or sets the Line2 field. </summary>
        [StringLength(AddressUnit.MaxStringNameLength)]
        public string Line2 { get; set; }

        /// <summary>Gets or sets the Line3 field. </summary>
        [StringLength(AddressUnit.MaxStringNameLength)]
        public string Line3 { get; set; }

        /// <summary>Gets or sets the Line4 field. </summary>
        [StringLength(AddressUnit.MaxStringNameLength)]
        public string Line4 { get; set; }

        /// <summary>Gets or sets the City field. </summary>
        [StringLength(AddressUnit.MaxStringNameLength)]
        public string City { get; set; }

        /// <summary>Gets or sets the State field. </summary>
        [StringLength(AddressUnit.MaxStringNameLength)]
        public string State { get; set; }

        /// <summary>Gets or sets the Country field. </summary>
        [StringLength(AddressUnit.MaxStringNameLength)]
        public string Country { get; set; }

        /// <summary>Gets or sets the PostalCode field. </summary>
        [StringLength(AddressUnit.MaxStringNameLength)]
        public string PostalCode { get; set; }

        /// <summary>Gets or sets the Fax field. </summary>
        [StringLength(AddressUnit.MaxStringNameLength)]
        public string Fax { get; set; }

        /// <summary>Gets or sets the Email field. </summary>
        [StringLength(AddressUnit.MaxStringNameLength)]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>Gets or sets the Phone1 field. </summary>
        [StringLength(AddressUnit.MaxStringNameLength)]
        public string Phone1 { get; set; }

        /// <summary>Gets or sets the Phone1Extension field. </summary>
        [StringLength(AddressUnit.MaxStringNameLength)]
        public string Phone1Extension { get; set; }

        /// <summary>Gets or sets the Phone2 field. </summary>
        [StringLength(AddressUnit.MaxStringNameLength)]
        public string Phone2 { get; set; }

        /// <summary>Gets or sets the Phone2Extension field. </summary>
        [StringLength(AddressUnit.MaxStringNameLength)]
        public string Phone2Extension { get; set; }

        /// <summary>Gets or sets the Website field. </summary>
        [StringLength(AddressUnit.MaxwebsiteLength)]
        public string Website { get; set; }

        /// <summary>Gets or sets the IsPrimary field. </summary>
        public bool IsPrimary { get; set; }       

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
