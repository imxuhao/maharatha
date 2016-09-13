using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.CreditCard.Dto
{
    public class CreditCardStatementDto
    {
        /// <summary>Get Sets the DocumentDate field.</summary>        
        public  DateTime? DocumentDate { get; set; }

        /// <summary>Get Sets the TransactionDate field.</summary>      
        public  DateTime TransactionDate { get; set; }

        /// <summary>Gets or sets the ControllingBankAccountId field. </summary>
        public  long? ControllingBankAccountId { get; set; }
       
        // <summary>Get Sets the IsPosted field.</summary>
        public  bool IsPosted { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        public virtual string Description { get; set; }

        /// <summary>Get Sets the ControlTotal field.</summary>
        public virtual decimal? ControlTotal { get; set; }


    }
}
