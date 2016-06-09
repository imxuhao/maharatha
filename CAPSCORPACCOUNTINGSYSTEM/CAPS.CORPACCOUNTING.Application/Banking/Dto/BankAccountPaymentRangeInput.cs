using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.Banking.Dto
{
    [AutoMapTo(typeof(BankAccountPaymentRangeUnit))]
    public class BankAccountPaymentRangeInput : IInputDto
    {
        /// <summary>Gets or Sets BankAccountPaymentRangeId. </summary>
        public int BankAccountPaymentRangeId { get; set; }

        /// <summary>Gets or Sets StartingPaymentNumber. </summary>
        [Range(1, Int32.MaxValue)]
        public int StartingPaymentNumber { get; set; }
        /// <summary>Gets or Sets EndingPaymentNumber. </summary>

        [Range(1, Int32.MaxValue)]
        public int EndingPaymentNumber { get; set; }

        /// <summary>Gets or sets the BankAccountId field. </summary>
      
        public virtual long BankAccountId { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }
    }
}
