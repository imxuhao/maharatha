using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Helpers
{
   public class ListInputDto: IInputDto
    {
      public List<long> List { get; set; }
    }
}
