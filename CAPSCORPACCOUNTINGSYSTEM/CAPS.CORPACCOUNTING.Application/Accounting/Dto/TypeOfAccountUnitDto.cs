using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Accounting.Dto
{

    [AutoMapFrom(typeof(TypeOfAccountUnit))]
    public class TypeOfAccountUnitDto
    {
        /// <summary>Gets or sets the TypeOfAccountID field</summary>
        public int TypeOfAccountId { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
       
        public  string Description { get; set; }

        /// <summary>Gets or sets the Caption field. </summary>
       
        public  string Caption { get; set; }

        /// <summary>Gets or sets the DisplaySequence field. </summary>
        public  short? DisplaySequence { get; set; }

        /// <summary>Gets or sets the Notes field. </summary>
        public  string Notes { get; set; }

        /// <summary>Gets or sets the TypeOfAccountClassificationID field. </summary>
        public  short? TypeOfAccountClassificationId { get; set; }

        /// <summary>Gets or sets the TypeOfAccountClassificationName field. </summary>
        public  string TypeOfAccountClassificationDesc { get; set; }

        /// <summary>Gets or sets the IsCurrencyCodeRequired field. </summary>
        public  bool IsCurrencyCodeRequired { get; set; }

        /// <summary>Gets or sets the IsPaymentType field. </summary>
        public  bool IsPaymentType { get; set; }

        /// <summary>Gets or sets the IsPaymentType field. </summary>
        public bool AllowDelete { get; set; }

        /// <summary>Gets or sets the IsPaymentType field. </summary>
        public bool AllowEdit { get; set; }

    }
}
