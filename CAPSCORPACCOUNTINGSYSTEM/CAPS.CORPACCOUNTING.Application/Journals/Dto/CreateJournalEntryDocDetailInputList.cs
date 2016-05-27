using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Journals.dto;
using System.Collections.Generic;

namespace CAPS.CORPACCOUNTING.Journals.Dto
{
    public class CreateJournalEntryDocDetailInputList  : IInputDto
    {
     public   List<CreateJournalEntryDocDetailInputUnit> CreateJournalEntryDocDetailList { get; set; }
    }
}
