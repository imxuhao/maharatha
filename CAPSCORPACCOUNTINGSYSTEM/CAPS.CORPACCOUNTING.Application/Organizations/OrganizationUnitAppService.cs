using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Organizations;
using CAPS.CORPACCOUNTING.Authorization;
using CAPS.CORPACCOUNTING.Organizations.Dto;
using Abp.Domain.Uow;
using CAPS.CORPACCOUNTING.Masters;
using System;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Organization;
using CAPS.CORPACCOUNTING.Sessions;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using System.Linq.Dynamic;

namespace CAPS.CORPACCOUNTING.Organizations
{
    [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits)]
    public class OrganizationUnitAppService : CORPACCOUNTINGAppServiceBase, IOrganizationUnitAppService
    {
        private readonly OrganizationExtendedUnitManager _organizationExtendedUnitManager;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<OrganizationExtended, long> _organizationExtendedUnitRepository;
        private readonly OrganizationUnitManager _organizationUnitManager;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CustomAppSession _customAppSession;
        private readonly IRepository<ConnectionStringUnit> _connectionStringRepository;
        public OrganizationUnitAppService(
            OrganizationExtendedUnitManager organizationExtendedUnitManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IUnitOfWorkManager unitOfWorkManager,
            CustomAppSession customAppSession,
            IRepository<OrganizationExtended, long> organizationExtendedUnitRepository,
          IRepository<ConnectionStringUnit> connectionStringRepository,
           OrganizationUnitManager organizationUnitManager)
        {
            _organizationExtendedUnitManager = organizationExtendedUnitManager;
            _organizationUnitRepository = organizationUnitRepository;
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
            _connectionStringRepository = connectionStringRepository;
            _organizationUnitManager = organizationUnitManager;
            _organizationExtendedUnitRepository = organizationExtendedUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _customAppSession = customAppSession;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits)]
        public async Task<ListResultOutput<OrganizationUnitDto>> GetOrganizationUnits()
        {
            var query =
                from ou in _organizationExtendedUnitRepository.GetAll()
                join uou in _userOrganizationUnitRepository.GetAll() on ou.Id equals uou.OrganizationUnitId into g
                select new { ou, memberCount = g.Count() };

            var items = await query.ToListAsync();
            return new ListResultOutput<OrganizationUnitDto>(
                items.Select(item =>
                {
                    var dto = item.ou.MapTo<OrganizationUnitDto>();
                    dto.MemberCount = item.memberCount;
                    return dto;
                }).ToList());
        }

        /// <summary>
        /// Get OrganizationList(HOST)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits)]
        public async Task<PagedResultOutput<HostOrganizationUnitDto>> GetHostOrganizationUnits(SearchInputDto input)
        {
            var query = from organization in _organizationExtendedUnitRepository.GetAll()
                        join constr in _connectionStringRepository.GetAll() on organization.ConnectionStringId equals constr.Id
                         into constring
                        from construnits in constring.DefaultIfEmpty()
                        select new { organization, ConnectionStringName= construnits.Name };



            if (!ReferenceEquals(input.Filters, null))
            {
                var mapSearchFilters = Helper.MappingFilters(input.Filters);
                if (!ReferenceEquals(mapSearchFilters, null))
                    query = query.CreateFilters(mapSearchFilters);
            }

            var resultCount = await query.CountAsync();
            var results = await query
                .AsNoTracking()
                .OrderBy(Helper.GetSort("organization.DisplayName ASC", input.Sorting))
                .PageBy(input)
                .ToListAsync();
            return new PagedResultOutput<HostOrganizationUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.organization.MapTo<HostOrganizationUnitDto>();
                if (item.organization.ConnectionStringId != null)
                {
                    dto.ConnectionStringId = item.organization.ConnectionStringId.Value;
                    dto.ConnectionStringName = item.ConnectionStringName;
                }
                return dto;
            }).ToList());
        }

        /// <summary>
        /// Abp Method to create Organization
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> CreateOrganizationUnit(CreateOrganizationUnitInput input)
        {
            var organizationUnit = new OrganizationUnit(AbpSession.TenantId, input.DisplayName, input.ParentId);

            await _organizationUnitManager.CreateAsync(organizationUnit);
            await CurrentUnitOfWork.SaveChangesAsync();

            return organizationUnit.MapTo<OrganizationUnitDto>();
        }

        /// <summary>
        /// Abp Method to update Organization
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> UpdateOrganizationUnit(UpdateOrganizationUnitInput input)
        {
            var organizationUnit = await _organizationUnitRepository.GetAsync(input.Id);

            organizationUnit.DisplayName = input.DisplayName;

            await _organizationUnitManager.UpdateAsync(organizationUnit);

            return await CreateOrganizationUnitDto(organizationUnit);
        }


        /// <summary>
        /// Sumit Method to Create HostOrganization(CompanyGroup)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task CreateHostOrganizationUnit(CreateHostOrganizationUnitInput input)
        {
          
            var organizationUnit = new OrganizationExtended(tenantid: AbpSession.TenantId, displayname:input.DisplayName,parentid: input.ParentId,connectionStringid:input.ConnectionStringId);

            await _organizationExtendedUnitManager.CreateAsync(organizationUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

     
        /// <summary>
        /// Sumit Method
        /// Update Host Organization (CompanyGroup)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task  UpdateHostOrganizationUnit(UpdateHostOrganizationUnitInput input)
        {
            var organizationUnit = await _organizationExtendedUnitRepository.GetAsync(input.Id);
            organizationUnit.DisplayName = input.DisplayName;
            await _organizationExtendedUnitManager.UpdateAsync(organizationUnit);
        }

        

        public async Task<PagedResultOutput<OrganizationUnitUserListDto>> GetOrganizationUnitUsers(GetOrganizationUnitUsersInput input)
        {
            var query = from uou in _userOrganizationUnitRepository.GetAll()
                        join ou in _organizationUnitRepository.GetAll() on uou.OrganizationUnitId equals ou.Id
                        join user in UserManager.Users on uou.UserId equals user.Id
                        where uou.OrganizationUnitId == input.Id
                        orderby input.Sorting
                        select new { uou, user };

            var totalCount = await query.CountAsync();
            var items = await query.PageBy(input).ToListAsync();

            return new PagedResultOutput<OrganizationUnitUserListDto>(
                totalCount,
                items.Select(item =>
                {
                    var dto = item.user.MapTo<OrganizationUnitUserListDto>();
                    dto.AddedTime = item.uou.CreationTime;
                    return dto;
                }).ToList());
        }


        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> MoveOrganizationUnit(MoveOrganizationUnitInput input)
        {
            await _organizationExtendedUnitManager.MoveAsync(input.Id, input.NewParentId);

            return await CreateOrganizationUnitDto(
                await _organizationUnitRepository.GetAsync(input.Id)
                );
        }
        public async Task DeleteOrganizationUnit(IdInput<long> input)
        {
            if (input.Id != 1)
            {
                await _organizationExtendedUnitManager.DeleteAsync(input.Id);
            }
        }
        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task DeleteHostOrganizationUnit(IdInput<long> input)
        {
            if (input.Id != 1)
            {
                await _organizationExtendedUnitManager.DeleteAsync(input.Id);
            }
        }

        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers)]
        public async Task AddUserToOrganizationUnit(UserToOrganizationUnitInput input)
        {
            await UserManager.AddToOrganizationUnitAsync(input.UserId, input.OrganizationUnitId);

            // Set DefaultOrganizationId to the User
            var user = await UserManager.GetUserByIdAsync(input.UserId);
            if (!user.DefaultOrganizationId.HasValue)
            {
                user.DefaultOrganizationId = input.OrganizationUnitId;
                await UserManager.UpdateAsync(user);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers)]
        public async Task RemoveUserFromOrganizationUnit(UserToOrganizationUnitInput input)
        {
            await UserManager.RemoveFromOrganizationUnitAsync(input.UserId, input.OrganizationUnitId);

            // set DefaultOrganizationId to null if the user has to remove organizationid is default organizationId
            var user = await UserManager.GetUserByIdAsync(input.UserId);
            if (user.DefaultOrganizationId.HasValue && user.DefaultOrganizationId.Value.CompareTo(input.OrganizationUnitId) == 0)
            {
                user.DefaultOrganizationId = null;
                await UserManager.UpdateAsync(user);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers)]
        public async Task<bool> IsInOrganizationUnit(UserToOrganizationUnitInput input)
        {
            return await UserManager.IsInOrganizationUnitAsync(input.UserId, input.OrganizationUnitId);
        }



        private async Task<OrganizationUnitDto> CreateOrganizationUnitDto(OrganizationUnit organizationUnit)
        {
            var dto = organizationUnit.MapTo<OrganizationUnitDto>();
            dto.MemberCount = await _userOrganizationUnitRepository.CountAsync(uou => uou.OrganizationUnitId == organizationUnit.Id);
            return dto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetOrganizationsListByUserId(IdInput<long> input)
        {
            if (_customAppSession.TenantId != null)
                _unitOfWorkManager.Current.SetTenantId(Convert.ToInt32(_customAppSession.TenantId));

            var organizations = await
                (from userOrg in _userOrganizationUnitRepository.GetAll()
                 join org in _organizationUnitRepository.GetAll() on userOrg.OrganizationUnitId equals org.Id
                 where userOrg.UserId == input.Id
                 select new NameValueDto { Name = org.DisplayName, Value = org.Id.ToString() }).ToListAsync();
            return organizations;
        }

        /// <summary>
        /// Get Host OrganizationsList
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetHostOrganizationsList()
        {
            var organizations= await (from organization in _organizationExtendedUnitRepository.GetAll()
            select new NameValueDto { Name = organization.DisplayName, Value = organization.Id.ToString() }).ToListAsync();
            return organizations;
        }

        /// <summary>
        /// Get ConnectionStringList
        /// </summary>
        /// <returns></returns>
        public async Task<List<NameValueDto>> GetConnectionStrings()
        {
            return await _connectionStringRepository.GetAll()
               .Select(u => new NameValueDto { Name = u.Name, Value = u.Id.ToString() }).ToListAsync();
        }

    }
}