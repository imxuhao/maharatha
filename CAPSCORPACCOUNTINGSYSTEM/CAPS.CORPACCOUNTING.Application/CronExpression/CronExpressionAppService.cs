using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CAPS.CORPACCOUNTING.BackgroundJobs.Dto;

namespace CAPS.CORPACCOUNTING.BackgroundJobs
{
    public class CronExpressionAppService : CORPACCOUNTINGServiceBase, ICronExpressionAppService
    {
        public CronExpressionDto GetCronExpression(CronExpressionInput input)
        {
            var cronExpressionDto = new CronExpressionDto();
            if (input.StartDate.HasValue)
            {
                // Format Holds for Daily and Weekly.
                if(input.ScheduleType == 2 || input.ScheduleType == 3 || input.ScheduleType == 4)
                {
                    cronExpressionDto.CronExpression = string.Format("{0} {1} {2} {3} {4}",
                                              input.StartDate.Value.UtcDateTime.ToString("mm"),
                                              (input.ScheduleType == 2 && input.Repeat.HasValue && input.Repeat.Value > 0) ? string.Format("*/{0}", input.Repeat.Value) : input.StartDate.Value.UtcDateTime.ToString("HH"),
                                              (input.ScheduleType == 3 && input.Repeat.HasValue) ? string.Format("1/{0}", input.Repeat.Value) : "*",
                                              "*",
                                              (input.Days == null || input.Days.Count == 0) ? "*" : string.Join(",", input.Days));
                } else if(input.ScheduleType == 5 ) // monthly
                {
                    cronExpressionDto.CronExpression = string.Format("{0} {1} {2} {3} {4}",
                                                                 input.StartDate.Value.UtcDateTime.ToString("mm"),
                                                                 (input.ScheduleType == 2 && input.Repeat.HasValue && input.Repeat.Value > 0) ? string.Format("*/{0}", input.Repeat.Value) : input.StartDate.Value.UtcDateTime.ToString("HH"),
                                                                 (input.ScheduleType == 5 && input.DayOfMonth.HasValue) ? string.Format("*/{0}", input.DayOfMonth.Value) : "*",
                                                                 (input.ScheduleType == 5 && input.Repeat.HasValue) ? string.Format("1/{0}", input.Repeat.Value) : "*",
                                                                 (input.DayOfWeek == null || input.DayOfWeek == "") ? "*" : input.DayOfWeek);
                }
                else if (input.ScheduleType == 6) // yearly
                {
                    cronExpressionDto.CronExpression = string.Format("{0} {1} {2} {3} {4}",
                                                                 input.StartDate.Value.UtcDateTime.ToString("mm"),
                                                                 (input.ScheduleType == 2 && input.Repeat.HasValue && input.Repeat.Value > 0) ? string.Format("*/{0}", input.Repeat.Value) : input.StartDate.Value.UtcDateTime.ToString("HH"),
                                                                 (input.ScheduleType == 6 && input.DayOfMonth.HasValue) ? string.Format("*/{0}", input.DayOfMonth.Value) : "*",
                                                                 (input.ScheduleType == 6 && input.Month.HasValue) ? string.Format("{0}", input.Month.Value) : "*",
                                                                 (input.DayOfWeek == null || input.DayOfWeek == "") ? "*" : input.DayOfWeek);
                }


            }
            return cronExpressionDto;
        }

    }
}
