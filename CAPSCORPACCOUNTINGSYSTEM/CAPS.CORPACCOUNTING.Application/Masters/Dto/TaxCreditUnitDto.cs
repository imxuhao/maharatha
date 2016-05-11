using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapFrom(typeof(TaxCreditUnit))]
    public class TaxCreditUnitDto : IOutputDto
    {
        /// <summary>Gets or sets the TaxCreditId field. </summary>
        public virtual int TaxCreditId { get; set; }

        /// <summary>Gets or sets the Number field. </summary>
        public virtual string Number { get; set; }

        /// <summary>Gets or sets the Description field. </summary>
        public virtual string Description { get; set; }

        /// <summary>Gets or sets the IsActive field. </summary>
        public bool IsActive { get; set; }

        /// <summary>Gets or sets the IsDefault field. </summary>
        public bool IsDefault { get; set; }

        /// <summary>Gets or sets the CompanyId field. </summary>
        public long? OrganizationUnitId { get; set; }

    }
}
