using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;

namespace CAPS.CORPACCOUNTING.Masters
{
    public interface IAccountUnitAppService:IApplicationService
    {
        Task<ListResultOutput<AccountUnitDto>> GetAccountUnits();

        Task<AccountUnitDto> CreateAccountUnit(CreateAccountUnitInput input);

        Task<AccountUnitDto> UpdateAccountUnit(UpdateAccountUnitInput input);
        Task<ListResultOutput<AccountUnitDto>> GetAccountUnitsByCoaId(IdInput coaId);
        Task DeleteAccountUnit(IdInput<long> input);
    }
}
