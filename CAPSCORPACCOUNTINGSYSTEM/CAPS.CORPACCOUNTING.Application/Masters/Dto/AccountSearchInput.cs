using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
   public class AccountSearchInput
    {

        /// <summary>
        /// Set JobId to search
        /// </summary>
        public int JobId { get; set; }

        /// <summary>
        /// dropdown keyword search
        /// </summary>
        public string Query { get; set; }



        /// <summary>
        ///set classification name to search 
        /// </summary>
        public string TypeOfAccount { get; set; }
    }
}
