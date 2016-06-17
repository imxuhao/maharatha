using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.BackgroundJobs;
using Abp.Dependency;
using CAPS.CORPACCOUNTING.Journals;
using CAPS.CORPACCOUNTING.Journals.dto;

namespace CAPS.CORPACCOUNTING.BackgroundJobs
{
   public class JournalEntryBackGroundJob : BackgroundJob<JournalEntryDocumentInputUnit>, ITransientDependency
    {
        private  readonly  IJournalEntryDocumentAppService _journalEntryDocumentAppService;
      

        public JournalEntryBackGroundJob(IJournalEntryDocumentAppService journalEntryDocumentAppService)
       {
           _journalEntryDocumentAppService = journalEntryDocumentAppService;
       }


       public override  void Execute(JournalEntryDocumentInputUnit args)
        {
             _journalEntryDocumentAppService.CreateJournalEntryDocumentUnit(args);
           
        }
    }
}
