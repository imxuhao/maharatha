using Abp.Application.Services.Dto;
using  Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapFrom(typeof(AddressUnit))]  
    public class AddressUnitDto : AuditedEntityDto
    {
        /// <summary>>Gets or sets the AddressId</summary>
        public long AddressId { get; set; }
        /// <summary>Gets or sets the ObjectId field. </summary>
        public int EmployeeId { get; set; }

        /// <summary>Gets or sets the TypeofObjectId field. </summary>
        public TypeofObject TypeofObjectId { get; set; }

        /// <summary>Gets or sets the AddressTypeId field. </summary>
        public TypeofAddress AddressTypeId { get; set; }

        /// <summary>Gets or sets the ContactNumber field. </summary>
        public string ContactNumber { get; set; }

        /// <summary>Gets or sets the Line1 field. </summary>
        public string Line1 { get; set; }

        /// <summary>Gets or sets the Line2 field. </summary>
        public string Line2 { get; set; }

        /// <summary>Gets or sets the Line3 field. </summary>
        public string Line3 { get; set; }

        /// <summary>Gets or sets the Line4 field. </summary>
        public string Line4 { get; set; }

        /// <summary>Gets or sets the City field. </summary>
        public string City { get; set; }

        /// <summary>Gets or sets the State field. </summary>
        public string State { get; set; }

        /// <summary>Gets or sets the Country field. </summary>
        public string Country { get; set; }

        /// <summary>Gets or sets the PostalCode field. </summary>
        public string PostalCode { get; set; }

        /// <summary>Gets or sets the Fax field. </summary>
        public string Fax { get; set; }

        /// <summary>Gets or sets the Email field. </summary>
        public string Email { get; set; }

        /// <summary>Gets or sets the Phone1 field. </summary>
        public string Phone1 { get; set; }

        /// <summary>Gets or sets the Phone1Extension field. </summary>
        public string Phone1Extension { get; set; }

        /// <summary>Gets or sets the Phone2 field. </summary>
        public string Phone2 { get; set; }

        /// <summary>Gets or sets the Phone2Extension field. </summary>
        public string Phone2Extension { get; set; }

        /// <summary>Gets or sets the Website field. </summary>
        public string Website { get; set; }

        /// <summary>Gets or sets the IsPrimary field. </summary>
        public bool IsPrimary { get; set; }

        public int TenantId { get; set; }
        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
