using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Configuration.Host.Dto;
using CAPS.CORPACCOUNTING.Configuration.Tenants.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.MultiTenancy.Dto
{
    [AutoMapFrom(typeof(TenantExtendedUnit))]
    public  class ComapnyPreferenceDto: IOutputDto
    {
        /// <summary>Gets or sets the TenantExtendedId field. </summary>
        public int TenantExtendedId { get; set; }

        /// <summary>Gets or sets the TransmitterContactName field. </summary>
        public string TransmitterContactName { get; set; }

        /// <summary>Gets or sets the TransmitterEmailAddress field. </summary>
        public string TransmitterEmailAddress { get; set; }

        /// <summary>Gets or sets the TransmitterCode field. </summary>
        public string TransmitterCode { get; set; }

        /// <summary>Gets or sets the TransmitterControlCode field. </summary>
        public string TransmitterControlCode { get; set; }

        /// <summary>Gets or sets the FederalTaxID field. </summary>
        public string FederalTaxId { get; set; }

        /// <summary>Gets or sets the Address of the Organization. </summary>
        public AddressUnitDto Address { get; set; }

        public TenantSettingsEditDto CompanySettings { get; set; }
    }
}
