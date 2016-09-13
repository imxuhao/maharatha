using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Security.Dto
{
    /// <summary>
    /// 
    /// </summary>
  
    public class ProjectAccessListUnitDto
    {
        /// <summary>Gets or sets the ProjectAccessId field. </summary>
        public long ProjectAccessId { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        public int JobId { get; set; }

        /// <summary>Gets or sets the Caption</summary>
        public string Caption { get; set; }

        /// <summary>Gets or sets the JobNumber</summary>
        public string JobNumber { get; set; }

        /// <summary>Gets or sets the OrganizationUnitId </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
