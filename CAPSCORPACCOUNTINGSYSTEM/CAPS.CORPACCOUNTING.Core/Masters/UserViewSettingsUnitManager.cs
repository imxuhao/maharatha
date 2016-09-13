using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Zero;
using System.Threading.Tasks;

namespace CAPS.CORPACCOUNTING.Masters
{
    public class UserViewSettingsUnitManager : DomainService
    {
        protected IRepository<UserViewSettingsUnit, int> _userViewSettingsUnitRepository { get; }

        public UserViewSettingsUnitManager(IRepository<UserViewSettingsUnit, int> userViewSettingsUnitRepository)
        {
            _userViewSettingsUnitRepository = userViewSettingsUnitRepository;
            LocalizationSourceName = AbpZeroConsts.LocalizationSourceName;
        }

        public virtual async Task CreateAsync(UserViewSettingsUnit userViewSettingsUnit)
        {
            await _userViewSettingsUnitRepository.InsertAsync(userViewSettingsUnit);
        }

        public virtual async Task UpdateAsync(UserViewSettingsUnit userViewSettingsUnit)
        {
            await _userViewSettingsUnitRepository.UpdateAsync(userViewSettingsUnit);
        }

        public virtual async Task DeleteAsync(IdInput input)
        {
            await _userViewSettingsUnitRepository.DeleteAsync(p => p.Id == input.Id);
        }

    }
}
