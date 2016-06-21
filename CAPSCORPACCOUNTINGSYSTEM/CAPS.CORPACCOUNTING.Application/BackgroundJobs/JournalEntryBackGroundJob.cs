using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.BackgroundJobs;
using Abp.Dependency;
using AutoMapper;
using CAPS.CORPACCOUNTING.Journals;
using CAPS.CORPACCOUNTING.Journals.dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using CAPS.CORPACCOUNTING.BackgroundJobs.Dto;

namespace CAPS.CORPACCOUNTING.BackgroundJobs
{
    /// <summary>
    /// You can pass anything instead of long. You pass objects as well like DTO where you can store and 
    /// use it to generate report
    /// </summary>
    /// 
    public class JournalEntryBackGroundJob : BackgroundJob<BackgroundJobInput<long>>, ITransientDependency
    {
        private readonly JournalEntryDocumentUnitManager _journalEntryDocumentUnitManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="journalEntryDocumentUnitManager"></param>
        public JournalEntryBackGroundJob(JournalEntryDocumentUnitManager journalEntryDocumentUnitManager)
        {
            _journalEntryDocumentUnitManager = journalEntryDocumentUnitManager;
        }


        /// <summary>
        /// Executes the job with the <see cref="!:args"/>.
        /// </summary>
        /// <param name="input"></param>
        public override async void Execute(BackgroundJobInput<long> input)
        {
            await _journalEntryDocumentUnitManager.CreateRecurringAsync(input.Id, input.tenantId);
        }
    }
}
