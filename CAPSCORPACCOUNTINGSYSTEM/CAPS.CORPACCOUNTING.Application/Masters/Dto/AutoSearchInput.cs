using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    public class AutoSearchInput: IInputDto
    {
        public long? Id { get; set; }

        public string Query { get; set; }
    }
}
