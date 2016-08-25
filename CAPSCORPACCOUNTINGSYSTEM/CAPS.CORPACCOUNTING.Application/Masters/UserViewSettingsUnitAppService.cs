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

namespace CAPS.CORPACCOUNTING.Masters
{
    public class UserViewSettingsUnitAppService : CORPACCOUNTINGAppServiceBase, IUserViewSettingsUnitAppService
    {

        private readonly UserViewSettingsUnitManager _userViewSettingsUnitManager;
        private readonly IRepository<UserViewSettingsUnit, int> _userViewSettingsUnitRepository;
        private readonly IRepository<GridListUnit, int> _gridListUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<SystemViewSettingsUnit> _systemViewSettingsUnitRepository;
        public UserViewSettingsUnitAppService(UserViewSettingsUnitManager userViewSettingsUnitManager,
            IRepository<UserViewSettingsUnit, int> userViewSettingsUnitRepository,
            IRepository<GridListUnit, int> gridListUnitRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<SystemViewSettingsUnit> systemViewSettingsUnitRepository)
        {
            _userViewSettingsUnitManager = userViewSettingsUnitManager;
            _userViewSettingsUnitRepository = userViewSettingsUnitRepository;
            _gridListUnitRepository = gridListUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _systemViewSettingsUnitRepository = systemViewSettingsUnitRepository;

        }


        /// <summary>
        /// Create the UserViewSettings.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<UserViewSettingsUnitDto> CreateUserViewSettingsUnit(CreateUserViewSettingsUnitInput input)
        {
            var tenantId = AbpSession.TenantId;
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                if (input.IsDefault.HasValue && input.IsDefault.Value)
                {
                    var res =
                        await
                            _userViewSettingsUnitRepository.FirstOrDefaultAsync(
                                p => p.IsDefault == true && p.ViewId == input.ViewId && p.UserId == input.UserId);
                    if (!ReferenceEquals(res, null))
                    {
                        res.IsDefault = false;
                        await _userViewSettingsUnitManager.UpdateAsync(res);
                        await CurrentUnitOfWork.SaveChangesAsync();
                    }
                }
                var UserViewSettings = input.MapTo<UserViewSettingsUnit>();
                UserViewSettings.TenantId = tenantId;
                await _userViewSettingsUnitManager.CreateAsync(UserViewSettings);
                await CurrentUnitOfWork.SaveChangesAsync();
                return UserViewSettings.MapTo<UserViewSettingsUnitDto>();
            }
        }

        /// <summary>
        ///  Update the UserViewSettings based on UserViewId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task<UserViewSettingsUnitDto> UpdateUserViewSettingsUnit(UpdateUserViewSettingsUnitInput input)
        {
            var tenantId = AbpSession.TenantId;
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                var UserViewSettings = await _userViewSettingsUnitRepository.GetAsync(input.UserViewId);
                if (!UserViewSettings.IsDefault.Value && input.IsDefault.Value)
                {
                    var res =
                        await
                            _userViewSettingsUnitRepository.FirstOrDefaultAsync(
                                p => p.IsDefault == true && p.ViewId == input.ViewId && p.UserId == input.UserId);
                    if (!ReferenceEquals(res, null))
                    {
                        res.IsDefault = false;
                        await _userViewSettingsUnitManager.UpdateAsync(res);
                        await CurrentUnitOfWork.SaveChangesAsync();
                    }
                }
                //Mapper.CreateMap<UpdateUserViewSettingsUnitInput, UserViewSettingsUnit>()
                //    .ForMember(u => u.Id, ap => ap.MapFrom(src => src.UserViewId));
                //var config = new MapperConfiguration(cfg => {
                //    cfg.CreateMap<UpdateUserViewSettingsUnitInput, UserViewSettingsUnit>().ForMember(u => u.Id, ap => ap.MapFrom(src => src.UserViewId));
                //});
                Mapper.Map(input, UserViewSettings);
                await _userViewSettingsUnitManager.UpdateAsync(UserViewSettings);
                await CurrentUnitOfWork.SaveChangesAsync();
                return UserViewSettings.MapTo<UserViewSettingsUnitDto>();
            }
        }

        /// <summary>
        ///  delete the UserViewSettings based on UserViewId.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task DeleteUserViewSettingsUnit(IdInput input)
        {
            var tenantId = AbpSession.TenantId;
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                await _userViewSettingsUnitManager.DeleteAsync(input);
            }
        }


        /// <summary>
        /// Get the list of all UserViewSettings by User
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task<ListResultDto<UserViewSettingsUnitDto>> GetUserViewSettingsUnitsByUserId(SearchInputDto input)
        {
            var tenantId = AbpSession.TenantId;
            using (_unitOfWorkManager.Current.SetTenantId(tenantId))
            {
                // _unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MustHaveTenant);
                var userViewSettings = CreateUserViewSettingsQuery(input);
                var results = await userViewSettings.ToListAsync();
                return new ListResultDto<UserViewSettingsUnitDto>(results);
            }
        }

        private IQueryable<UserViewSettingsUnitDto> CreateUserViewSettingsQuery(SearchInputDto input)
        {
            int viewId = 0;
            IQueryable<UserViewSettingsUnitDto> query = (from settings in _userViewSettingsUnitRepository.GetAll()
                                                         join gridList in _gridListUnitRepository.GetAll()
                                                         on settings.ViewId equals gridList.Id into gridsetting
                                                         from grdsettings in gridsetting.DefaultIfEmpty()
                                                         where settings.OrganizationUnitId==input.OrganizationUnitId
                                                         select new UserViewSettingsUnitDto
                                                         {
                                                             UserViewId = settings.Id,
                                                             ViewId = settings.ViewId,
                                                             UserId = settings.UserId,
                                                             IsDefault = settings.IsDefault,
                                                             ViewName = settings.ViewName,
                                                             ViewSettings = settings.ViewSettings,
                                                             Grid_Name = grdsettings.Name,
                                                             Grid_Description = grdsettings.Description,
                                                             IsSystemDefault = false
                                                         });

            if (!ReferenceEquals(input.Filters, null))
            {
                SearchTypes mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = Helper.CreateFilters(query, mapSearchFilters);
            }

            foreach (Filters filter in input.Filters)
            {
                if (filter.Property == "viewId")
                {
                    viewId = Convert.ToInt32(filter.SearchTerm);
                    break;
                }
            }
            query = query.Union(from settings in _systemViewSettingsUnitRepository.GetAll()
                          .Where(p => p.ViewId == viewId)
                                join gridList in _gridListUnitRepository.GetAll()
                                    on settings.ViewId equals gridList.Id into gridsetting
                                from grdsettings in gridsetting.DefaultIfEmpty()
                                select new UserViewSettingsUnitDto
                                {
                                    UserViewId = settings.Id,
                                    ViewId = settings.ViewId,
                                    UserId = 0,
                                    IsDefault = settings.IsDefault,
                                    ViewName = settings.ViewName,
                                    ViewSettings = settings.ViewSettings,
                                    Grid_Name = grdsettings.Name,
                                    Grid_Description = grdsettings.Description,
                                    IsSystemDefault = true
                                });
            return query;
        }

    }
}
