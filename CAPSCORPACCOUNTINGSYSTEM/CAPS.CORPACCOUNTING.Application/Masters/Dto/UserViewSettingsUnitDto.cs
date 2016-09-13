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
        public virtual int ViewId { get; set; }

        /// <summary>Gets or sets the UserId field. </summary>
        public virtual long UserId { get; set; }

        /// <summary>Gets or sets the ViewSettingName field. </summary>
        public virtual string ViewName { get; set; }
        
        /// <summary>Gets or sets the ViewSettings field. </summary>
        public virtual string ViewSettings { get; set; }

        /// <summary>Gets or sets the IsDefault field. </summary>
        public virtual bool? IsDefault { get; set; }

        /// <summary>Gets or sets the Grid_Name field. </summary>
        public virtual string Grid_Name { get; set; }

        /// <summary>Gets or sets the Grid_Description field. </summary>
        public virtual string Grid_Description { get; set; }

        /// <summary>Gets or sets the IsDefault field. 
        /// if IsSystemDefault is true means the View is from SystemViewSettings
        /// if its is false the View is from UserViewSettings
        /// </summary>
        public bool IsSystemDefault { get; set; }

        /// <summary>Gets or sets the OrganizationUnitId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
