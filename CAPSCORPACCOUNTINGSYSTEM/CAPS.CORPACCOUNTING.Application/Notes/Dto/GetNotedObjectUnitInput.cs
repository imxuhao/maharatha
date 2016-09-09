using CAPS.CORPACCOUNTING.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Notes.Dto
{
    /// <summary>
    /// This is used to GET the Noted Object Unit 
    /// </summary>
   public class GetNotedObjectUnitInput
    {
        /// <summary>Gets or sets the TypeOfObjectId field. </summary>
        public virtual TypeofObject TypeOfObjectId { get; set; }

        /// <summary>Gets or sets the ObjectId field. </summary>
        public virtual long ObjectId { get; set; }
    }
}
