using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Domain.Repositories;
using Abp.AutoMapper;
using AutoMapper;
using System.Data.Entity;
using Abp.Domain.Uow;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.Sessions;

namespace CAPS.CORPACCOUNTING.Masters
{
    public class UserViewSettingsUnitAppService : CORPACCOUNTINGServiceBase, IUserViewSettingsUnitAppService
    {
        private readonly UserViewSettingsUnitManager _userViewSettingsUnitManager;
        private readonly IRepository<UserViewSettingsUnit, int> _userViewSettingsUnitRepository;
        private readonly IRepository<GridListUnit, int> _gridListUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CustomAppSession _customAppSession;
        private int? TenantId = null;
        public UserViewSettingsUnitAppService(UserViewSettingsUnitManager userViewSettingsUnitManager,
            IRepository<UserViewSettingsUnit, int> userViewSettingsUnitRepository,
            IRepository<GridListUnit, int> gridListUnitRepository,
            IUnitOfWorkManager unitOfWorkManager, CustomAppSession customAppSession)
        {
            _userViewSettingsUnitManager = userViewSettingsUnitManager;
            _userViewSettingsUnitRepository = userViewSettingsUnitRepository;
            _gridListUnitRepository = gridListUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _customAppSession = customAppSession;
            if (!ReferenceEquals(_customAppSession.TenantId, null))
            {
                TenantId = Convert.ToInt32(_customAppSession.TenantId);
            }

        }


        /// <summary>
        /// Create the UserViewSettings.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<UserViewSettingsUnitDto> CreateUserViewSettingsUnit(CreateUserViewSettingsUnitInput input)
        {

            if (input.IsDefault.Value)
            {
                var res = await _userViewSettingsUnitRepository.FirstOrDefaultAsync(p => p.IsDefault == true &&
                                        p.GridId == input.GridId && p.UserId == input.UserId);
                if (!ReferenceEquals(res, null))
                {
                    res.IsDefault = false;
                    await _userViewSettingsUnitManager.UpdateAsync(res);
                    await CurrentUnitOfWork.SaveChangesAsync();
                }
            }
            var UserViewSettings = input.MapTo<UserViewSettingsUnit>();
            UserViewSettings.TenantId = TenantId;
            await _userViewSettingsUnitManager.CreateAsync(UserViewSettings);
            await CurrentUnitOfWork.SaveChangesAsync();
            return UserViewSettings.MapTo<UserViewSettingsUnitDto>();
        }

        /// <summary>
        ///  Update the UserViewSettings based on UserViewId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<UserViewSettingsUnitDto> UpdateUserViewSettingsUnit(UpdateUserViewSettingsUnitInput input)
        {
            var UserViewSettings = await _userViewSettingsUnitRepository.GetAsync(input.UserViewId);
            if (!UserViewSettings.IsDefault.Value && input.IsDefault.Value)
            {
                var res = await _userViewSettingsUnitRepository.FirstOrDefaultAsync(p => p.IsDefault == true
                            && p.GridId == input.GridId && p.UserId == input.UserId);
                if (!ReferenceEquals(res, null))
                {
                    res.IsDefault = false;
                    await _userViewSettingsUnitManager.UpdateAsync(res);
                    await CurrentUnitOfWork.SaveChangesAsync();
                }
            }
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

        public async Task<ListResultDto<UserViewSettingsUnitDto>> GetUserViewSettingsUnitsByUserId(SearchInputDto input)
        {
            _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant);
            var userViewSettings = CreateUserViewSettingsQuery(input);
            using (UnitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                var results = await userViewSettings.ToListAsync();
                return new ListResultDto<UserViewSettingsUnitDto>(results);
            }
        }

        private IQueryable<UserViewSettingsUnitDto> CreateUserViewSettingsQuery(SearchInputDto input)
        {
            //Constructing the query to get default and current user userviewsettings
            int gridid = 0;
            IQueryable<UserViewSettingsUnitDto> query = (from settings in _userViewSettingsUnitRepository.GetAll()
                .Where(p => p.TenantId == TenantId)
                                                         join gridList in _gridListUnitRepository.GetAll()
                                                             on settings.GridId equals gridList.Id into gridsetting
                                                         from grdsettings in gridsetting.DefaultIfEmpty()
                                                         select new UserViewSettingsUnitDto
                                                         {
                                                             UserViewId = settings.Id,
                                                             GridId = settings.GridId,
                                                             UserId = settings.UserId,
                                                             IsDefault = settings.IsDefault,
                                                             ViewSettingName = settings.ViewSettingName,
                                                             ViewSettings = settings.ViewSettings,
                                                             Grid_Name = grdsettings.Name,
                                                             Grid_Description = grdsettings.Description,
                                                             IsSystemDefault = false
                                                         });

            foreach (Filters filter in input.Filters)
            {
                if (filter.Property == "gridId")
                {
                    gridid = Convert.ToInt32(filter.SearchTerm);
                    break;
                }
            }
            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }
            query = query.Union(from settings in _userViewSettingsUnitRepository.GetAll()
                .Where(p => p.TenantId == null && p.GridId == gridid && p.UserId==null)
                                join gridList in _gridListUnitRepository.GetAll()
                                    on settings.GridId equals gridList.Id into gridsetting
                                from grdsettings in gridsetting.DefaultIfEmpty()
                                select new UserViewSettingsUnitDto
                                {
                                    UserViewId = settings.Id,
                                    GridId = settings.GridId,
                                    UserId = settings.UserId,
                                    IsDefault = settings.IsDefault,
                                    ViewSettingName = settings.ViewSettingName,
                                    ViewSettings = settings.ViewSettings,
                                    Grid_Name = grdsettings.Name,
                                    Grid_Description = grdsettings.Description,
                                    IsSystemDefault=true
                                });
            return query;
        }

    }

}

