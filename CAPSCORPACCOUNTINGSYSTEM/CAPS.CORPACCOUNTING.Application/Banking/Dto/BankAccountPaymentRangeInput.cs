using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Runtime.Validation;

namespace CAPS.CORPACCOUNTING.Banking.Dto
{
    [AutoMapTo(typeof(BankAccountPaymentRangeUnit))]
    public class BankAccountPaymentRangeInput : IInputDto, ICustomValidate
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

        public void AddValidationErrors(List<ValidationResult> results)
        {
            if (StartingPaymentNumber > EndingPaymentNumber)
            {
                results.Add(new ValidationResult("StartingCheck# must be lessthan EndingCheck#"));
            }
        }
    }
}
