using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomAmountAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            decimal amount = Convert.ToDecimal(value);
            return amount != 0;
        }
    }
}
