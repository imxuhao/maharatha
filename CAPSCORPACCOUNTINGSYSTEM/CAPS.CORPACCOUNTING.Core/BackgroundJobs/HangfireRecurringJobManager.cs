using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.BackgroundJobs;
using Abp.Hangfire.Configuration;
using Abp.Threading.BackgroundWorkers;
using Hangfire;
using Hangfire.Common;

namespace CAPS.CORPACCOUNTING.BackgroundJobs
{
    public class HangfireRecurringJobManager : BackgroundWorkerBase, IRecurringJobManager
    {
        private readonly IBackgroundJobConfiguration _backgroundJobConfiguration;
        private readonly IAbpHangfireConfiguration _hangfireConfiguration;

        public HangfireRecurringJobManager(IAbpHangfireConfiguration hangfireConfiguration, IBackgroundJobConfiguration backgroundJobConfiguration)
        {
            this._hangfireConfiguration = hangfireConfiguration;
            this._backgroundJobConfiguration = backgroundJobConfiguration;
        }

        public override void Start()
        {
            base.Start();

            if (_hangfireConfiguration.Server == null && _backgroundJobConfiguration.IsJobExecutionEnabled)
            {
                _hangfireConfiguration.Server = new BackgroundJobServer();
            }
        }

        public override void WaitToStop()
        {
            if (_hangfireConfiguration.Server != null)
            {
                try
                {
                    _hangfireConfiguration.Server.Dispose();
                }
                catch (Exception ex)
                {
                    Logger.Warn(ex.ToString(), ex);
                }
            }

            base.WaitToStop();
        }

        public Task AddOrUpdateAsync<TJob, TArgs>(string jobId, TArgs args, string cronExpressions, BackgroundJobPriority priority = BackgroundJobPriority.Normal)
            where TJob : IBackgroundJob<TArgs>
        {
            
            RecurringJob.AddOrUpdate<TJob>(jobId,job => job.Execute(args), cronExpressions);
            Logger.Info("jobId :" + jobId + "is Created.");
            return Task.FromResult(0);
        }

        public void DeleteJob(string jobId)
        {
            RecurringJob.RemoveIfExists(jobId);
            Logger.Info("jobId :" + jobId + "is deleted.");
        }
    }
}
