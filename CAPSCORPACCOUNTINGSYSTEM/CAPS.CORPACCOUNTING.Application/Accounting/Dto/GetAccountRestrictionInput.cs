using System;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Helpers;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Accounting.Dto
{
    /// <summary>
    /// InputDto to get SubAccountRestrictionList
    /// </summary>
    public class GetAccountRestrictionInput : IInputDto
    {
        /// <summary>Gets or sets the SubAccountId </summary>
         public long? SubAccountId { get; set; }


        /// <summary>Gets or sets the OrganizationUnitId </summary>
        public long? OrganizationUnitId { get; set; }

        /// <summary>Gets or sets the Filters </summary>
        public List<Filters> Filters { get; set; }
    }
}
