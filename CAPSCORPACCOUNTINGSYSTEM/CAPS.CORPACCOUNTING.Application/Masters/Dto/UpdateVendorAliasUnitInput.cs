using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
   public class UpdateVendorAliasUnitInput : IInputDto
    {

        /// <summary>Gets or sets the VendorAliasId field. </summary>
        public virtual int VendorAliasId { get; set; }
        /// <summary>Gets or sets the VendorId field. </summary>
        public virtual int VendorId { get; set; }
        /// <summary>Gets or sets the AliasName field. </summary>
        public virtual string AliasName { get; set; }
    }
}
