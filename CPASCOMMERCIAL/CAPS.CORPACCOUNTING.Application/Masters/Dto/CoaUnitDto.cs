using Abp.Application.Services.Dto;
using  Abp.AutoMapper;

namespace CAPS.CORPACCOUNTING.Masters.Dto
{
    [AutoMapFrom(typeof(CoaUnit))]  
    public class CoaUnitDto : AuditedEntityDto
    {
        public int CoaId { get; set; }
    }
}
