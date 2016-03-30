using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using AutoMapper;
using System.Data.Entity;
using Abp.Domain.Uow;

namespace CAPS.CORPACCOUNTING.Masters
{
    class UserViewSettingsUnitAppService : CORPACCOUNTINGServiceBase, IUserViewSettingsUnitAppService
    {

        private readonly UserViewSettingsUnitManager _userViewSettingsUnitManager;
        private readonly IRepository<UserViewSettingsUnit, int> _userViewSettingsUnitRepository;
        private readonly IRepository<GridListUnit, int> _gridListUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public UserViewSettingsUnitAppService(UserViewSettingsUnitManager userViewSettingsUnitManager,
            IRepository<UserViewSettingsUnit, int> userViewSettingsUnitRepository,
            IRepository<GridListUnit, int> gridListUnitRepository,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _userViewSettingsUnitManager = userViewSettingsUnitManager;
            _userViewSettingsUnitRepository = userViewSettingsUnitRepository;
            _gridListUnitRepository = gridListUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }


        /// <summary>
        /// Create the UserViewSettings.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<UserViewSettingsUnitDto> CreateUserViewSettingsUnit(CreateUserViewSettingsUnitInput input)
        {
            var UserViewSettings = input.MapTo<UserViewSettingsUnit>();
            await _userViewSettingsUnitManager.CreateAsync(UserViewSettings);
            await CurrentUnitOfWork.SaveChangesAsync();
            return UserViewSettings.MapTo<UserViewSettingsUnitDto>();
        }

        /// <summary>
        ///  Update the UserViewSettings based on UserViewId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<UserViewSettingsUnitDto> UpdateUserViewSettingsUnit(UpdateUserViewSettingsUnitInput input)
        {
            var UserViewSettings = await _userViewSettingsUnitRepository.GetAsync(input.UserViewId);
            Mapper.CreateMap<UpdateUserViewSettingsUnitInput, UserViewSettingsUnit>()
                          .ForMember(u => u.Id, ap => ap.MapFrom(src => src.UserViewId));
            Mapper.Map(input, UserViewSettings);
            await _userViewSettingsUnitManager.UpdateAsync(UserViewSettings);
            await CurrentUnitOfWork.SaveChangesAsync();
            return UserViewSettings.MapTo<UserViewSettingsUnitDto>();
        }

        /// <summary>
        ///  delete the UserViewSettings based on UserViewId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteUserViewSettingsUnit(IdInput input)
        {
            await _userViewSettingsUnitManager.DeleteAsync(input);
        }


        /// <summary>
        /// Get the list of all UserViewSettings by User
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<ListResultDto<UserViewSettingsUnitDto>> GetUserViewSettingsUnitsByUserId(GetUserViewSettingsUnitDto input)
        {
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant);
            var userViewSettings = CreateUserViewSettingsQuery();
            userViewSettings = userViewSettings.Where(u => u.UserId == input.UserId);
           
            if (input.GridId != 0)
                userViewSettings = userViewSettings.Where(u => u.GridId == input.GridId);
            var results = await userViewSettings.ToListAsync();
            return new ListResultDto<UserViewSettingsUnitDto>(results);
        }

        private IQueryable<UserViewSettingsUnitDto> CreateUserViewSettingsQuery()
        {
            return (from settings in _userViewSettingsUnitRepository.GetAll()
                    join gridList in _gridListUnitRepository.GetAll()
                    on settings.GridId equals gridList.Id into gridsetting
                    from grdsettings in gridsetting.DefaultIfEmpty()
                    select new UserViewSettingsUnitDto
                    {
                        UserViewId = settings.Id,
                        GridId = settings.GridId,
                        UserId = settings.UserId,
                        IsDefault = settings.IsDefault,
                        ViewSettings = settings.ViewSettings,
                        Grid_Name = grdsettings.Name,
                        Grid_Description = grdsettings.Description
                    });
        }

    }
}
