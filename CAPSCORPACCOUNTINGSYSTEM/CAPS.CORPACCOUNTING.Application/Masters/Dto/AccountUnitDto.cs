using Abp.Application.Services.Dto;
using Abp.AutoMapper;


namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapFrom(typeof(AccountUnit))]
    public class AccountUnitDto :AuditedEntityDto<long>
    {

        public long? ParentId { get; set; }

        public string AccountNumber { get; set; }

        


    }
}
