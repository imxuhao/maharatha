using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class GetUserViewSettingsUnitDto:IInputDto
    {
        /// <summary>Gets or sets the UserId field. </summary>
        public long UserId { get; set; }

        /// <summary>Gets or sets the GridId field. </summary>
        public long GridId { get; set; }

    }
}
