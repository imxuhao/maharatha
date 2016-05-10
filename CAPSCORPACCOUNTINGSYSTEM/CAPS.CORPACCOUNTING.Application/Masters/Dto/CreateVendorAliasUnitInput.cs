using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.ComponentModel.DataAnnotations;
namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapTo(typeof(VendorAliasUnit))]
    public class CreateVendorAliasUnitInput : IInputDto
    {

    /// <summary>Gets or sets the VendorId field. </summary>
        public virtual int VendorId { get; set; }
        /// <summary>Gets or sets the AliasName field. </summary>
        public virtual string AliasName { get; set; }
    }
}
