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
using CAPS.CORPACCOUNTING.Masters.Dto;
using Abp.Configuration;
using System;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Authorization.Users.Profile.Dto;
using System.IO;
using System.Drawing;
using Abp.UI;
using Abp.IO;
using CAPS.CORPACCOUNTING.Organization;
using System.Collections.Generic;
using CAPS.CORPACCOUNTING.Sessions;
using System.Security.Claims;
using System.Threading;
using CAPS.CORPACCOUNTING.Helpers;
using CAPS.CORPACCOUNTING.GenericSearch.Dto;
using System.Linq.Dynamic;
using AutoMapper;
using CAPS.CORPACCOUNTING.Configuration.Organization;
using Abp.Runtime.Session;
using CAPS.CORPACCOUNTING.Configuration;
using CAPS.CORPACCOUNTING.Configuration.Host.Dto;
using Microsoft.AspNet.Identity;

namespace CAPS.CORPACCOUNTING.Organizations
{
    [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits)]
    public class OrganizationUnitAppService : CORPACCOUNTINGAppServiceBase, IOrganizationUnitAppService
    {
        private readonly OrganizationExtendedUnitManager _organizationExtendedUnitManager;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<OrganizationExtended, long> _organizationExtendedUnitRepository;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;
        private readonly IRepository<AddressUnit, long> _addressRepository;
        private readonly ISettingDefinitionManager _settingDefinitionManager;
        private readonly IAppFolders _appFolders;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly CustomAppSession _customAppSession;
        private readonly IOrganizationSettingManager _organizationSettingManager;
        public OrganizationUnitAppService(
            OrganizationExtendedUnitManager organizationExtendedUnitManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IRepository<AddressUnit, long> addressRepository,
            ISettingDefinitionManager settingDefinitionManager,
            IAppFolders appFolders,
            IUnitOfWorkManager unitOfWorkManager,
            CustomAppSession customAppSession,
            IRepository<OrganizationExtended, long> organizationExtendedUnitRepository,
           IOrganizationSettingManager organizationSettingManager)
        {
            _organizationExtendedUnitManager = organizationExtendedUnitManager;
            _organizationUnitRepository = organizationUnitRepository;
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
            _addressRepository = addressRepository;

            _organizationSettingManager = organizationSettingManager;
            _settingDefinitionManager = settingDefinitionManager;
            _appFolders = appFolders;
            _organizationExtendedUnitRepository = organizationExtendedUnitRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _customAppSession = customAppSession;
        }

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
        public async Task<PagedResultOutput<OrganizationUnitDto>> GetHostOrganizationUnits(SearchInputDto input)
        {
            var query = from organization in _organizationExtendedUnitRepository.GetAll()
                        select new { organization };



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
            return new PagedResultOutput<OrganizationUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.organization.MapTo<OrganizationUnitDto>();
                return dto;
            }).ToList());
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task CreateHostOrganizationUnit(CreateOrganizationUnitInput input)
        {


            var organizationUnit = new OrganizationExtended(AbpSession.TenantId, input.DisplayName, input.ParentId, input.TransmitterContactName,
                input.TransmitterEmailAddress, input.TransmitterCode, input.TransmitterControlCode, input.FederalTaxId, null);

            await _organizationExtendedUnitManager.CreateAsync(organizationUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

     
        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task  UpdateHostOrganizationUnit(UpdateOrganizationUnitInput input)
        {

            var organizationUnit = await _organizationExtendedUnitRepository.GetAsync(input.Id);
            organizationUnit.DisplayName = input.DisplayName;
            await _organizationExtendedUnitManager.UpdateAsync(organizationUnit);
        }


        /// <summary>
        /// Get ComapnySetup (Tenant Organizations)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        [AbpAuthorize(AppPermissions.Pages_Administration_CompanySetUp)]
        public async Task<PagedResultOutput<OrganizationUnitDto>> GetOrganizationUnits(SearchInputDto input)
        {
            var query = from organization in _organizationExtendedUnitRepository.GetAll()
                        join address in _addressRepository.GetAll().Where(u => u.TypeofObjectId == TypeofObject.Org) on organization.Id equals address.ObjectId into addresss
                        from address in addresss.DefaultIfEmpty()
                        join uou in _userOrganizationUnitRepository.GetAll() on organization.Id equals uou.OrganizationUnitId into g
                        select new { organization, address, memberCount = g.Count() };



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
            return new PagedResultOutput<OrganizationUnitDto>(resultCount, results.Select(item =>
            {
                var dto = item.organization.MapTo<OrganizationUnitDto>();
                dto.TransmitterCode = item.organization.TransmitterCode;
                dto.TransmitterControlCode = item.organization.TransmitterControlCode;
                dto.TransmitterContactName = item.organization.TransmitterContactName;
                dto.TransmitterEmailAddress = item.organization.TransmitterEmailAddress;
                dto.FederalTaxId = item.organization.FederalTaxId;
                dto.MemberCount = item.memberCount;
                dto.Address = item.address.MapTo<AddressUnitDto>();
                if (item.address != null)
                    dto.Address.AddressId = item.address.Id;
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
        [AbpAuthorize(AppPermissions.Pages_Administration_CompanySetUp_Edit)]
        public async Task<OrganizationUnitDto> CreateOrganizationUnit(CreateOrganizationUnitInput input)
        {

            byte[] logo = null;
            if (!ReferenceEquals(input.Logo, null))
                logo = await UpdateOrganizationPicture(input.Logo);

            var organizationUnit = new OrganizationExtended(AbpSession.TenantId, input.DisplayName, input.ParentId, input.TransmitterContactName,
                input.TransmitterEmailAddress, input.TransmitterCode, input.TransmitterControlCode, input.FederalTaxId, logo);

            await _organizationExtendedUnitManager.CreateAsync(organizationUnit);
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

        [AbpAuthorize(AppPermissions.Pages_Administration_CompanySetUp_Create)]
        public async Task<OrganizationUnitDto> UpdateOrganizationUnit(UpdateOrganizationUnitInput input)
        {


            byte[] logo = null;
            if (!ReferenceEquals(input.Logo, null))
                logo = await UpdateOrganizationPicture(input.Logo);

            var organizationUnit = await _organizationExtendedUnitRepository.GetAsync(input.Id);

            organizationUnit.DisplayName = input.DisplayName;
            organizationUnit.TransmitterContactName = input.TransmitterContactName;
            organizationUnit.TransmitterControlCode = input.TransmitterControlCode;
            organizationUnit.TransmitterEmailAddress = input.TransmitterEmailAddress;
            organizationUnit.TransmitterCode = input.TransmitterCode;
            organizationUnit.FederalTaxId = input.FederalTaxId;
            await _organizationExtendedUnitManager.UpdateAsync(organizationUnit);


            // update address Information

            if (!ReferenceEquals(input.Address, null))
            {
                if (input.Address.AddressId != 0)
                {

                    var addressUnit =
              await _addressRepository.GetAsync(input.Address.AddressId);
                    Mapper.Map(input.Address, addressUnit);
                    //addressUnit.Id = input.Address.AddressId;
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


            if (!ReferenceEquals(input.OrganizationSettings, null))
            {
                input.OrganizationSettings.OrganizationUnitId = input.Id;
                await UpdateAllSettings(input.OrganizationSettings);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
            return await CreateOrganizationUnitDto(organizationUnit);
        }


        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task<OrganizationUnitDto> MoveOrganizationUnit(MoveOrganizationUnitInput input)
        {
            await _organizationExtendedUnitManager.MoveAsync(input.Id, input.NewParentId);

            return await CreateOrganizationUnitDto(
                await _organizationUnitRepository.GetAsync(input.Id)
                );
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree)]
        public async Task DeleteOrganizationUnit(IdInput<long> input)
        {
            await _addressRepository.DeleteAsync(p => p.ObjectId == input.Id && p.TypeofObjectId == TypeofObject.Org);
            await _organizationExtendedUnitManager.DeleteAsync(input.Id);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private async Task<byte[]> UpdateOrganizationPicture(UpdateProfilePictureInput input)
        {
            var tempProfilePicturePath = Path.Combine(_appFolders.TempFileDownloadFolder, input.FileName);

            byte[] byteArray;

            using (var fsTempProfilePicture = new FileStream(tempProfilePicturePath, FileMode.Open))
            {
                using (var bmpImage = new Bitmap(fsTempProfilePicture))
                {
                    var width = input.Width == 0 ? bmpImage.Width : input.Width;
                    var height = input.Height == 0 ? bmpImage.Height : input.Height;
                    var bmCrop = bmpImage.Clone(new Rectangle(input.X, input.Y, width, height), bmpImage.PixelFormat);

                    using (var stream = new MemoryStream())
                    {
                        bmCrop.Save(stream, bmpImage.RawFormat);
                        stream.Close();
                        byteArray = stream.ToArray();
                    }
                }
            }

            if (byteArray.LongLength > 102400) //100 KB
            {
                throw new UserFriendlyException(L("ResizedProfilePicture_Warn_SizeLimit"));
            }

            FileHelper.DeleteIfExists(tempProfilePicturePath);
            return byteArray;
        }

        public async Task<OrganizationManagementSettingsEditDto> GetAllSettings(IdInput<long> input)
        {

            var settings = new OrganizationManagementSettingsEditDto
            {

                IsAllowDuplicateAPInvoiceNos = Convert.ToBoolean(await _organizationSettingManager.GetSettingValueForOrganization(input.Id,
                                AppSettings.OrganizationManagement.AllowDuplicateAPInvoiceNos)),
                IsAllowDuplicateARInvoiceNos = Convert.ToBoolean(await _organizationSettingManager.GetSettingValueForOrganization(input.Id,
                                AppSettings.OrganizationManagement.AllowDuplicateARInvoiceNos)),
                IsAllowAccountnumbersStartingwithZero = Convert.ToBoolean(await _organizationSettingManager.GetSettingValueForOrganization(input.Id,
                                AppSettings.OrganizationManagement.AllowAccountNumbersStartingWithZero)),
                IsImportPOlogsfromProducersActualUploads = Convert.ToBoolean(await _organizationSettingManager.GetSettingValueForOrganization(input.Id,
                                AppSettings.OrganizationManagement.ImportPOlogsfromProducersActualuploads)),
                BuildAPuponCCstatementPosting = Convert.ToBoolean(await _organizationSettingManager.GetSettingValueForOrganization(input.Id,
                                AppSettings.OrganizationManagement.BuildAPuponCCstatementPosting)),
                BuildAPuponPayrollPosting = Convert.ToBoolean(await _organizationSettingManager.GetSettingValueForOrganization(input.Id,
                                AppSettings.OrganizationManagement.BuildAPuponPayrollPosting)),
                ARAgingDate = await _organizationSettingManager.GetSettingValueForOrganization(input.Id,
                            AppSettings.OrganizationManagement.ARAgingDate),
                APAgingDate = await _organizationSettingManager.GetSettingValueForOrganization(input.Id,
                            AppSettings.OrganizationManagement.APAgingDate),
                DepositGracePeriods = Convert.ToInt32(await _organizationSettingManager.GetSettingValueForOrganization(input.Id,
                                AppSettings.OrganizationManagement.DepositGracePeriods)),
                PaymentsGracePeriods = Convert.ToInt32(await _organizationSettingManager.GetSettingValueForOrganization(input.Id,
                                AppSettings.OrganizationManagement.PaymentGracePeriods)),
                DefaultAPPostingDate = await _organizationSettingManager.GetSettingValueForOrganization(input.Id,
                            AppSettings.OrganizationManagement.APPostingDateDefault),
                DefaultBank = Convert.ToInt64(await _organizationSettingManager.GetSettingValueForOrganization(input.Id,
                                AppSettings.OrganizationManagement.DefaultBank)),
                AllowTransactionsJobWithGL = Convert.ToBoolean(await _organizationSettingManager.GetSettingValueForOrganization(input.Id,
                                AppSettings.OrganizationManagement.AllowTransactionsactionsJobWithGL)),
                SetDefaultAPTerms = Convert.ToInt32(await _organizationSettingManager.GetSettingValueForOrganization(input.Id,
                                AppSettings.OrganizationManagement.SetDefaultAPTerms)),
                SetDefaultARTerms = Convert.ToInt32(await _organizationSettingManager.GetSettingValueForOrganization(input.Id,
                                AppSettings.OrganizationManagement.SetDefaultARTerms)),
                POAutoNumberingforDivisions = Convert.ToBoolean(await _organizationSettingManager.GetSettingValueForOrganization(input.Id,
                                AppSettings.OrganizationManagement.POAutoNumberingforDivisions)),
                POAutoNumberingforProjects = Convert.ToBoolean(await _organizationSettingManager.GetSettingValueForOrganization(input.Id,
                                AppSettings.OrganizationManagement.POAutoNumberingforProjects)),

                OrganizationUnitId = input.Id

            };
            return settings;
        }

        /// <summary>
        /// Update the OrganizationLevel Settings
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [UnitOfWork]
        public async Task UpdateAllSettings(OrganizationManagementSettingsEditDto input)
        {
            //Tenant management
            await _organizationSettingManager.ChangeSettingForOrganizationAsync(input.OrganizationUnitId.Value,
                AppSettings.OrganizationManagement.AllowDuplicateAPInvoiceNos, input.IsAllowDuplicateAPInvoiceNos.ToString());
            await _organizationSettingManager.ChangeSettingForOrganizationAsync(input.OrganizationUnitId.Value,
                    AppSettings.OrganizationManagement.AllowDuplicateARInvoiceNos, input.IsAllowDuplicateARInvoiceNos.ToString());
            await _organizationSettingManager.ChangeSettingForOrganizationAsync(input.OrganizationUnitId.Value,
                    AppSettings.OrganizationManagement.AllowAccountNumbersStartingWithZero, input.IsAllowAccountnumbersStartingwithZero.ToString());
            await _organizationSettingManager.ChangeSettingForOrganizationAsync(input.OrganizationUnitId.Value,
                    AppSettings.OrganizationManagement.ImportPOlogsfromProducersActualuploads, input.IsImportPOlogsfromProducersActualUploads.ToString());
            await _organizationSettingManager.ChangeSettingForOrganizationAsync(input.OrganizationUnitId.Value,
                    AppSettings.OrganizationManagement.BuildAPuponCCstatementPosting, input.BuildAPuponCCstatementPosting.ToString());
            await _organizationSettingManager.ChangeSettingForOrganizationAsync(input.OrganizationUnitId.Value,
                    AppSettings.OrganizationManagement.ARAgingDate, input.ARAgingDate);
            await _organizationSettingManager.ChangeSettingForOrganizationAsync(input.OrganizationUnitId.Value,
                    AppSettings.OrganizationManagement.APAgingDate, input.APAgingDate);
            await _organizationSettingManager.ChangeSettingForOrganizationAsync(input.OrganizationUnitId.Value,
                    AppSettings.OrganizationManagement.DepositGracePeriods, input.DepositGracePeriods.HasValue ? input.DepositGracePeriods.ToString() : null);
            await _organizationSettingManager.ChangeSettingForOrganizationAsync(input.OrganizationUnitId.Value,
                    AppSettings.OrganizationManagement.PaymentGracePeriods, input.PaymentsGracePeriods.HasValue ? input.PaymentsGracePeriods.ToString() : null);
            await _organizationSettingManager.ChangeSettingForOrganizationAsync(input.OrganizationUnitId.Value,
                    AppSettings.OrganizationManagement.APPostingDateDefault, input.DefaultAPPostingDate);
            await _organizationSettingManager.ChangeSettingForOrganizationAsync(input.OrganizationUnitId.Value,
                    AppSettings.OrganizationManagement.DefaultBank, input.DefaultBank.HasValue ? input.DefaultBank.ToString() : null);
            await _organizationSettingManager.ChangeSettingForOrganizationAsync(input.OrganizationUnitId.Value,
                    AppSettings.OrganizationManagement.AllowTransactionsactionsJobWithGL, input.AllowTransactionsJobWithGL.ToString());
            await _organizationSettingManager.ChangeSettingForOrganizationAsync(input.OrganizationUnitId.Value,
                   AppSettings.OrganizationManagement.POAutoNumberingforDivisions, input.POAutoNumberingforDivisions.ToString());
            await _organizationSettingManager.ChangeSettingForOrganizationAsync(input.OrganizationUnitId.Value,
                  AppSettings.OrganizationManagement.POAutoNumberingforProjects, input.POAutoNumberingforProjects.ToString());

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
      
    }
}