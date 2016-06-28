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
using CAPS.CORPACCOUNTING.Authorization.Users;
using Abp.Domain.Uow;
using CAPS.CORPACCOUNTING.Masters;
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Configuration;
using CAPS.CORPACCOUNTING.Configuration.Host.Dto;
using CAPS.CORPACCOUNTING.Configuration;
using System;
using System.Configuration;

namespace CAPS.CORPACCOUNTING.Organizations
{
    [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits)]
    public class OrganizationUnitAppService : CORPACCOUNTINGAppServiceBase, IOrganizationUnitAppService
    {
        private readonly OrganizationUnitManager _organizationUnitManager;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;
        private readonly IRepository<AddressUnit, long> _addressRepository;
        private readonly ISettingDefinitionManager _settingDefinitionManager;
        public OrganizationUnitAppService(
            OrganizationUnitManager organizationUnitManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IRepository<AddressUnit, long> addressRepository,
            ISettingDefinitionManager settingDefinitionManager
            )
        {
            _organizationUnitManager = organizationUnitManager;
            _organizationUnitRepository = organizationUnitRepository;
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
            _addressRepository = addressRepository;
            _settingDefinitionManager = settingDefinitionManager;
        }

        public async Task<ListResultOutput<OrganizationUnitDto>> GetOrganizationUnits()
        {
            var query =
                from ou in _organizationUnitRepository.GetAll()
                join address in _addressRepository.GetAll().Where(u=>u.TypeofObjectId==TypeofObject.Org) on ou.Id equals address.ObjectId
                join uou in _userOrganizationUnitRepository.GetAll() on ou.Id equals uou.OrganizationUnitId into g
                select new { ou, memberCount = g.Count() };

            var items = await query.ToListAsync();

            //var hostSettings = new HostSettingsEditDto
            //{
            //    OrganizationManagement = new OrganizationManagementSettingsEditDto
            //    {
            //        IsAllowDuplicateAPInvoiceNos = await SettingManager.GetSettingValueAsync<bool>(AppSettings.OrganizationManagement.AllowDuplicateAPInvoiceNos),
            //        IsAllowDuplicateARInvoiceNos = await SettingManager.GetSettingValueAsync<bool>(AppSettings.OrganizationManagement.AllowDuplicateARInvoiceNos),
            //        AllowTransactionsJobWithGL = await SettingManager.GetSettingValueAsync<bool>(AppSettings.OrganizationManagement.AllowTransactionsactionsJobWithGL),
            //        APAgingDate = await SettingManager.GetSettingValueAsync<bool>(AppSettings.OrganizationManagement.APAgingDate),
            //        ARAgingDate = await SettingManager.GetSettingValueAsync<bool>(AppSettings.OrganizationManagement.ARAgingDate),
            //        BuildAPuponCCstatementPosting = await SettingManager.GetSettingValueAsync<bool>(AppSettings.OrganizationManagement.BuildAPuponCCstatementPosting),
            //        BuildAPuponPayrollPosting = await SettingManager.GetSettingValueAsync<bool>(AppSettings.OrganizationManagement.BuildAPuponPayrollPosting),
            //        DefaultAPPostingDate = await SettingManager.GetSettingValueAsync<bool>(AppSettings.OrganizationManagement.APPostingDateDefault),
            //        DefaultBank = await SettingManager.GetSettingValueAsync<long>(AppSettings.OrganizationManagement.DefaultBank),
            //        DepositGracePeriods = await SettingManager.GetSettingValueAsync<int>(AppSettings.OrganizationManagement.DepositGracePeriods),
            //        IsAllowAccountnumbersStartingwithZero = await SettingManager.GetSettingValueAsync<bool>(AppSettings.OrganizationManagement.AllowAccountNumbersStartingWithZero),
            //        IsImportPOlogsfromProducersActualUploads = await SettingManager.GetSettingValueAsync<bool>(AppSettings.OrganizationManagement.ImportPOlogsfromProducersActualuploads),
            //        PaymentsGracePeriods = await SettingManager.GetSettingValueAsync<int>(AppSettings.OrganizationManagement.PaymentGracePeriods),
            //        POAutoNumbering = await SettingManager.GetSettingValueAsync<bool>(AppSettings.OrganizationManagement.POAutoNumbering),
            //    }

            //};

            return new ListResultOutput<OrganizationUnitDto>(
                items.Select(item =>
                {
                    var dto = item.ou.MapTo<OrganizationUnitDto>();
                    dto.MemberCount = item.memberCount;
                    return dto;
                }).ToList());
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

        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> CreateOrganizationUnit(CreateOrganizationUnitInput input)
        {
            var organizationUnit = new OrganizationUnit(AbpSession.TenantId, input.DisplayName, input.ParentId);

            await _organizationUnitManager.CreateAsync(organizationUnit);
            await CurrentUnitOfWork.SaveChangesAsync();


            // Set DefaultOrganizationId to the User if DefaultOrganizationId is null
            var user = await UserManager.GetUserByIdAsync(organizationUnit.CreatorUserId.Value);
            if (!user.DefaultOrganizationId.HasValue)
            {
                user.DefaultOrganizationId = organizationUnit.Id;
                await UserManager.UpdateAsync(user);
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            //address Information
            if (!ReferenceEquals(input.Address, null))
            {
               
                    if (input.Address.Line1 != null || input.Address.Line2 != null ||
                        input.Address.Line4 != null || input.Address.Line4 != null ||
                        input.Address.State != null || input.Address.Country != null ||
                        input.Address.Email != null || input.Address.Phone1 != null ||
                        input.Address.ContactNumber != null)
                    {
                    input.Address.TypeofObjectId = TypeofObject.Org;
                    input.Address.ObjectId = organizationUnit.Id;
                        var addressUnit = input.Address.MapTo<AddressUnit>();
                        await _addressRepository.InsertAsync(addressUnit);
                    }
                    await CurrentUnitOfWork.SaveChangesAsync();
               
            }

            //Organization Settings

            return organizationUnit.MapTo<OrganizationUnitDto>();
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> UpdateOrganizationUnit(UpdateOrganizationUnitInput input)
        {
            var organizationUnit = await _organizationUnitRepository.GetAsync(input.Id);

            organizationUnit.DisplayName = input.DisplayName;

            await _organizationUnitManager.UpdateAsync(organizationUnit);


            // update address Information

            if (!ReferenceEquals(input.Address, null))
            {
                if (input.Address.AddressId != 0)
                {
                    var addressUnit = input.Address.MapTo<AddressUnit>();
                    await _addressRepository.UpdateAsync(addressUnit);
                }
                else
                {
                    if (input.Address.Line1 != null || input.Address.Line2 != null ||
                        input.Address.Line4 != null || input.Address.Line4 != null ||
                        input.Address.State != null || input.Address.Country != null ||
                        input.Address.Email != null || input.Address.Phone1 != null || input.Address.Website != null)
                    {
                        input.Address.TypeofObjectId = TypeofObject.Org;
                        input.Address.ObjectId = input.Id;
                        var addressUnit = input.Address.MapTo<AddressUnit>();
                        await _addressRepository.InsertAsync(addressUnit);
                    }
                }
            }
            await CurrentUnitOfWork.SaveChangesAsync();
            return await CreateOrganizationUnitDto(organizationUnit);
        }


        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> MoveOrganizationUnit(MoveOrganizationUnitInput input)
        {
            await _organizationUnitManager.MoveAsync(input.Id, input.NewParentId);

            return await CreateOrganizationUnitDto(
                await _organizationUnitRepository.GetAsync(input.Id)
                );
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task DeleteOrganizationUnit(IdInput<long> input)
        {
            await _addressRepository.DeleteAsync(p => p.ObjectId == input.Id && p.TypeofObjectId == TypeofObject.Org);
            await _organizationUnitManager.DeleteAsync(input.Id);
        }

        [UnitOfWork]
        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers)]
        public async Task AddUserToOrganizationUnit(UserToOrganizationUnitInput input)
        {
            await UserManager.AddToOrganizationUnitAsync(input.UserId, input.OrganizationUnitId);

            /// Set DefaultOrganizationId to the User
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

    }
}