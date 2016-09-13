using Abp.Application.Services;
using CAPS.CORPACCOUNTING.BackgroundJobs.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.BackgroundJobs
{
    public interface ICronExpressionAppService : IApplicationService
    {
        /// <summary>
        /// Get Cron Expression
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
       CronExpressionDto GetCronExpression(CronExpressionInput input);
    }
}
