using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.BackgroundJobs.Dto
{
    public class CronExpressionDto : IOutputDto
    {
        public string CronExpression { get; set; }
    }
}
