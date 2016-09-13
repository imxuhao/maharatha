using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
   public class VendorSearchInput
    {
        /// <summary>
        /// dropdown keyword search
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        ///set classification id to search 
        /// </summary>
        public TypeofVendor TypeofVendorId { get; set; }
    }
}
