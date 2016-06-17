using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.BackgroundJobs;
using Abp.Threading.BackgroundWorkers;

namespace CAPS.CORPACCOUNTING.BackgroundJobs
{
    public interface IRecurringJobManager : IBackgroundWorker
    {
        Task AddOrUpdateAsync<TJob, TArgs>(string jobId,TArgs args, string cronExpressions, BackgroundJobPriority priority = BackgroundJobPriority.Normal)
            where TJob : IBackgroundJob<TArgs>;
    }
}
