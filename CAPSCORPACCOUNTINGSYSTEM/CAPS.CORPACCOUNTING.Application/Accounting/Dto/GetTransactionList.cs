

using CAPS.CORPACCOUNTING.GenericSearch.Dto;

namespace CAPS.CORPACCOUNTING.Journals.Dto
{
    public class GetTransactionList:SearchInputDto
    {
        public  long AccountingDocumentId { get; set; }
    }
}
