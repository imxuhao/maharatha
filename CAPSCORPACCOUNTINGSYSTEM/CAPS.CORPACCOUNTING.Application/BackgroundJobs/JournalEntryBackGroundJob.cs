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

namespace CAPS.CORPACCOUNTING.BackgroundJobs
{
   /// <summary>
   /// You can pass anything instead of long. You pass objects as well like DTO where you can store and 
   /// use it to generate report
   /// </summary>
   /// 
   public class JournalEntryBackGroundJob : BackgroundJob<long>, ITransientDependency
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
      
       /// <param name="journalId"></param>
       public override async void Execute(long journalId)
       {

            await _journalEntryDocumentUnitManager.CreateRecurringAsync(journalId);


       }
    }
}
