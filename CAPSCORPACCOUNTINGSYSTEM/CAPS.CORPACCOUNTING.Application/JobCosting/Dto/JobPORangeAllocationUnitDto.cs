using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.JobCosting.Dto
{
    [AutoMapFrom(typeof(JobPORangeAllocationUnit))]
    public class JobPORangeAllocationUnitDto : IOutputDto
    {
        /// <summary>Overriding the ID column with PORangeAllocationId</summary>
        public int PORangeAllocationId { get; set; }

        /// <summary>Gets or sets the JobId field. </summary>
        public virtual int JobId { get; set; }

        /// <summary>Gets or sets the poRangeStartNumber field. </summary>
        public virtual long PoRangeStartNumber { get; set; }

        /// <summary>Gets or sets the poRangeEndNumber field. </summary>
        public virtual long PoRangeEndNumber { get; set; }

        /// <summary>Gets or sets the Company field. </summary>
        public virtual long? OrganizationUnitId { get; set; }
    }
}
