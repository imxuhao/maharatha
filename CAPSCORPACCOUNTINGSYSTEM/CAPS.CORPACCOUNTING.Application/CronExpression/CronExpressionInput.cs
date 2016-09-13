using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.BackgroundJobs.Dto
{
    public class CronExpressionInput 
    {
        public int ScheduleType { get; set; }
        public string TimeZone { get; set; }
        public TimeSpan TimeSpan
        {
            get
            {
                if (string.IsNullOrEmpty(TimeZone) || TimeZone.IndexOf(":") == -1)
                {
                    return new TimeSpan(0, 0, 0);
                }
                else
                {
                    return new TimeSpan(int.Parse(TimeZone.Split(new char[] { ':' })[0]), int.Parse(TimeZone.Split(new char[] { ':' })[1]), 0);
                }
            }
        }
        private DateTimeOffset? _startDate;
        public DateTimeOffset? StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                if (value.HasValue)
                {
                    _startDate = new DateTimeOffset(value.Value.DateTime, TimeSpan);
                }
                else
                {
                    _startDate = new DateTimeOffset();
                }
            }
        }
        private DateTimeOffset? _endDate;
        public DateTimeOffset? EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                if (value.HasValue)
                {
                    if (StartDate.HasValue && value.Value <= StartDate.Value)
                    {
                        _endDate = ScheduleType == 2
                            ? new DateTimeOffset(StartDate.Value.DateTime.AddMinutes(5), TimeSpan) : new DateTimeOffset(StartDate.Value.DateTime.AddDays(1), TimeSpan);
                    }
                    else
                    {
                        _endDate = new DateTimeOffset(value.Value.DateTime, TimeSpan);
                    }
                }
            }
        }
        private int? _repeat;
        public int? Repeat
        {
            get
            {
                return _repeat;
            }
            set
            {
                _repeat = value;
                if (!_repeat.HasValue)
                    _repeat = 1;
            }
        }
        public int? JobEndType { get; set; }
        private int? _occurrence;
        public int? Occurrence
        {
            get
            {
                return _occurrence;
            }
            set
            {
                _occurrence = value;
                if (_occurrence.HasValue && (JobEndType.HasValue && JobEndType.Value == 2))
                {
                    switch (ScheduleType)
                    {
                        case 2:
                            // Hourly Schedule
                            EndDate = StartDate.Value.AddHours((Int32)(Repeat * _occurrence.Value));
                            EndDate = new DateTime(EndDate.Value.Year, EndDate.Value.Month, EndDate.Value.Day, EndDate.Value.Hour, 00, 00);
                            break;
                        case 3:
                            // Daily Schedule
                            EndDate = StartDate.Value.AddDays((Int32)(Repeat * _occurrence.Value));
                            EndDate = new DateTime(EndDate.Value.Year, EndDate.Value.Month, EndDate.Value.Day, EndDate.Value.Hour, 00, 00);
                            break;
                        case 4:
                            // Weekly Schedule
                            EndDate = StartDate.Value.AddDays((Int32)(_occurrence.Value * 7));
                            EndDate = new DateTime(EndDate.Value.Year, EndDate.Value.Month, EndDate.Value.Day, EndDate.Value.Hour, 00, 00);
                            break;
                        case 5:
                            // Monthly Schedule
                            EndDate = StartDate.Value.AddMonths((Int32)(Repeat * _occurrence.Value));
                            EndDate = new DateTime(EndDate.Value.Year, EndDate.Value.Month, EndDate.Value.Day, EndDate.Value.Hour, 00, 00);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public List<int> Days { get; set; }

        public int? DayOfMonth { get; set; }
        public string DayOfWeek { get; set; }

        public int? Month { get; set; }

    }
}
