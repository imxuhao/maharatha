using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.ObjectModel;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapFrom(typeof(VendorAliasUnit))]
   public class VendorAliasUnitDto : IOutputDto
    {
        /// <summary>Gets or sets the VendorAliasId field. </summary>
        public virtual int VendorAliasId { get; set; }

        /// <summary>Gets or sets the VendorId field. </summary>
        public virtual int VendorId { get; set; }

        /// <summary>Gets or sets the AliasName field. </summary>
        public virtual string AliasName { get; set; }
      
    }
}
