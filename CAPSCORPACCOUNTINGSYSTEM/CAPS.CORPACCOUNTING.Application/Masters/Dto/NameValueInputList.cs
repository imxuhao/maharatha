using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class NameValueInputList : IInputDto
    {
        public List<NameValueDto> NameValueList { get; set; }

        public long? OrganizationUnitId { get; set; }

        public string Type { get; set; }
    }
}
