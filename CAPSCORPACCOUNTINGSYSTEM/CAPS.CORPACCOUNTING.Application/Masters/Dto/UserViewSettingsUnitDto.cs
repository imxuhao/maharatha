using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapFrom(typeof(UserViewSettingsUnit))]
    public class UserViewSettingsUnitDto : IOutputDto
    {
        /// <summary>Gets or sets the UserViewId field. </summary>
        public virtual int UserViewId { get; set; }

        /// <summary>Gets or sets the GridId field. </summary>
        public virtual int GridId { get; set; }

        /// <summary>Gets or sets the UserId field. </summary>
        public virtual long UserId { get; set; }

        /// <summary>Gets or sets the ViewSettings field. </summary>
        public virtual string ViewSettings { get; set; }

        /// <summary>Gets or sets the IsDefault field. </summary>
        public virtual bool? IsDefault { get; set; }

        public virtual string Grid_Name { get; set; }

        public virtual string Grid_Description { get; set; }
    }
}
