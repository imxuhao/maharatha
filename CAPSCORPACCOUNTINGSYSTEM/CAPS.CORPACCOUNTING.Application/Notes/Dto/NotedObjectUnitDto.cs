using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Common;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Notes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Notes.Dto
{
    /// <summary>
    /// 
    /// </summary>
    [AutoMapFrom(typeof(NotedObjectUnit))]
    public class NotedObjectUnitDto
    {
        /// <summary>Gets or sets the NotedObjectId field. </summary>
        public long NotedObjectId { get; set; }

        /// <summary>Gets or sets the TypeOfObjectId field. </summary>
        public virtual TypeofObject TypeOfObjectId { get; set; }

        /// <summary>Gets or sets the ObjectId field. </summary>
        public virtual long ObjectId { get; set; }

        /// <summary>Gets or sets the Notes field. </summary>
        public virtual string Notes { get; set; }

        /// <summary>Gets or sets the IsSharedUpdate field. </summary>
        public virtual bool IsSharedUpdate { get; set; }

        /// <summary>Gets or sets the CreatedUser field. </summary>
        public string CreatedUser { get; set; }

        /// <summary>Gets or sets the CreateDateTime field. </summary>
        public DateTime CreateDateTime { get; set; }


    }
}
