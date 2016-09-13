using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using CAPS.CORPACCOUNTING.Banking;

namespace CAPS.CORPACCOUNTING.Accounting.Dto
{
    [AutoMapFrom(typeof(SubAccountUnit))]
    public class SubAccountUnitDto : IOutputDto
    {
        public  long SubAccountId { get; set; }
        public string Description { get; set; }
        public string Caption { get; set; }
        public short? DisplaySequence { get; set; }
        public string SubAccountNumber { get; set; }
        public int? AccountingLayoutItemId { get; set; }
        public string GroupCopyLabel { get; set; }
        public bool IsAccountSpecific { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsBudgetInclusive { get; set; }
        public bool IsCorporateSubAccount { get; set; }
        public bool IsProjectSubAccount { get; set; }
        public int EntityId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
        public TypeOfInactiveStatus? TypeOfInactiveStatusId { get; set; }
        public string TypeOfInactiveStatus { get; set; }
        public bool? IsEnterable { get; set; }
        public long? SearchOrder { get; set; }
        public string SearchNo { get; set; }
        public virtual long? OrganizationUnitId { get; set; }
        public TypeofSubAccount TypeofSubAccountId { get; set; }
        public string TypeofSubAccount { get; set; }
    }
}
