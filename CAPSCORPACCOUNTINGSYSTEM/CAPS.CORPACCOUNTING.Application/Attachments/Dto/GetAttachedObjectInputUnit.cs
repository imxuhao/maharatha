using CAPS.CORPACCOUNTING.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Attachments.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAttachedObjectInputUnit
    {
        /// <summary>Gets or sets the TypeOfObjectID field. </summary>
        public virtual TypeofObject TypeOfObjectId { get; set; }

        /// <summary>Gets or sets the ObjectID field. </summary>
        public virtual long ObjectId { get; set; }
       
    }
}
