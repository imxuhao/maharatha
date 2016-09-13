using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Security.Dto
{
   
    public class ProjectAccessListInputUnit
    {
      
        /// <summary>Gets or sets the JobId field. </summary>
        public int JobId { get; set; }


        /// <summary>Gets or sets the JobNumber field. </summary>
        public string JobNumber { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
        public string Caption { get; set; }

        /// <summary>Gets or sets the UserId field. </summary>
        public long UserId { get; set; }

        /// <summary>Gets or sets the OrganizationUnitId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
