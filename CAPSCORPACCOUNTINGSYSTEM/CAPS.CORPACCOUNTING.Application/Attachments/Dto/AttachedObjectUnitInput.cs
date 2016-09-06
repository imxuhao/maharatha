using CAPS.CORPACCOUNTING.Common;
using CAPS.CORPACCOUNTING.Masters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Attachments.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class AttachedObjectUnitInput
    {
        /// <summary>Gets or sets the AttachedObject </summary>
        public List<CreateAttachedObjectInputUnit> CreateAttachedObjectUnit { get; set; }
    }
}
